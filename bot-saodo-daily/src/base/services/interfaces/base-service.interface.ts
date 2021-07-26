import { FilterQuery, UpdateQuery } from 'mongoose';

export interface IBaseService<T> {
  findById(_id: string): Promise<T>;
  findByIdLean(_id: string): Promise<T>;
  findByIds(_ids: []): Promise<T[]>;
  findByIdsLean(_ids: []): Promise<T[]>;
  findAll(): Promise<T[]>;
  findAllLean(): Promise<T[]>;
  findOne(filter: FilterQuery<T>): Promise<T>;
  findOneLean(filter: FilterQuery<T>): Promise<T>;
  findMany(filter: FilterQuery<T>): Promise<T[]>;
  findManyLean(filter: FilterQuery<T>): Promise<T[]>;

  clearAll(): Promise<any>;

  deleteById(_id: string): Promise<T>;
  deleteManyByIds(_ids: []): Promise<T>;
  deleteOne(filter: FilterQuery<T>): Promise<T>;
  deleteMany(filter: FilterQuery<T>): Promise<any>;

  checkExitsById(_id: string): Promise<boolean>;
  checkExitsManyByIds(_ids: []): Promise<boolean>;
  checkExits(filter: FilterQuery<T>): Promise<boolean>;

  findOneAndUpdateOrInsert(
    filter: FilterQuery<T>,
    query: UpdateQuery<T>,
  ): Promise<T>;
  findOneAndUpdateOrInsertLean(
    filter: FilterQuery<T>,
    query: UpdateQuery<T>,
  ): Promise<T>;
  updateOne(filter: FilterQuery<T>, query: UpdateQuery<T>): Promise<any>;
  updateOneLean(filter: FilterQuery<T>, query: UpdateQuery<T>): Promise<any>;
  updateMany(filter: FilterQuery<T>, query: UpdateQuery<T>): Promise<any>;
  updateManyLean(filter: FilterQuery<T>, query: UpdateQuery<T>): Promise<any>;
  findOneAndUpdate(filter: FilterQuery<T>, query: UpdateQuery<T>): Promise<T>;
  findOneAndUpdateLean(
    filter: FilterQuery<T>,
    query: UpdateQuery<T>,
  ): Promise<T>;
  findByIdAndUpdate(id: string, update: UpdateQuery<T>): Promise<T>;
  findByIdAndUpdateLean(id: string, update: UpdateQuery<T>): Promise<T>;
  findByIdsAndUpdate(ids: [], update: UpdateQuery<T>): Promise<any>;
  findByIdsAndUpdateLean(ids: [], update: UpdateQuery<T>): Promise<any>;
}
