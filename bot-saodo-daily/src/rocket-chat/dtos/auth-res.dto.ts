import { IsNotEmpty, IsString } from 'class-validator';

export class AuthResDTO {
  @IsString()
  @IsNotEmpty()
  token: string;

  @IsString()
  @IsNotEmpty()
  userId: string;
}
