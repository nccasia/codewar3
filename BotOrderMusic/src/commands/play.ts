import { api, driver } from "@rocket.chat/sdk";
import { MODLIST } from "..";
import { logBotMessage } from "../helpers/botLogging";
import { getModerators } from "../helpers/getModerators";
import { isModerator } from "../helpers/isModerator";
import { sendToLog } from "../helpers/sendToLog";
import { CommandInt } from "../interfaces/CommandInt";
import { OrderInfoInt, OrderInt } from "../interfaces/OrderInt";
import { orderService } from "../services/orderService";

export const play: CommandInt = {
  name: "play",
  description: "Phát nhạc ngay và dừng việc order nhạc",
  parameters: [],
  usage: [
    "`{prefix} play` - sẽ phát nhạc và dừng việc order nhạc",
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
      MODLIST.users = await getModerators(BOT, room);
      const modCheck = await isModerator(_message.u.username, MODLIST);

      if (!modCheck) {
        await driver.sendToRoom(
          `@${_message.u.username} Bạn không có quyền sử dụng lệnh \`play\``,
          room
        );
        return;
      }

      BOT.canOrder = false;
      BOT.listOrders = BOT.listOrders.sort(function(a, b) {
        return b.totalReact - a.totalReact;
      });
      const list = BOT.listOrders.map(
        (track) => `${track.isPlaying?":cd:":"__"} \`${track.userName}\`: ${track.title} , Vote: \`${track.totalReact}\``
    );
      let response = '';
      if (list && list.length > 0) {
        response = `Đã hết giờ order, sau đây là các bài hát có lượt bình chọn nhiều nhất:\n${list.join("\n")}\n
          Hẹn gặp lại cả nhà vào lần đến.`;
      } else {
        response = `Đã hết giờ order, hôm nay thật buồn vì không có ai order nhạc cho mình cả.
          Hẹn gặp lại cả nhà vào lần đến.`;
      }
      await driver.sendToRoom(response, room);
      // TODO: call api yêu cầu phát bài hát ngay
      let req: OrderInt = {
        links: [],
      };
      for (let i = 0; i < BOT.listOrders.length; i++) {
          let data: OrderInfoInt = {
              id: BOT.listOrders[i].msgId,
              link: BOT.listOrders[i].url!
          }
          req.links.push(data);
      }
      await orderService(req);
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
