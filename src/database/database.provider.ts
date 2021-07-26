import { EnvService } from 'src/environment';
import { EnvModule } from 'src/environment/env.module';

export const mongoConfigProvider = {
  imports: [EnvModule],
  useFactory: (config: EnvService) => {
    const {
      mongoUri: uri,
      mongoUsername: user,
      mongoPassword: pass,
    } = config.getMany(['mongo_uri', 'mongo_username', 'mongo_password']);
    return {
      uri,
      useNewUrlParser: true,
      useUnifiedTopology: true,
      useFindAndModify: false,
      useCreateIndex: true,
      user,
      pass,
    };
  },
  inject: [EnvService],
};
