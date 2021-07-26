import { Injectable } from '@nestjs/common';
import axios from 'axios';
import { ECronName } from 'src/base/interfaces';
import { EnvService } from 'src/environment';
import { EProjectType } from 'src/project/interfaces/enums';
import { ProjectService } from '../../project/services';
import { AuthResDTO, SearchMessageResDTO } from '../dtos';

@Injectable()
export class RocketChatService {
  constructor(
    private readonly config: EnvService,
    private readonly projectService: ProjectService,
  ) {}

  private async auth(): Promise<AuthResDTO> {
    try {
      const { rootUrl, user, pass } = this.config.bot;
      const res = await axios.post(`${rootUrl}/api/v1/login`, {
        user,
        password: pass,
      });
      return {
        token: res.data.data.authToken,
        userId: res.data.data.userId,
      };
    } catch (error) {
      throw error;
    }
  }

  private async axiosGet(param: string): Promise<any> {
    try {
      const { userId, token } = await this.auth();
      const url = this.config.bot.rootUrl;
      const res = await axios.get(`${url}/api/v1/${param}`, {
        headers: {
          'X-Auth-Token': token,
          'X-User-Id': userId,
          'Content-type': 'application/json',
        },
      });
      return res.data;
    } catch (error) {
      throw error;
    }
  }

  private async axiosPost(param: string, body: any): Promise<void> {
    try {
      const { userId, token } = await this.auth();
      const url = this.config.bot.rootUrl;
      await axios.post(`${url}/api/v1/${param}`, body, {
        headers: {
          'X-Auth-Token': token,
          'X-User-Id': userId,
          'Content-type': 'application/json',
        },
      });
      return;
    } catch (error) {
      throw error;
    }
  }

  public async updateListTeamJoined(): Promise<void> {
    try {
      const data = await this.axiosGet('teams.list');
      const teams = data.teams;
      for (const team of teams) {
        const isExits = await this.projectService.checkExits({
          projectKomuId: team.roomId,
        });
        if (!isExits) {
          await this.projectService.create({
            name: team.name,
            projectKomuId: team.roomId,
            type: EProjectType.Team,
          });
        }
      }
    } catch (error) {
      throw error;
    }
  }

  public async updateListChanelJoined(): Promise<void> {
    try {
      const data = await this.axiosGet('channels.list.joined');
      const channels: any[] = data.channels;
      for (const channel of channels) {
        const isExits = await this.projectService.checkExits({
          projectKomuId: channel._id,
        });
        if (!isExits) {
          await this.projectService.create({
            name: channel.name,
            projectKomuId: channel._id,
            type: EProjectType.Chanel,
          });
        }
      }
      return;
    } catch (error) {
      throw error;
    }
  }

  public async searchMessage(roomId: string): Promise<SearchMessageResDTO[]> {
    try {
      const { searchText, countHashtag } = this.config.getMany([
        'SEARCH_TEXT',
        'COUNT_HASHTAG',
      ]);
      const data = await this.axiosGet(
        `chat.search?roomId=${roomId}&searchText=${searchText}&count=${countHashtag}`,
      );
      const messages: any[] = data.messages;
      return messages.map((message) => {
        return {
          roomId: message.rid,
          user: {
            username: message.u.username,
            name: message.u.name,
          },
          content: message.msg,
          time: message.ts,
        };
      });
    } catch (error) {
      throw error;
    }
  }

  public async sendMessageDailyMorning(data: {
    channel: string;
    teamName: string;
    type: EProjectType;
  }): Promise<void> {
    try {
      return await this.axiosPost('chat.postMessage', {
        channel: `@${data.channel}`,
        avatar:
          'https://i.pinimg.com/originals/9a/11/33/9a1133d4af3b637e1c6c8ff251785f27.jpg',
        text: `Chào bạn, đã 9h rồi mà bạn chưa daily. Nhanh daily đi nào.`,
        alias: 'Giang hồ Part-time NCC',
        attachments: [
          {
            title: 'Mời bạn click vào đây để daily trong nhóm',
            title_link: `https://komu.nccsoft.vn/${data.type}/${data.teamName}`,
            author_icon:
              'https://image.flaticon.com/icons/png/512/17/17538.png',
            color: 'brown',
            author_name: 'DAILY',
          },
        ],
      });
    } catch (error) {
      throw error;
    }
  }

  public async sendMessageDailyAfternoon(data: {
    channel: string;
    teamName: string;
    isDaily: boolean;
    type: EProjectType;
  }): Promise<void> {
    try {
      const attachments = [];
      if (!data.isDaily)
        attachments.push({
          title: 'Mời bạn click vào đây để daily trong nhóm',
          title_link: `https://komu.nccsoft.vn/${data.type}/${data.teamName}`,
          author_icon: 'https://image.flaticon.com/icons/png/512/17/17538.png',
          color: 'brown',
          author_name: 'DAILY',
        });
      attachments.push({
        title: 'Mời bạn click vào đây để vào log Timesheet',
        title_link: `http://timesheet.nccsoft.vn/`,
        author_icon:
          'https://www.pinclipart.com/picdir/middle/26-269712_svg-transparent-stock-clock-coming-soon-daily-daily.png',
        color: 'brown',
        author_name: 'TIMESHEET',
      });
      return await this.axiosPost('chat.postMessage', {
        avatar:
          'https://i.pinimg.com/originals/9a/11/33/9a1133d4af3b637e1c6c8ff251785f27.jpg',
        channel: `@${data.channel}`,
        text: `Chào bạn, đã 13h30 rồi mà bạn chưa daily. Nhanh daily đi nào.`,
        alias: 'Giang hồ Part-time NCC',
        attachments,
      });
    } catch (error) {
      throw error;
    }
  }

  public async sendMessagePunish(
    channel: string,
    type: ECronName,
    name: string,
  ): Promise<void> {
    try {
      return await this.axiosPost('chat.postMessage', {
        channel: `@${channel}`,
        avatar:
          'https://i.pinimg.com/originals/9a/11/33/9a1133d4af3b637e1c6c8ff251785f27.jpg',
        text: `Chúc mừng ${name} đã bay mất 20k vì không daily ${
          type === ECronName.Morning ? 'buổi sáng' : 'buổi chiều'
        }`,
        alias: 'Giang hồ Part-time NCC',
      });
    } catch (error) {
      throw error;
    }
  }

  public async sendLinkFile(channel: string, link: string): Promise<void> {
    try {
      return await this.axiosPost('chat.postMessage', {
        channel: `@${channel}`,
        avatar:
          'https://i.pinimg.com/originals/9a/11/33/9a1133d4af3b637e1c6c8ff251785f27.jpg',
        text: 'File report daily',
        alias: 'Giang hồ Part-time NCC',
        attachments: [
          {
            title: 'Mời bạn click vào đây download file report',
            title_link: link,
            author_icon:
              'https://image.flaticon.com/icons/png/512/17/17538.png',
            color: 'brown',
            author_name: 'REPORT',
          },
        ],
      });
    } catch (error) {
      throw error;
    }
  }
}
