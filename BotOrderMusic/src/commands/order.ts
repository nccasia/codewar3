import { driver } from "@rocket.chat/sdk";
import getYouTubeID from "get-youtube-id";
import getYoutubeTitle from "get-youtube-title";
import { youtubeUrlRegex } from "../assets/youtube-url-regex";
import { logBotMessage } from "../helpers/botLogging";
import { CommandInt } from "../interfaces/CommandInt";

export const order: CommandInt = {
    name: "order",
    description: "Gửi bài nhạc từ link youtube cho bot music",
    parameters: ["youtubeUrl"],
    usage: ["`{prefix} order <youtubeUrl>` - Gửi bài nhạc từ link youtube cho bot music."],
    modCommand: false,
    command: async (_message, room, BOT) => {
        try {
            const start = Date.now();
            if (!_message.u || !_message.msg) {
                return;
            }
            const [url] = _message.msg!.split(" ").slice(2);
            if (!BOT.canOrder) {
                // await driver.sendToRoom(
                //     `@${_message.u?.username} chưa đến giờ order nhạc.`,
                //     room
                // );
                return;
            }
            if (!youtubeUrlRegex.test(url)) {
                console.log("order fail");
                BOT.listOrders.splice(BOT.listOrders.findIndex(x => x.msgId == _message._id), 1);
                return;
            }
            
            const youtubeId = getYouTubeID(url);

            if (BOT.listOrders.findIndex(x => x.youtubeId == youtubeId) > -1) {
                await driver.sendToRoom(
                    `@${_message.u?.username} bài hát của bạn đã được order rồi.`,
                    room
                );
                BOT.listOrders.splice(BOT.listOrders.findIndex(x => x.msgId == _message._id), 1);
                return;
            }
            const foundIndex = BOT.listOrders.findIndex(x => x.msgId == _message._id);
            if (foundIndex > -1) {
                getYoutubeTitle(youtubeId, function (err, title) {
                    BOT.listOrders[foundIndex].youtubeId = youtubeId!;
                    BOT.listOrders[foundIndex].url = url!;
                    BOT.listOrders[foundIndex].title = title!
                });

                BOT.listOrders[foundIndex].lastTimeOrder = Date.now();
                await driver.setReaction(":heart:", `${_message._id}`);
                BOT.listOrders[foundIndex].totalReact = 1;
            } 
        } catch (err) {
            await logBotMessage(
                `${room} had an error with the \`order\` command. Check the logs for more info.`,
                BOT
            );
            console.error(err);
        }
    },
};
