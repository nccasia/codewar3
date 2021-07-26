import { EProjectType } from './enums';

export interface IProject {
  name: string;
  projectKomuId: string;
  type: EProjectType;
}
