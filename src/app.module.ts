import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { DatabaseModule } from './database/database.module';
import { EnvModule } from './environment/env.module';
import { ProjectModule } from './project/project.module';
import { RocketChatModule } from './rocket-chat/rocket-chat.module';
import { ScheduleModule } from '@nestjs/schedule';
import { RemoteModule } from './remote/remote.module';
import { DailyModule } from './daily/daily.module';
import { CronJobModule } from './cron-job/cron-job.module';
import { InspectorModule } from './inspector/inspector.module';

@Module({
  imports: [
    EnvModule,
    DatabaseModule,
    ProjectModule,
    RocketChatModule,
    RemoteModule,
    DailyModule,
    CronJobModule,
    InspectorModule,
    ScheduleModule.forRoot(),
  ],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
