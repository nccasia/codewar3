import { Module } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';
import { Inspector, InspectorSchema } from './models';
import { InspectorService } from './services';

@Module({
  imports: [
    MongooseModule.forFeature([
      { name: Inspector.name, schema: InspectorSchema },
    ]),
  ],
  providers: [InspectorService],
  exports: [InspectorService],
})
export class InspectorModule {}
