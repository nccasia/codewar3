import { Injectable } from '@nestjs/common';
import { InjectModel } from '@nestjs/mongoose';
import { Model, Types } from 'mongoose';
import { BaseService } from 'src/base/services';
import { ProjectCreateDTO } from '../dtos';
import { Project, ProjectDocument } from '../models';

@Injectable()
export class ProjectService extends BaseService<
  ProjectDocument,
  Model<ProjectDocument>
> {
  constructor(
    @InjectModel(Project.name)
    private readonly projectModel: Model<ProjectDocument>,
  ) {
    super(projectModel);
  }

  public async create(data: ProjectCreateDTO): Promise<ProjectDocument> {
    try {
      return await this.projectModel.create({ _id: Types.ObjectId(), ...data });
    } catch (error) {
      throw error;
    }
  }

  public async findAllProjectKomuId(): Promise<string[]> {
    try {
      const rooms = await this.projectModel.find().lean();
      return rooms.map((room) => room.projectKomuId);
    } catch (error) {
      throw error;
    }
  }
}
