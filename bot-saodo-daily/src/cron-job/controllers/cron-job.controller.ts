import { Controller, Get } from '@nestjs/common';
import { ApiTags } from '@nestjs/swagger';
import { ERemoteTime } from 'src/base/interfaces';
import { InspectorService } from 'src/inspector/services';
import { ProjectService } from 'src/project/services';
import { RemoteService } from 'src/remote/services';
import { RocketChatService } from 'src/rocket-chat/services';
import { CronJobService } from '../services';

@Controller('test')
@ApiTags('test')
export class CronJobController {
  constructor(
    private readonly cronJobService: CronJobService,
    private readonly rocketChatService: RocketChatService,
    private readonly projectService: ProjectService,
    private readonly remoteService: RemoteService,
    private readonly inspectorService: InspectorService,
  ) {}

  @Get('morning')
  public async dailyMorning(): Promise<void> {
    try {
      await this.cronJobService.cronDailyMorning();
    } catch (error) {
      throw error;
    }
  }

  @Get('afternoon')
  public async dailyAfternoon(): Promise<void> {
    try {
      await this.cronJobService.cronDailyAfternoon();
    } catch (error) {
      throw error;
    }
  }

  @Get('punish_morning')
  public async morningPunish(): Promise<void> {
    try {
      await this.cronJobService.cronCheckPunishMorning();
    } catch (error) {
      throw error;
    }
  }

  @Get('punish_afternoon')
  public async afternoonPunish(): Promise<void> {
    try {
      await this.cronJobService.cronCheckPunishAfternoon();
    } catch (error) {
      throw error;
    }
  }

  @Get('csv')
  public async exportCSV(): Promise<void> {
    try {
      return await this.cronJobService.sendCSVDaily();
    } catch (error) {
      throw error;
    }
  }

  @Get('seed')
  public async seedData(): Promise<void> {
    try {
      await this.remoteService.clearAll();
      await this.remoteService.create({
        username: 'Vy.nguyen.van',
        name: 'Nguyen Van Vy',
        remoteDate: Date().toString(),
        projectId: '60faf9335b3d9542bc0afc6a',
        remoteTime: ERemoteTime.AllDay,
      });
      await this.remoteService.create({
        username: 'thuy.nguyenxuan',
        name: 'Nguyen Xuan Thuy',
        remoteDate: Date().toString(),
        projectId: '60faf9325b3d9542bc0afc67',
        remoteTime: ERemoteTime.Morning,
      });
      await this.remoteService.create({
        username: 'thao.vuthiphuong',
        name: 'Vu Thi Phuong Thao',
        remoteDate: Date().toString(),
        projectId: '60faf9335b3d9542bc0afc6d',
        remoteTime: ERemoteTime.Afternoon,
      });
      await this.inspectorService.clearAll();
      await this.inspectorService.create({
        name: 'name',
        username: 'username',
      });
      return;
    } catch (error) {
      throw error;
    }
  }

  @Get('sync-data')
  public async getData() {
    try {
      await this.rocketChatService.updateListChanelJoined();
      await this.rocketChatService.updateListTeamJoined();
      return { status: 'OK' };
    } catch (error) {
      throw error;
    }
  }

  @Get('all-project')
  public async findAll() {
    try {
      return await this.projectService.findAll();
    } catch (error) {
      throw error;
    }
  }

  @Get('clear')
  public async clear() {
    try {
      return await this.projectService.clearAll();
    } catch (error) {
      throw error;
    }
  }

  @Get('search')
  public async searchMessage() {
    try {
      return await this.rocketChatService.searchMessage('YbBWKWNNY98rHSYo2');
    } catch (error) {
      throw error;
    }
  }
}
