import { ConfigService } from '@nestjs/config';
import { Injectable } from '@nestjs/common';
import { EValueType } from '../constants';
import { IBotConfig } from '../interfaces';
import camelcaseKeys from 'camelcase-keys';

@Injectable()
export class EnvService extends ConfigService {
  constructor() {
    super();
  }

  public getMany(keys: string[], types?: EValueType[]): any {
    try {
      if (keys.length === 0) {
        throw new Error('Invalid data');
      }
      const values = {};
      for (let i = 0; i < keys.length; i++) {
        let tmp = this.get(keys[i].toUpperCase());
        if (types?.length && types?.length !== 0) {
          switch (types[i]) {
            case EValueType.String:
              tmp = String(tmp);
              break;
            case EValueType.Number:
              tmp = Number(tmp);
              break;
            case EValueType.Boolean:
              tmp = Boolean(tmp);
              break;
            case EValueType.Int:
              tmp = parseInt(tmp, 10);
              break;
            default:
              tmp = String(tmp);
              break;
          }
        }
        values[keys[i]] = tmp || undefined;
      }
      return camelcaseKeys(values);
    } catch (error) {
      throw error;
    }
  }
  get isDebug(): boolean {
    return this.get<boolean>('DEBUG');
  }
  get isSendMail(): boolean {
    return this.get<boolean>('SEND_MAIL');
  }
  get salt(): number {
    return this.get<number>('SALT');
  }

  get bot(): IBotConfig {
    const {
      rocketchatUseSsl: ssl,
      rocketchatUrl: rootUrl,
      rocketchatHost: host,
      rocketchatUser: user,
      rocketchatPassword: pass,
      rocketchatBotName: botName,
      rocketchatRooms: rooms,
    } = this.getMany(
      [
        'ROCKETCHAT_USE_SSL',
        'ROCKETCHAT_URL',
        'ROCKETCHAT_HOST',
        'ROCKETCHAT_USER',
        'ROCKETCHAT_PASSWORD',
      ],
      [EValueType.Boolean],
    );
    return {
      rootUrl,
      ssl,
      host,
      user,
      pass,
      botName,
      rooms,
    };
  }
  get google() {
    const {
      googleDriveClientId: clientId,
      googleDriveClientSecret: clientSecret,
      googleDriveRedirectUri: redirectUri,
      googleDriveRefreshToken: refreshToken,
    } = this.getMany([
      'GOOGLE_DRIVE_CLIENT_ID',
      'GOOGLE_DRIVE_CLIENT_SECRET',
      'GOOGLE_DRIVE_REDIRECT_URI',
      'GOOGLE_DRIVE_REFRESH_TOKEN',
    ]);
    return {
      clientId,
      clientSecret,
      redirectUri,
      refreshToken,
    };
  }
}
