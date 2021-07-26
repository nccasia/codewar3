import { Module } from '@nestjs/common';
import { GGDriveService, S3Service } from './services';

@Module({
  providers: [GGDriveService, S3Service],
  exports: [GGDriveService, S3Service],
})
export class GGDriveModule {}
