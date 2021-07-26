import { transports, format } from 'winston';
import { utilities } from 'nest-winston';
import winstonDaily from 'winston-daily-rotate-file';
import { logDir } from './logger.const';
const { combine, timestamp, simple, ms } = format;

export const winstonTransport = [
  process.env.NODE_ENV === 'development'
    ? new transports.Console({
        format: combine(timestamp(), ms(), simple()),
      })
    : new transports.Console({
        format: combine(timestamp(), format.ms(), utilities.format.nestLike()),
      }),
  new winstonDaily({
    level: 'info',
    datePattern: 'YYYY-MM-DD',
    dirname: logDir + '/info',
    filename: `%DATE%.info.log`,
    maxFiles: 30,
    json: true,
    zippedArchive: true,
  }),
  new winstonDaily({
    level: 'error',
    datePattern: 'YYYY-MM-DD',
    dirname: logDir + '/error',
    filename: `%DATE%.error.log`,
    maxFiles: 30,
    handleExceptions: true,
    json: true,
    zippedArchive: true,
  }),
  new winstonDaily({
    level: 'http',
    datePattern: 'YYYY-MM-DD',
    dirname: logDir + '/http',
    filename: `%DATE%.http.log`,
    maxFiles: 30,
    handleExceptions: true,
    json: true,
    zippedArchive: true,
  }),
];
