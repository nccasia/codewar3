import { Injectable } from '@nestjs/common';
import { Document, FilterQuery, Model, UpdateQuery } from 'mongoose';
import { IBaseService } from './interfaces';

@Injectable()
export class BaseService<T extends Document, R extends Model<T>>
  implements IBaseService<T>
{
  constructor(private readonly model: R) {}

  public async findById(_id: string): Promise<T> {
    try {
      return this.model.findById(_id);
    } catch (error) {
      throw error;
    }
  }

  public async findByIdLean(_id: string): Promise<T> {
    try {
      return this.model.findById(_id).lean();
    } catch (error) {
      throw error;
    }
  }

  public async findByIds(_ids: []): Promise<T[]> {
    try {
      return await this.model.find({ _id: { $in: _ids } });
    } catch (error) {
      throw error;
    }
  }

  public async findByIdsLean(_ids: []): Promise<T[]> {
    try {
      return await this.model.find({ _id: { $in: _ids } }).lean();
    } catch (error) {
      throw error;
    }
  }
  public async findAll(): Promise<T[]> {
    try {
      return await this.model.find();
    } catch (error) {
      throw error;
    }
  }

  public async findAllLean(): Promise<T[]> {
    try {
      return await this.model.find().lean();
    } catch (error) {
      throw error;
    }
  }

  public async findOne(filter: FilterQuery<T>): Promise<T> {
    try {
      return await this.model.findOne({ ...filter });
    } catch (error) {
      throw error;
    }
  }

  public async findOneLean(filter: FilterQuery<T>): Promise<T> {
    try {
      return await this.model.findOne({ ...filter }).lean();
    } catch (error) {
      throw error;
    }
  }

  public async findMany(filter: FilterQuery<T>): Promise<T[]> {
    try {
      return await this.model.find({ ...filter });
    } catch (error) {
      throw error;
    }
  }

  public async findManyLean(filter: FilterQuery<T>): Promise<T[]> {
    try {
      return await this.model.find({ ...filter }).lean();
    } catch (error) {
      throw error;
    }
  }

  public async deleteById(_id: string): Promise<T> {
    try {
      return await this.model.findByIdAndDelete(_id);
    } catch (error) {
      throw error;
    }
  }

  public async deleteManyByIds(_ids: []): Promise<any> {
    try {
      return await this.model.deleteMany({ _id: { $in: _ids } });
    } catch (error) {
      throw error;
    }
  }

  public async clearAll(): Promise<any> {
    try {
      return await this.model.deleteMany();
    } catch (error) {
      throw error;
    }
  }

  public async checkExitsById(_id: any): Promise<boolean> {
    try {
      return await this.model.exists({ _id });
    } catch (error) {
      throw error;
    }
  }

  public async checkExitsManyByIds(_ids: []): Promise<boolean> {
    try {
      return await this.model.exists({ _id: { $in: _ids } });
    } catch (error) {
      throw error;
    }
  }

  public async checkExits(filter: FilterQuery<T>): Promise<boolean> {
    try {
      return await this.model.exists({ ...filter });
    } catch (error) {
      throw error;
    }
  }

  public async deleteOne(filter: FilterQuery<T>): Promise<T> {
    try {
      return await this.model.findOneAndDelete({ ...filter });
    } catch (error) {
      throw error;
    }
  }

  public async deleteMany(filter: FilterQuery<T>): Promise<any> {
    try {
      return await this.model.deleteMany({ ...filter });
    } catch (error) {
      throw error;
    }
  }

  public async findOneAndUpdateOrInsert(
    filter: FilterQuery<T>,
    query: UpdateQuery<T>,
  ): Promise<T> {
    try {
      return await this.model.findOneAndUpdate(
        { ...filter },
        { ...query },
        { new: true, upsert: true, setDefaultsOnInsert: true },
      );
    } catch (error) {
      throw error;
    }
  }

  public async findOneAndUpdateOrInsertLean(
    filter: FilterQuery<T>,
    query: UpdateQuery<T>,
  ): Promise<T> {
    try {
      return await this.model
        .findOneAndUpdate(
          { ...filter },
          { ...query },
          { new: true, upsert: true, setDefaultsOnInsert: true },
        )
        .lean();
    } catch (error) {
      throw error;
    }
  }

  public async findOneAndUpdate(
    filter: FilterQuery<T>,
    query: UpdateQuery<T>,
  ): Promise<T> {
    try {
      return await this.model.findOneAndUpdate(
        { ...filter },
        { ...query },
        { new: true },
      );
    } catch (error) {
      throw error;
    }
  }

  public async findOneAndUpdateLean(
    filter: FilterQuery<T>,
    query: UpdateQuery<T>,
  ): Promise<T> {
    try {
      return await this.model
        .findOneAndUpdate({ ...filter }, { ...query }, { new: true })
        .lean();
    } catch (error) {
      throw error;
    }
  }

  public async updateOne(
    filter: FilterQuery<T>,
    query: UpdateQuery<T>,
  ): Promise<any> {
    try {
      return await this.model.updateOne({ ...filter }, { ...query });
    } catch (error) {
      throw error;
    }
  }

  public async updateOneLean(
    filter: FilterQuery<T>,
    query: UpdateQuery<T>,
  ): Promise<any> {
    try {
      return await this.model.updateOne({ ...filter }, { ...query }).lean();
    } catch (error) {
      throw error;
    }
  }

  public async updateMany(
    filter: FilterQuery<T>,
    query: UpdateQuery<T>,
  ): Promise<any> {
    try {
      return await this.model.updateMany({ ...filter }, { ...query });
    } catch (error) {
      throw error;
    }
  }

  public async updateManyLean(
    filter: FilterQuery<T>,
    query: UpdateQuery<T>,
  ): Promise<any> {
    try {
      return await this.model.updateMany({ ...filter }, { ...query }).lean();
    } catch (error) {
      throw error;
    }
  }

  public async findByIdAndUpdate(
    id: string,
    update: UpdateQuery<T>,
  ): Promise<T> {
    try {
      return await this.model.findByIdAndUpdate(
        id,
        { ...update },
        { new: true },
      );
    } catch (error) {
      throw error;
    }
  }

  public async findByIdAndUpdateLean(
    id: string,
    update: UpdateQuery<T>,
  ): Promise<T> {
    try {
      return await this.model
        .findByIdAndUpdate(id, { ...update }, { new: true })
        .lean();
    } catch (error) {
      throw error;
    }
  }

  public async findByIdsAndUpdate(
    ids: [],
    update: UpdateQuery<T>,
  ): Promise<any> {
    try {
      return await this.model.updateMany(
        { _id: { $in: ids } },
        { ...update },
        { new: true },
      );
    } catch (error) {
      throw error;
    }
  }

  public async findByIdsAndUpdateLean(
    ids: [],
    update: UpdateQuery<T>,
  ): Promise<any> {
    try {
      return await this.model
        .updateMany({ _id: { $in: ids } }, { ...update }, { new: true })
        .lean();
    } catch (error) {
      throw error;
    }
  }
}
