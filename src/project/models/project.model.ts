import { Prop, Schema, SchemaFactory } from '@nestjs/mongoose';
import { Document } from 'mongoose';
import { CSchemaOption } from 'src/base/constants';
import { Base } from 'src/base/models';
import { IProject, EProjectType } from '../interfaces';

@Schema(CSchemaOption)
export class Project extends Base implements IProject {
  @Prop({ type: String, required: true, unique: true })
  name: string;

  @Prop({ type: String, required: true, unique: true })
  projectKomuId: string;

  @Prop({
    type: String,
    enum: Object.values(EProjectType),
    default: EProjectType.Team,
  })
  type: EProjectType;
}

export type ProjectDocument = Project & Document;
export const ProjectSchema = SchemaFactory.createForClass(Project);
