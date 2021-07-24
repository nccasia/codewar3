import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { Model, Types } from 'mongoose';
import { BaseService } from 'src/base/services';
import { DailyCreateDTO } from '../dtos';
import { DailyDocument, Daily } from '../models';
import moment from 'moment';

@Injectable()
export class DailyService extends BaseService<
  DailyDocument,
  Model<DailyDocument>
> {
  constructor(
    @InjectModel(Daily.name) private readonly dailyModel: Model<DailyDocument>,
  ) {
    super(dailyModel);
  }

  public async create(daily: DailyCreateDTO): Promise<DailyDocument> {
    try {
      return await this.dailyModel.create({ _id: Types.ObjectId(), ...daily });
    } catch (error) {
      throw error;
    }
  }

  public async findAllDailyOfDay(): Promise<DailyDocument[]> {
    try {
      return await this.dailyModel
        .find({
          createdAt: {
            $gte: moment().subtract(1, 'days').startOf('day').toDate(),
          },
          noDaily: { $ne: true },
        })
        .populate({
          path: 'remoteId',
          model: 'Remote',
        })
        .populate({ path: 'projectId', model: 'Project' });
    } catch (error) {
      throw error;
    }
  }

  public async findAllNoDaiLyOfDay(): Promise<DailyDocument[]> {
    try {
      return await this.dailyModel
        .find({
          noDaily: true,
          createdAt: {
            $gte: moment().subtract(1, 'days').startOf('day').toDate(),
          },
        })
        .populate({
          path: 'remoteId',
          model: 'Remote',
        })
        .populate({ path: 'projectId', model: 'Project' });
    } catch (error) {
      throw error;
    }
  }
}
