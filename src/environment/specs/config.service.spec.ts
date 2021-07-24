import { Test, TestingModule } from '@nestjs/testing';
import { ConfigService } from '../services';

describe('ConfigService', () => {
  let configService: ConfigService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [ConfigService],
    }).compile();
    configService = module.get<ConfigService>(ConfigService);
  });

  describe('define', () => {
    it('should be defined', () => {
      expect(configService).toBeDefined();
    });
  });
});
