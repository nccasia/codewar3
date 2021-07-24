import { EValueType } from '../constants';

export interface IGetMany {
  keys: string[];
  types?: EValueType[];
}
