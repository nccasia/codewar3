import { format } from 'winston';
const { printf, combine, json, timestamp, simple, colorize, ms } = format;

const logFormat = printf(
  ({ timestamp, level, message }) => `${timestamp} ${level}: ${message}`,
);

export function getFormat() {
  if (process.env.NODE_ENV === 'production')
    return combine(
      timestamp({
        format: 'YYYY-MM-DD HH:mm:ss',
      }),
      json(),
      ms(),
      logFormat,
    );
  return combine(
    timestamp({
      format: 'YYYY-MM-DD HH:mm:ss',
    }),
    logFormat,
    colorize(),
    ms(),
    simple(),
  );
}
