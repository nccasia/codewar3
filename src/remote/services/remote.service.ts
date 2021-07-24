import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { Model, Types } from 'mongoose';
import { BaseService } from 'src/base/services';
import { CreateRemoteDTO } from '../dtos';
import { Remote, RemoteDocument } from '../models';
import moment from 'moment';
import { ERemoteTime } from 'src/base/interfaces';

@Injectable()
export class RemoteService extends BaseService<
  RemoteDocument,
  Model<RemoteDocument>
> {
  constructor(
    @InjectModel(Remote.name)
    private readonly remoteModel: Model<RemoteDocument>,
  ) {
    super(remoteModel);
  }

  public async create(remote: CreateRemoteDTO): Promise<RemoteDocument> {
    try {
      return await this.remoteModel.create({
        _id: Types.ObjectId(),
        ...remote,
      });
    } catch (error) {
      throw error;
    }
  }

  public async findAllRemoteMorningAndAllDay(): Promise<RemoteDocument[]> {
    try {
      return await this.remoteModel
        .find({
          remoteDate: {
            $gte: moment().set({ hour: 0, minute: 0, second: 0 }).toDate(),
          },
          remoteTime: {
            $in: [ERemoteTime.Morning, ERemoteTime.AllDay],
          },
        })
        .populate({
          path: 'projectId',
          model: 'Project',
        })
        .lean();
    } catch (error) {
      throw error;
    }
  }

  public async findAllRemoteAfternoonAndAllDay(): Promise<RemoteDocument[]> {
    try {
      return await this.remoteModel
        .find({
          remoteDate: {
            $gte: moment().set({ hour: 0, minute: 0, second: 0 }).toDate(),
          },
          remoteTime: {
            $in: [ERemoteTime.Afternoon, ERemoteTime.AllDay],
          },
        })
        .populate({ path: 'projectId', model: 'Project' })
        .lean();
    } catch (error) {
      throw error;
    }
  }
}
