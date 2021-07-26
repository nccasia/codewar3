import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { Types, Schema as MongooseSchema, Document } from 'mongoose';
import { CSchemaOption } from 'src/base/constants';
import { ERemoteTime } from 'src/base/interfaces';
import { Base } from 'src/base/models';
import { IRemote } from '../interfaces';

@Schema(CSchemaOption)
export class Remote extends Base implements IRemote {
  @Prop({ type: String, required: true, immutable: true, unique: true })
  username: string;

  @Prop({ type: String, required: true, immutable: true })
  name: string;

  @Prop({ type: Date, required: true, immutable: true })
  remoteDate: Date;

  @Prop({
    type: MongooseSchema.Types.ObjectId,
    required: true,
    immutable: true,
    ref: 'Project',
  })
  projectId: Types.ObjectId;

  @Prop({
    type: String,
    required: true,
    enum: ERemoteTime,
    immutable: true,
    default: ERemoteTime.AllDay,
  })
  remoteTime: ERemoteTime;

  @Prop({
    type: MongooseSchema.Types.ObjectId,
    required: false,
    immutable: true,
    ref: 'Daily',
  })
  dailyId?: Types.ObjectId;
}

export type RemoteDocument = Remote & Document;
export const RemoteSchema = SchemaFactory.createForClass(Remote);
