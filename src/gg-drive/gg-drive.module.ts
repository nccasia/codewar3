import { Module } from '@nestjs/common';
import { GGDriveService } from './services';

@Module({
  providers: [GGDriveService],
  exports: [GGDriveService],
})
export class GGDriveModule {}
