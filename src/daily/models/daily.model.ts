import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { CSchemaOption } from 'src/base/constants';
import { Base } from 'src/base/models';
import { IDaily } from '../interfaces';
import { Document, Schema as MongooseSchema, Types } from 'mongoose';
import { ECronTime } from 'src/base/interfaces';

@Schema(CSchemaOption)
export class Daily extends Base implements IDaily {
  @Prop({ type: Date, default: null })
  time: Date;

  @Prop({
    type: MongooseSchema.Types.ObjectId,
    ref: 'Remote',
  })
  remoteId: Types.ObjectId;

  @Prop({
    type: MongooseSchema.Types.ObjectId,
    ref: 'Project',
  })
  projectId: Types.ObjectId;

  @Prop({ type: String })
  content: string;

  @Prop({ type: Boolean, default: false })
  noDaily: boolean;

  @Prop({ type: String, required: true, enum: Object.values(ECronTime) })
  timeRemote: ECronTime;
}

export type DailyDocument = Daily & Document;
export const DailySchema = SchemaFactory.createForClass(Daily);
