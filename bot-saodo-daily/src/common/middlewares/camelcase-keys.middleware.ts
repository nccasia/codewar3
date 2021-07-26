import { Injectable, NestMiddleware } from '@nestjs/common';
import { Request, Response, NextFunction } from 'express';
import camelcaseKeys from 'camelcase-keys';

@Injectable()
export class CamelcaseKeysMiddleware implements NestMiddleware {
  use(req: Request, res: Response, next: NextFunction) {
    req.body = camelcaseKeys(req.body, { deep: true });
    req.params = camelcaseKeys(req.params);
    req.query = camelcaseKeys(req.query);
    next();
  }
}
