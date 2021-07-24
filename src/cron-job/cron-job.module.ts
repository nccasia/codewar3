import { Module } from '@nestjs/common';
import { DailyModule } from 'src/daily/daily.module';
import { GGDriveModule } from 'src/gg-drive/gg-drive.module';
import { InspectorModule } from 'src/inspector/inspector.module';
import { ProjectModule } from 'src/project/project.module';
import { RemoteModule } from 'src/remote/remote.module';
import { RocketChatModule } from 'src/rocket-chat/rocket-chat.module';
import { CronJobController } from './controllers';
import { CronJobService } from './services';

@Module({
  imports: [
    RocketChatModule,
    DailyModule,
    ProjectModule,
    RemoteModule,
    InspectorModule,
    GGDriveModule,
  ],
  providers: [CronJobService],
  controllers: [CronJobController],
  exports: [CronJobService],
})
export class CronJobModule {}
