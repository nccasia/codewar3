import { api, driver } from "@rocket.chat/sdk";
import { logBotMessage } from "../helpers/botLogging";
import { isModerator } from "../helpers/isModerator";
import { sendToLog } from "../helpers/sendToLog";
import { CommandInt } from "../interfaces/CommandInt";

export const play: CommandInt = {
  name: "play",
  description: "Phát nhạc sau <number> giây, giá trị mặc định là 0.",
  parameters: ["number"],
  usage: [
    "`{prefix} play` - sẽ phát nhạc sau <number> giây.",
  ],
  modCommand: true,
  command: async (_message, room, BOT): Promise<void> => {
    try {
      if (!BOT.canOrder || (!BOT.listOrders)) {
        return;
      }
      if (!_message.u) {
        return;
      }
      const modCheck = await isModerator(_message.u.username, BOT);

      if (!modCheck) {
        await driver.sendToRoom(
          `@${_message.u.username} Bạn không có quyền sử dụng lệnh \`play\``,
          room
        );
        return;
      }

      const [time] = _message.msg!.split(" ").slice(2);

      // TODO: job để chạy lệnh phát

      // TODO: call api yêu cầu phát bài hát ngay
      
      return;
    } catch (err) {
      await logBotMessage(
        `${room} had an error with the \`close\` command. Check the logs for more info.`,
        BOT
      );
      console.error(err);
    }
  },
};
