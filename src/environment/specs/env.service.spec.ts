import { Test, TestingModule } from '@nestjs/testing';
import { EnvService } from '../services';

describe('EnvService', () => {
  let envService: EnvService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [EnvService],
    }).compile();
    envService = module.get<EnvService>(EnvService);
  });

  describe('define', () => {
    it('should be defined', () => {
      expect(envService).toBeDefined();
    });
  });
});
