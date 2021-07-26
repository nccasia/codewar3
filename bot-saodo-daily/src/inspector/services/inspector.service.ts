import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { Model, Types } from 'mongoose';
import { BaseService } from 'src/base/services';
import { Inspector, InspectorDocument } from '../models';

@Injectable()
export class InspectorService extends BaseService<
  InspectorDocument,
  Model<InspectorDocument>
> {
  constructor(
    @InjectModel(Inspector.name)
    private readonly inspectorModel: Model<InspectorDocument>,
  ) {
    super(inspectorModel);
  }

  public async create(data: {
    name: string;
    username: string;
  }): Promise<InspectorDocument> {
    try {
      return await this.inspectorModel.create({
        _id: Types.ObjectId,
        ...data,
      });
    } catch (error) {
      throw error;
    }
  }
}
