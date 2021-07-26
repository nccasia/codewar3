import { Global, Module } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';
import { mongoConfigProvider } from './database.provider';

@Global()
@Module({
  imports: [MongooseModule.forRootAsync(mongoConfigProvider)],
})
export class DatabaseModule {}
