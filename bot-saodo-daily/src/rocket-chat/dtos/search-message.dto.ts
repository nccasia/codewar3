export class SearchMessageResDTO {
  roomId: string;
  user: {
    username: string;
    name: string;
  };
  content: string;
  time: string;
}
