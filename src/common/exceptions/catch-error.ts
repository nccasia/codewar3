import {
  BadGatewayException,
  BadRequestException,
  ConflictException,
  ForbiddenException,
  GoneException,
  HttpStatus,
  HttpVersionNotSupportedException,
  ImATeapotException,
  InternalServerErrorException,
  MethodNotAllowedException,
  NotAcceptableException,
  NotFoundException,
  NotImplementedException,
  PayloadTooLargeException,
  PreconditionFailedException,
  RequestTimeoutException,
  UnauthorizedException,
  UnprocessableEntityException,
  UnsupportedMediaTypeException,
} from '@nestjs/common';

export const catchError = (error: any) => {
  const message = error.message || 'Server error!';
  switch (error.status) {
    case HttpStatus.BAD_REQUEST:
      throw new BadRequestException(message);

    case HttpStatus.UNAUTHORIZED:
      throw new UnauthorizedException(message);

    case HttpStatus.FORBIDDEN:
      throw new ForbiddenException(message);

    case HttpStatus.NOT_FOUND:
      throw new NotFoundException(message);

    case HttpStatus.NOT_ACCEPTABLE:
      throw new NotAcceptableException(message);

    case HttpStatus.REQUEST_TIMEOUT:
      throw new RequestTimeoutException(message);

    case HttpStatus.CONFLICT:
      throw new ConflictException(message);

    case HttpStatus.GONE:
      throw new GoneException(message);

    case HttpStatus.HTTP_VERSION_NOT_SUPPORTED:
      throw new HttpVersionNotSupportedException(message);

    case HttpStatus.PAYLOAD_TOO_LARGE:
      throw new PayloadTooLargeException(message);

    case HttpStatus.UNSUPPORTED_MEDIA_TYPE:
      throw new UnsupportedMediaTypeException(message);

    case HttpStatus.UNPROCESSABLE_ENTITY:
      throw new UnprocessableEntityException(message);

    case HttpStatus.INTERNAL_SERVER_ERROR:
      throw new InternalServerErrorException(message);

    case HttpStatus.NOT_IMPLEMENTED:
      throw new NotImplementedException(message);

    case HttpStatus.I_AM_A_TEAPOT:
      throw new ImATeapotException(message);

    case HttpStatus.METHOD_NOT_ALLOWED:
      throw new MethodNotAllowedException(message);

    case HttpStatus.BAD_GATEWAY:
      throw new BadGatewayException(message);

    case HttpStatus.PRECONDITION_FAILED:
      throw new PreconditionFailedException(message);

    default:
      throw new Error(message);
  }
};
