import {
  ExceptionFilter,
  Catch,
  ArgumentsHost,
  HttpException,
  HttpStatus,
} from '@nestjs/common';
import { Response, Request } from 'express';
import { AppLogger } from '../log';
import { ErrorResSW } from './dtos';
@Catch()
export class HttpExceptionsFilter implements ExceptionFilter {
  catch(exception: unknown, host: ArgumentsHost) {
    AppLogger.error(exception);
    const ctx = host.switchToHttp();
    const response = ctx.getResponse<Response>();
    const request = ctx.getRequest<Request>();
    const status =
      exception instanceof HttpException
        ? exception.getStatus()
        : HttpStatus.INTERNAL_SERVER_ERROR;
    let message: any =
      exception instanceof HttpException
        ? exception.getResponse()
        : 'Internal server';
    message =
      message instanceof Object ? message.message || 'Unknown Error' : message;
    if (message instanceof Array) {
      for (let i = 0; i < message.length; i++) {
        if (message[i].split(' ')[0].includes('.')) {
          const customMessage = message[i].split(' ');
          customMessage[0] = customMessage[0].split('.')[2];
          message[i] = customMessage.join(' ');
        }
      }
    }
    const res: ErrorResSW = {
      status,
      timestamp: new Date().toLocaleString(),
      method: request.method,
      path: request.url,
      message,
    };
    return response.status(status).json(res);
  }
}
