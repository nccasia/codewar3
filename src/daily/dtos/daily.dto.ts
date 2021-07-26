import { OmitType } from '@nestjs/swagger';
import { IsEnum, IsNotEmpty, IsString } from 'class-validator';
import { ERemoteTime } from 'src/base/interfaces';
import { EProjectType } from 'src/project/interfaces';
import { DailyCreateDTO } from './create.dto';

export class DailyDTO extends OmitType(DailyCreateDTO, ['timeRemote']) {
  @IsString()
  @IsNotEmpty()
  username: string;

  @IsString()
  @IsNotEmpty()
  teamName: string;

  @IsEnum(EProjectType)
  @IsNotEmpty()
  type: EProjectType;

  @IsEnum(ERemoteTime)
  @IsNotEmpty()
  timeRemote: ERemoteTime;
}
