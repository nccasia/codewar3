import { api, driver } from "@rocket.chat/sdk";
import { MODLIST } from "..";
import { logBotMessage } from "../helpers/botLogging";
import { getModerators } from "../helpers/getModerators";
import { isModerator } from "../helpers/isModerator";
import { sendToLog } from "../helpers/sendToLog";
import { CommandInt } from "../interfaces/CommandInt";

export const open: CommandInt = {
  name: "open",
  description: "Bắt đầu việc order nhạc.",
  parameters: [],
  usage: [
    "`{prefix} open` - sẽ bắt đầu việc order nhạc.",
  ],
  modCommand: true,
  command: async (_message, room, BOT): Promise<void> => {
    try {
      if (BOT.canOrder) {
        await driver.sendToRoom("Đang trong thời order nhạc", room);
        return;
      }
      if (!_message.u) {
        return;
      }
      MODLIST.users = await getModerators(BOT, room);
      const modCheck = await isModerator(_message.u.username, MODLIST);

      if (!modCheck) {
        await driver.sendToRoom(
          `@${_message.u.username} Bạn không có quyền sử dụng lệnh \`open\``,
          room
        );
        return;
      }

      BOT.canOrder = true;
      await driver.sendToRoom("Cơ trưởng on air zô zô @all", room);
      
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
