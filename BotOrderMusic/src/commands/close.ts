import { driver } from "@rocket.chat/sdk";
import { MODLIST } from "..";
import { logBotMessage } from "../helpers/botLogging";
import { getModerators } from "../helpers/getModerators";
import { isModerator } from "../helpers/isModerator";
import { CommandInt } from "../interfaces/CommandInt";
import { stopStreamService } from "../services/stopStreamService";

export const close: CommandInt = {
  name: "close",
  description: "Dừng việc stream.",
  parameters: [],
  usage: [
    "`{prefix} close` - sẽ dừng việc phát nhạc.",
  ],
  modCommand: true,
  command: async (message, room, BOT): Promise<void> => {
    try {
      if (!BOT.listOrders || BOT.listOrders.length < 1) {
        return;
      }
      if (!message.u) {
        return;
      }
      MODLIST.users = await getModerators(BOT, room);
      const modCheck = await isModerator(message.u.username, MODLIST);

      if (!modCheck) {
        await driver.sendToRoom(
          `@${message.u.username} Bạn không có quyền sử dụng lệnh \`close\``,
          room
        );
        return;
      }
      BOT.canOrder = false;
      await stopStreamService();
      BOT.listOrders = [];
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
