import { Injectable } from '@nestjs/common';
import { Cron } from '@nestjs/schedule';
import { ECronName, ECronTime, ERemoteTime } from 'src/base/interfaces';
import { CTimeZone } from 'src/base/interfaces/constants';
import { DailyService } from 'src/daily/services';
import { RemoteService } from 'src/remote/services';
import { RocketChatService } from 'src/rocket-chat/services';
import {
  checkTimeIsBetweenTimeAndNow,
  checkTimeIsInThisDayAndNow,
  messageValidate,
} from 'src/base/tools';
import { DailyDTO, NoDailyDTO } from 'src/daily/dtos';
import moment from 'moment';
import xl from 'excel4node';
import { InspectorService } from 'src/inspector/services';
import { GGDriveService, S3Service } from 'src/gg-drive/services';
import { EnvService } from 'src/environment';
import path from 'path';
import fs from 'fs';
import { v4 } from 'uuid';
import { EProjectType } from 'src/project/interfaces';

@Injectable()
export class CronJobService {
  constructor(
    private readonly rocketchatService: RocketChatService,
    private readonly dailyService: DailyService,
    private readonly remoteService: RemoteService,
    private readonly inspectorService: InspectorService,
    private readonly ggDriveService: GGDriveService,
    private readonly config: EnvService,
    private readonly s3Service: S3Service,
  ) {}

  private async checkDaily(
    time: ERemoteTime,
  ): Promise<{ daily: DailyDTO[]; noDaily: NoDailyDTO[] }> {
    try {
      await this.rocketchatService.updateListChanelJoined();
      await this.rocketchatService.updateListTeamJoined();
      let remotes: any[];
      switch (time) {
        case ERemoteTime.Morning:
          remotes = await this.remoteService.findAllRemoteMorningAndAllDay();
          break;
        case ERemoteTime.Afternoon:
          remotes = await this.remoteService.findAllRemoteAfternoonAndAllDay();
          break;
      }
      const daily: DailyDTO[] = [];
      const noDaily: NoDailyDTO[] = [];
      for (const remote of remotes) {
        const messages = await this.rocketchatService.searchMessage(
          remote.projectId.projectKomuId,
        );
        if (messages.length === 0) {
          noDaily.push({
            remoteId: remote._id,
            username: remote.username,
            teamName: remote.projectId.name,
            projectId: remote.projectId,
            name: remote.name,
            type: remote.projectId.type,
            timeRemote: remote.remoteTime,
          });
        }
        messages.every((message) => {
          const checkTimeMessage =
            time === ERemoteTime.Morning
              ? checkTimeIsInThisDayAndNow(message.time)
              : checkTimeIsBetweenTimeAndNow(
                  moment()
                    .set({ hour: 12, minute: 0, second: 0 })
                    .toISOString(),
                  message.time,
                );
          if (
            checkTimeMessage &&
            message.user.username === remote.username &&
            messageValidate(message.content)
          ) {
            return daily.push({
              teamName: remote.projectId.name,
              username: remote.username,
              remoteId: remote._id,
              projectId: remote.projectId,
              content: message.content,
              time: message.time,
              type: remote.projectId.type,
              timeRemote: remote.remoteTime,
            });
          }
        });
        noDaily.push({
          remoteId: remote._id,
          username: remote.username,
          teamName: remote.projectId.name,
          projectId: remote.projectId,
          name: remote.name,
          type: remote.projectId.type,
          timeRemote: remote.remoteTime,
        });
      }
      return { daily, noDaily };
    } catch (error) {
      throw error;
    }
  }

  @Cron(ECronTime.Morning, { name: ECronName.Morning, timeZone: CTimeZone })
  public async cronDailyMorning(): Promise<void> {
    try {
      const { daily, noDaily } = await this.checkDaily(ERemoteTime.Morning);
      if (daily.length !== 0) {
        for (const _ of daily) {
          await this.dailyService.create({
            ..._,
            timeRemote: ERemoteTime.Morning,
          });
        }
      }
      if (noDaily.length !== 0) {
        for (const _ of noDaily) {
          await this.rocketchatService.sendMessageDailyMorning({
            channel: _.username,
            teamName: _.teamName,
            type: _.type,
          });
        }
      }
    } catch (error) {
      throw error;
    }
  }

  @Cron(ECronTime.Afternoon, {
    name: ECronName.Afternoon,
    timeZone: CTimeZone,
  })
  public async cronDailyAfternoon(): Promise<void> {
    try {
      const { daily, noDaily } = await this.checkDaily(ERemoteTime.Morning);
      if (daily.length !== 0) {
        for (const _ of daily) {
          await this.dailyService.create({
            ..._,
            timeRemote: ERemoteTime.Afternoon,
          });
          await this.rocketchatService.sendMessageDailyAfternoon({
            channel: _.username,
            teamName: _.teamName,
            isDaily: true,
            type: _.type,
          });
        }
      }
      if (noDaily.length !== 0) {
        for (const _ of noDaily) {
          await this.rocketchatService.sendMessageDailyAfternoon({
            channel: _.username,
            teamName: _.teamName,
            isDaily: false,
            type: _.type,
          });
        }
      }
      return;
    } catch (error) {
      throw error;
    }
  }

