import { Types } from 'mongoose';
import { ECronTime } from 'src/base/interfaces';

export interface IDaily {
  remoteId: Types.ObjectId;
  projectId: Types.ObjectId;
  content: string;
  time: Date;
  noDaily: boolean;
  timeRemote: ECronTime;
}
