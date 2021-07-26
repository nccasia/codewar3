import {
  IsBoolean,
  IsDateString,
  IsEnum,
  IsMongoId,
  IsString,
} from 'class-validator';
import { ERemoteTime } from 'src/base/interfaces';

export class DailyCreateDTO {
  @IsMongoId()
  remoteId?: string;

  @IsMongoId()
  projectId?: string;

  @IsString()
  content?: string;

  @IsDateString()
  time?: string;

  @IsBoolean()
  noDaily?: boolean;

  @IsEnum(ERemoteTime)
  timeRemote: ERemoteTime;
}
