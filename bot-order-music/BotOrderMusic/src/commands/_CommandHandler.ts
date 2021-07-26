import { driver } from "@rocket.chat/sdk";
import { BOT } from "..";
import { logBotMessage } from "../helpers/botLogging";
import { MessageInt } from "../interfaces/messageInt";
import { TrackListsInt } from "../interfaces/OrderInt";
import { CommandList } from "./_CommandList";

/**
 * This is a command handler which runs on each message
 * that the bot receives.
 * @param {unknown} err RocketChat SDK uses an error first callback.
 * @param {string[]} messages This is actually an array of messages, not a single message.
 * @param {unknown} _messageOptions Not sure what this is...
 * @returns {Promise<void>} nothing
 * @todo Need to determine typing for the error and messageOptions?
 */
export const CommandHandler = async (
  err: unknown,
  message: MessageInt
): Promise<void> => {
  if (err) {
    console.error(err);
    return;
  }

  // const message = messages[0];
  console.log("msgId: ", message._id);

  if (!message)
    return;
  if (!message || message._id === BOT.botId) {
    return;
  }

  if (message.replies) {
    return;
  }

  if (message.editedBy || message.editedAt) {
    return;
  }

  if (!message.msg) {
    console.error("Message empty??");
    return;
  }

  if (!message.rid) {
    console.error("No room id?");
    return;
  }

  // if (message.reactions && message.reactions?.[':heart:']) {
  //   console.log(message.reactions?.[':heart:'].usernames.length);
  // }
  const roomName = await driver.getRoomName(message.rid);

  const foundIndex = BOT.listOrders.findIndex(x => x.msgId == message._id);
  if (foundIndex > -1) {
    BOT.listOrders[foundIndex].totalReact = message.reactions?.[':heart:'].usernames?.length!;
  }

  const [prefix, commandName] = message.msg.split(" ");
  if (prefix.toLowerCase() === BOT.prefix) {
    const currentTime = Date.now();
    const timeSinceLastCommand = currentTime - BOT.lastCommandCalled;

    if (commandName === 'order' && BOT.canOrder) {
      console.log("Track: ", BOT.listOrders);
      if (BOT.listOrders.some(o => o.msgId.includes(message._id!))) return;
      // let dataFilter = BOT.listOrders.filter(el => el.userName === message.u?.username);
      // if (dataFilter && dataFilter.length > 0) {
      //   let maxTime = Math.max(...dataFilter.map(el => el.lastTimeOrder));
      //   console.log("Time",currentTime - maxTime);
      //   if ((currentTime - maxTime) < 600*1000) {
      //     await driver.sendToRoom(
      //       `@${message.u?.username
      //       }, bạn còn \`${currentTime - maxTime
      //       }\` giây cho đến lượt order tiếp theo`,
      //       roomName
      //     );
      //     return;
      //   }
      // }
      let track: TrackListsInt = {
        msgId: message._id!,
        userName: message.u?.username!,
        totalReact: 0,
        title: undefined,
        url: undefined,
        youtubeId: undefined,
        lastTimeOrder: 0,
        isPlaying: false
      }
      BOT.listOrders.push(track);
    }
    if (timeSinceLastCommand < BOT.botRateLimit * 1000) {
      // await driver.sendToRoom(
      //   `@${message.u?.username
      //   }, bạn đã nhập quá nhanh. Vui lòng đợi trong ${BOT.botRateLimit - timeSinceLastCommand / 1000
      //   } giây và thử lại.`,
      //   roomName
      // );
      // return;
    }
    for (const Command of CommandList) {
      if (commandName === Command.name) {
        await Command.command(message, roomName, BOT);
        await logBotMessage(
          `@${message.u?.username} called the \`${commandName}\` command in #${roomName}.`,
          BOT
        );
        BOT.lastCommandCalled = Date.now();
        return;
      }
    }
    await driver.sendToRoom(
      `@${message.u?.username} lệnh ${commandName} không hợp lệ.`,
      roomName
    );
  }
};
