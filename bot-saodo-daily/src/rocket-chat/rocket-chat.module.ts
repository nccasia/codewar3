import { Module } from '@nestjs/common';
import { ProjectModule } from 'src/project/project.module';
import { RocketChatService } from './services';

@Module({
  imports: [ProjectModule],
  providers: [RocketChatService],
  exports: [RocketChatService],
})
export class RocketChatModule {}
