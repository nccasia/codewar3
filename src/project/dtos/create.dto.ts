import { IsEnum, IsNotEmpty, IsString } from 'class-validator';
import { EProjectType } from '../interfaces/enums';

export class ProjectCreateDTO {
  @IsString()
  @IsNotEmpty()
  name: string;

  @IsString()
  @IsNotEmpty()
  projectKomuId: string;

  @IsEnum(EProjectType)
  type: EProjectType;
}
