import {
  IsDateString,
  IsEnum,
  IsMongoId,
  IsNotEmpty,
  IsString,
} from 'class-validator';
import { ERemoteTime } from 'src/base/interfaces';

export class CreateRemoteDTO {
  @IsString()
  @IsNotEmpty()
  username: string;

  @IsString()
  @IsNotEmpty()
  name: string;

  @IsDateString()
  @IsNotEmpty()
  remoteDate: string;

  @IsMongoId()
  @IsNotEmpty()
  projectId: string;

  @IsEnum(ERemoteTime)
  @IsNotEmpty()
  remoteTime: ERemoteTime;
}
