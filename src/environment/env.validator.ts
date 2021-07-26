import joi from 'joi';
import { CEnvironment } from './constants';

export const envValidate = joi
  .object()
  .keys({
    NODE_ENV: joi
      .string()
      .valid(...CEnvironment)
      .default(CEnvironment[0]),
    // url and port
    PORT: joi.number().default(8080),
    URL: joi
      .string()
      .uri({ scheme: [/https?/] })
      .default('https://localhost'),
    // database
    MONGO_URI: joi
      .string()
      .regex(/^mongodb/)
      .default('mongodb://localhost:27017/Project2'),
    // bcrypt salt
    SALT: joi.number().min(4).max(15).default(10),
    // jwt config
    USER_SECRET: joi
      .string()
      .regex(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/)
      .min(10)
      .required(),
    // cluster config
    CLUSTER_ENABLED: joi.boolean().default(false),
  })
  .unknown(true);
