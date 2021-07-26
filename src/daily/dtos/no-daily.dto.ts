import { IsEnum, IsMongoId, IsNotEmpty, IsString } from 'class-validator';
import { ERemoteTime } from 'src/base/interfaces';
import { EProjectType } from 'src/project/interfaces';

export class NoDailyDTO {
  @IsString()
  @IsNotEmpty()
  remoteId: string;

  @IsString()
  @IsNotEmpty()
  username: string;

  @IsString()
  @IsNotEmpty()
  teamName: string;

  @IsMongoId()
  @IsNotEmpty()
  projectId: string;

  @IsString()
  @IsNotEmpty()
  name: string;

  @IsEnum(EProjectType)
  @IsNotEmpty()
  type: EProjectType;

  @IsEnum(ERemoteTime)
  @IsNotEmpty()
  timeRemote: ERemoteTime;
}
