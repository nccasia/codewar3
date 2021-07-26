import { format } from 'winston';
import { WinstonModule } from 'nest-winston';
import { winstonTransport } from './logger.transports';
import { getFormat } from './logger.format';

const { combine, timestamp } = format;

export const AppLogger = WinstonModule.createLogger({
  format: combine(
    timestamp({
      format: 'YYYY-MM-DD HH:mm:ss',
    }),
    getFormat(),
  ),
  transports: [...winstonTransport],
});

export const stream = {
  write: (message: string) => {
    AppLogger.log(message.substring(0, message.lastIndexOf('\n')));
  },
};
