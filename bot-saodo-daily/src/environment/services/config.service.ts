import 'dotenv/config';

interface IJwtConfig {
  secret: string;
  signOptions: {
    expiresIn: string;
  };
}

interface ISwaggerConfig {
  enable: boolean;
  write: boolean;
}

export class ConfigService {
  constructor(private env: { [k: string]: string | undefined }) {}
  private getValue(key: string, throwOnMissing = true): string {
    const value = this.env[key];
    if (!value && throwOnMissing) {
      throw new Error(`config error - missing env.${key}`);
    }
    return value;
  }

  public ensureValues(keys: string[]) {
    keys.forEach((k) => this.getValue(k, true));
    return this;
  }

  get host(): string {
    return this.getValue('HOST') || 'localhost';
  }

  get port(): number {
    return parseInt(this.getValue('PORT')) || 3000;
  }

  get nodeEnv(): string {
    return this.getValue('NODE_ENV') || 'development';
  }

  get swagger(): ISwaggerConfig {
    return {
      enable: this.getValue('SWAGGER_ENABLE') === 'true',
      write: this.getValue('SWAGGER_WRITE') === 'true',
    };
  }

  public isDevelopment(): boolean {
    return this.getValue('NODE_ENV', false) === 'development';
  }

  get salt(): number {
    return parseInt(this.getValue('SALT')) || 8;
  }
  get userSecret(): string {
    return this.getValue('USER_SECRET');
  }
  get cluster(): boolean {
    return this.getValue('CLUSTER_ENABLE') === 'true';
  }

  public isProduction(): boolean {
    return this.getValue('NODE_ENV', false) !== 'development';
  }

  public getJwtConfig(): IJwtConfig {
    return {
      secret: this.getValue('JWT_SECRET'),
      signOptions: {
        expiresIn: `${this.getValue('JWT_EXPIRATION_TIME')}s` || '60s',
      },
    };
  }

  public getClientUrl(): string {
    return this.getValue('CLIENT_URL');
  }

  public getAdminPassword(): string {
    return this.getValue('ADMIN_PASSWORD');
  }
}

const configService = new ConfigService(process.env).ensureValues([
  'HOST',
  'PORT',
]);

export { configService };
