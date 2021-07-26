import { IPartialDriveFile } from './partial-drive-file.interface';

export interface ISearchResultResponse {
  kind: 'drive#fileList';
  nextPageToken: string;
  incompleteSearch: boolean;
  files: IPartialDriveFile[];
}
