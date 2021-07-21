import { driver } from "@rocket.chat/sdk";
import { logBotMessage } from "../helpers/botLogging";
import { isModerator } from "../helpers/isModerator";
import { CommandInt } from "../interfaces/CommandInt";

export const close: CommandInt = {
  name: "close",
  description: "Dừng việc order.",
  parameters: [],
  usage: [
    "`{prefix} close` - sẽ dừng việc order nhạc.",
  ],
  modCommand: true,
  command: async (message, room, BOT): Promise<void> => {
    try {
      if (!BOT.canOrder) {
        return;
      }
      if (!message.u) {
        return;
      }
      const modCheck = await isModerator(message.u.username, BOT);

      if (!modCheck) {
        await driver.sendToRoom(
          `@${message.u.username} Bạn không có quyền sử dụng lệnh \`close\``,
          room
        );
        return;
      }

      BOT.canOrder = false;
      const max = Math.max(...BOT.listOrders.map(({ totalReact }) => totalReact));
      const topTrack = BOT.listOrders.filter(({ totalReact }) => totalReact === max).map(
        (track) => `\`${track.userName}\`: ${track.title} , Vote: \`${track.totalReact}\``
      );
      let response = '';
      if (topTrack && topTrack.length > 0) {
        response = `Đã hết giờ order, sau đây là các bài hát có lượt bình chọn nhiều nhất:\n${topTrack.join("\n")}\n
          Hẹn gặp lại cả nhà vào lần đến.`;
      } else {
        response = `Đã hết giờ order, hôm nay thật buồn vì không có ai order nhạc cho mình cả.
          Hẹn gặp lại cả nhà vào lần đến.`;
      }
      await driver.sendToRoom(response, room);
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
