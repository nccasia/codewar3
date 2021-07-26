import { envValidate } from './env.validator';

export const envConfigProvider = {
  cache: true,
  isGlobal: true,
  envFilePath: '.env',
  validationSchema: envValidate,
};
