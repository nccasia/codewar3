import { Injectable, NestMiddleware } from '@nestjs/common';
import { Request, Response, NextFunction } from 'express';
import omitEmpty from 'omit-empty';

@Injectable()
export class OmitEmptyMiddleware implements NestMiddleware {
  use(req: Request, res: Response, next: NextFunction) {
    req.body = omitEmpty(req.body);
    req.params = omitEmpty(req.params) as any;
    req.query = omitEmpty(req.query) as any;
    next();
  }
}
