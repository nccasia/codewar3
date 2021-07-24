import S3 from 'aws-sdk/clients/s3';

export class S3Service {
  private bucket = new S3({
    accessKeyId: 'AKIARBVALEDL4QKNTS7R',
    secretAccessKey: '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',
  });

  public uploadFile(file, name: string): Promise<string | undefined> {
    return new Promise((resolve, reject) => {
      const params = {
        Bucket: 'attic-trudi-assets',
        Key: `${Date.now()}/${name}.xlsx`,
        Body: file,
        ACL: 'public-read',
        ContentType:
          'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
      };
      return this.bucket.upload(params, function (err, data) {
        if (err) {
          return reject(undefined);
        }
        return resolve(data.Location);
      });
    });
  }
}
