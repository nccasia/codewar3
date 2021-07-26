import { NestFactory } from '@nestjs/core';
import morgan from 'morgan';
import { INestApplication, ValidationPipe } from '@nestjs/common';
import express from 'express';
import helmet from 'helmet';
import path from 'path';
import favicon from 'serve-favicon';
import { SwaggerModule, DocumentBuilder } from '@nestjs/swagger';
import { AppLogger, stream } from './common/log';
import { AppModule } from './app.module';
import { TimeoutInterceptor } from './common/interceptors';
import fs from 'fs';
import { runInCluster } from './cluster';
import { configService } from './environment';

async function bootstrap() {
  const app = await NestFactory.create(AppModule, { logger: AppLogger });
  initMiddleware(app);
  initGlobal(app);
  initSwagger(app);
  const port = configService.port;
  await app.listen(port, () => log());
}

function initMiddleware(app: INestApplication) {
  app.use(
    morgan(' :method :url :status :res[content-length] - :response-time ms', {
      stream: stream,
    }),
  );
  app.enableCors();
  app.use(helmet());
  app.use(express.json());
  app.use(express.urlencoded({ extended: true, limit: '2mb' }));
  app.use(favicon(path.join(__dirname, '../public/favicon.ico')));
}

function initGlobal(app: INestApplication) {
  app.useGlobalPipes(new ValidationPipe({ transform: true }));
  // app.useGlobalInterceptors(new TimeoutInterceptor());
}

function initSwagger(app: INestApplication) {
  const { swagger } = configService;
  if (swagger.enable) {
    const config = new DocumentBuilder()
      .addBearerAuth(
        {
          type: 'http',
          name: 'user',
          in: 'headers',
          description: 'Auth for account auth',
        },
        'user',
      )
      .addBearerAuth(
        {
          type: 'http',
          name: 'device',
          in: 'headers',
          description: 'Auth for device auth',
        },
        'device',
      )
      .setTitle('Mesthing API')
      .setContact(
        'Nguyen Van Vy',
        'https://github.com/nguyenvanvy1999',
        'nguyenvanvy1999@gmail.com',
      )
      .setDescription('Resort Management API')
      .setVersion('1.1')
      .setLicense('MIT', 'https://opensource.org/licenses/MIT')
      .build();
    const document = SwaggerModule.createDocument(app, config);
    if (swagger.write) {
      fs.writeFileSync('./swagger-spec.json', JSON.stringify(document));
    }
    SwaggerModule.setup('apis', app, document);
  }
}

function log() {
  const { port, host, nodeEnv, swagger } = configService;
  console.log('');
  console.log('');
  console.log('');
  console.log(`-------------------------------------------------------`);
  console.log(`Server        : ${host}:${port}`);
  console.log(`Environment   : ${nodeEnv}`);
  swagger.enable
    ? console.log(`Swagger       : ${host}:${port}/apis`)
    : console.log('Swagger       : Disable');
  console.log(`-------------------------------------------------------`);
  console.log('');
}

if (configService.cluster) {
  runInCluster(bootstrap);
} else {
  bootstrap();
}
