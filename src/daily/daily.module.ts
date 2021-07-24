import { Module } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';
import { Daily, DailySchema } from './models';
import { DailyService } from './services';

@Module({
  imports: [
    MongooseModule.forFeature([{ name: Daily.name, schema: DailySchema }]),
  ],
  providers: [DailyService],
  exports: [DailyService],
})
export class DailyModule {}
