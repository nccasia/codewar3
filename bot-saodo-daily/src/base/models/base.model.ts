import { Prop, Schema } from '@nestjs/mongoose';
import { Types, Schema as MongooseSchema } from 'mongoose';
import { CSchemaOption } from '../constants';

@Schema(CSchemaOption)
export class Base {
  @Prop({ type: MongooseSchema.Types.ObjectId })
  _id: Types.ObjectId;

  @Prop({ type: Date, require: true, default: new Date() })
  createdAt?: Date;

  @Prop({ type: Date, require: true, default: new Date() })
  updatedAt?: Date;

  @Prop({ type: Date, require: false, default: null })
  deletedAt?: Date;

  @Prop({ type: MongooseSchema.Types.ObjectId, require: false, default: null })
  createdBy?: Types.ObjectId;

  @Prop({ type: MongooseSchema.Types.ObjectId, require: false, default: null })
  updatedBy?: Types.ObjectId;

  @Prop({ type: MongooseSchema.Types.ObjectId, require: false, default: null })
  deletedBy: Types.ObjectId;
}
