import { Types } from 'mongoose';
import { ERemoteTime } from 'src/base/interfaces';

export interface IRemote {
  username: string;
  name: string;
  remoteDate: Date;
  projectId: Types.ObjectId;
  remoteTime: ERemoteTime;
  dailyId?: Types.ObjectId;
}
