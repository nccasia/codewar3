import { Module } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';
import { Remote, RemoteSchema } from './models';
import { RemoteService } from './services';

@Module({
  imports: [
    MongooseModule.forFeature([{ name: Remote.name, schema: RemoteSchema }]),
  ],
  providers: [RemoteService],
  exports: [RemoteService],
})
export class RemoteModule {}
