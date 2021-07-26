import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { CSchemaOption } from 'src/base/constants';
import { Base } from 'src/base/models';
import { IInspector } from '../interfaces';
import { Document } from 'mongoose';

@Schema(CSchemaOption)
export class Inspector extends Base implements IInspector {
  @Prop({ type: String, required: true })
  name: string;

  @Prop({ type: String, required: true, immutable: true })
  username: string;
}

export type InspectorDocument = Inspector & Document;
export const InspectorSchema = SchemaFactory.createForClass(Inspector);
