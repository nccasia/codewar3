import { Global, Module } from '@nestjs/common';
import { EnvService } from './services';
import { ConfigModule, ConfigService } from '@nestjs/config';
import { envConfigProvider } from './env.provider';

@Global()
@Module({
  imports: [ConfigModule.forRoot(envConfigProvider)],
  providers: [EnvService, ConfigService],
  exports: [EnvService, ConfigService],
})
export class EnvModule {}