  @Cron(ECronTime.EndTimeDailyMorning, {
    name: ECronTime.EndTimeDailyMorning,
    timeZone: CTimeZone,
  })
  public async cronCheckPunishMorning(): Promise<void> {
    try {
      const { daily, noDaily } = await this.checkDaily(ERemoteTime.Morning);
      if (daily.length !== 0) {
        for (const _ of daily) {
          await this.dailyService.create({
            ..._,
            timeRemote: _.timeRemote,
          });
        }
      }
      if (noDaily.length !== 0) {
        for (const _ of noDaily) {
          await this.dailyService.create({
            time: Date(),
            projectId: _.projectId,
            remoteId: _.remoteId,
            content: '',
            noDaily: true,
            timeRemote: _.timeRemote,
          });
          await this.rocketchatService.sendMessagePunish(
            _.username,
            ECronName.Morning,
            _.name,
          );
        }
      }
    } catch (error) {
      throw error;
    }
  }

  @Cron(ECronTime.EndTimeDailyAfternoon, {
    name: ECronTime.EndTimeDailyAfternoon,
    timeZone: CTimeZone,
  })
  public async cronCheckPunishAfternoon(): Promise<void> {
    try {
      const { daily, noDaily } = await this.checkDaily(ERemoteTime.Afternoon);
      if (daily.length !== 0) {
        for (const _ of daily) {
          const dailyLog = await this.dailyService.create({
            ..._,
            timeRemote: _.timeRemote,
          });
          await this.remoteService.updateOneLean(
            { _id: _.remoteId },
            { dailyId: dailyLog._id },
          );
        }
      }
      if (noDaily.length !== 0) {
        for (const _ of noDaily) {
          await this.dailyService.create({
            time: Date(),
            projectId: _.projectId,
            remoteId: _.remoteId,
            content: '',
            noDaily: true,
            timeRemote: _.timeRemote,
          });
          await this.rocketchatService.sendMessagePunish(
            _.username,
            ECronName.Afternoon,
            _.name,
          );
        }
      }
    } catch (error) {
      throw error;
    }
  }

  @Cron(ECronTime.ReportDay, { name: ECronName.ReportDay, timeZone: CTimeZone })
  public async sendCSVDaily(): Promise<void> {
    try {
      const wb = new xl.Workbook();
      const ws = wb.addWorksheet('Daily');
      const style = wb.createStyle({
        font: {
          color: '#FF0800',
          size: 14,
        },
        numberFormat: '$#,##0.00; ($#,##0.00); -',
      });
      ws.cell(1, 1).string('Team').style(style);
      ws.cell(1, 2).string('Username').style(style);
      ws.cell(1, 3).string('Name').style(style);
      ws.cell(1, 4).string('Time daily').style(style);
      ws.cell(1, 5).string('Content').style(style);
      ws.cell(1, 6).string('Daily Time').style(style);
      const daily: any[] = await this.dailyService.findAllDailyOfDay();
      if (daily.length > 0) {
        for (let i = 0; i < daily.length; i++) {
          ws.cell(i + 2, 1).string(daily[i].projectId.name);
          ws.cell(i + 2, 2).string(daily[i].remoteId.username);
          ws.cell(i + 2, 3).string(daily[i].remoteId.name);
          ws.cell(i + 2, 4).string(daily[i].time);
          ws.cell(i + 2, 5).string(daily[i].content);
          ws.cell(i + 2, 6).string(daily[i].timeRemote);
        }
      }
      const ws2 = wb.addWorksheet('No Daily');
      ws2.cell(1, 1).string('Team').style(style);
      ws2.cell(1, 2).string('Username').style(style);
      ws2.cell(1, 3).string('Name').style(style);
      ws2.cell(1, 4).string('Daily Time').style(style);
      const noDaily: any[] = await this.dailyService.findAllNoDaiLyOfDay();
      if (noDaily.length > 0) {
        for (let i = 0; i < noDaily.length; i++) {
          ws2.cell(i + 2, 1).string(noDaily[i].projectId.name);
          ws2.cell(i + 2, 2).string(noDaily[i].remoteId.username);
          ws2.cell(i + 2, 3).string(noDaily[i].remoteId.name);
          ws.cell(i + 2, 4).string(noDaily[i].timeRemote);
        }
      }
      const fileName = this.getNameFile();
      await wb.write(`${fileName}.xlsx`);
      const finalPath = `D:\\NCC\\ncc-bot\\${fileName}.xlsx`;
      // if (!fs.existsSync(finalPath)) {
      //   throw new Error('File not found!');
      // }
      const uploaded: string = await this.s3Service.uploadFile(
        fs.readFileSync('D:\\NCC\\ncc-bot\\Daily_Summary_5-6-2021.xlsx'),
        'Daily_Summary_5-6-2021.xlsx',
      );
      if (uploaded) {
        const inspectors = await this.inspectorService.findAll();
        for (const inspector of inspectors) {
          await this.rocketchatService.sendLinkFile(
            inspector.username,
            uploaded,
          );
        }
      }
      return;
    } catch (error) {
      throw error;
    }
  }

  private getNameFile(): string {
    const date = moment().subtract(1, 'days').startOf('day').local();
    return `Daily_Summary_${date.day()}-${date.month()}-${date.year()}`;
  }
}
