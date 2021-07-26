import { Injectable } from '@nestjs/common';
import { google } from 'googleapis';
import fs from 'fs';
import { IPartialDriveFile, ISearchResultResponse } from '../interfaces';
import { EnvService } from 'src/environment';

@Injectable()
export class GGDriveService {
  private driveClient: any;

  public constructor(private readonly config: EnvService) {
    const { clientId, clientSecret, redirectUri, refreshToken } =
      this.config.google;
    this.driveClient = this.createDriveClient(
      clientId,
      clientSecret,
      redirectUri,
      refreshToken,
    );
  }

  public createDriveClient(
    clientId: string,
    clientSecret: string,
    redirectUri: string,
    refreshToken: string,
  ) {
    try {
      const client = new google.auth.OAuth2(
        clientId,
        clientSecret,
        redirectUri,
      );
      client.setCredentials({ refresh_token: refreshToken });
      return google.drive({
        version: 'v3',
        auth: client,
      });
    } catch (error) {
      throw error;
    }
  }

  public async createFolder(folderName: string): Promise<IPartialDriveFile> {
    try {
      return await this.driveClient.files.create({
        resource: {
          name: folderName,
          mimeType: 'application/vnd.google-apps.folder',
        },
        fields: 'id, name',
      });
    } catch (error) {
      throw error;
    }
  }

  public async searchFolder(
    folderName: string,
  ): Promise<IPartialDriveFile | null> {
    try {
      const res = await this.driveClient.files.list({
        q: `mimeType='application/vnd.google-apps.folder' and name='${folderName}'`,
        fields: 'files(id, name)',
      });
      const data: ISearchResultResponse = res.data;
      return data.files ? data.files[0] : null;
    } catch (error) {
      throw error;
    }
  }

  public async saveFile(
    fileName: string,
    filePath: string,
    fileMimeType: string,
    folderId?: string,
  ) {
    try {
      return await this.driveClient.files.create({
        requestBody: {
          name: fileName,
          mimeType: fileMimeType,
          parents: folderId ? [folderId] : [],
        },
        media: {
          mimeType: fileMimeType,
          body: fs.createReadStream(filePath),
        },
      });
    } catch (error) {
      throw error;
    }
  }
}
