import { Test, TestingModule } from '@nestjs/testing';
import { BaseService } from '../services';
import { Document, Model } from 'mongoose';

type T = Document;
type R = Model<T>;

describe('BaseService', () => {
  let baseService: BaseService<T, R>;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [BaseService],
    }).compile();
    baseService = module.get<BaseService<T, R>>(BaseService);
  });

  describe('define', () => {
    it('should be defined', () => {
      expect(baseService).toBeDefined();
    });
  });
});
