import { ApiProperty } from '@nestjs/swagger';

export class ErrorResSW {
  @ApiProperty({
    description: 'Status code',
    type: Number,
    required: true,
    default: 500,
  })
  readonly status: number;

  @ApiProperty({
    description: 'Timestamp',
    type: String,
    required: true,
    format: 'date-time',
    default: new Date().toLocaleString(),
  })
  readonly timestamp: string;

  @ApiProperty({
    description: 'Method of req',
    required: true,
    type: String,
    enum: ['POST', 'GET', 'PATCH', 'PUT', 'DELETE'],
  })
  readonly method: string;

  @ApiProperty({ description: 'Path of req', required: true, type: String })
  readonly path: string;

  @ApiProperty({ description: 'Message', required: true })
  readonly message: any;
}
