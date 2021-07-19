import { driver } from "@rocket.chat/sdk";
import { youtubeUrlRegex } from "../assets/youtube-url-regex";
import { logBotMessage } from "../helpers/botLogging";
import { CommandInt } from "../interfaces/CommandInt";
import { OrderInt } from "../interfaces/OrderInt";
import { orderService } from "../services/orderService";

let req: OrderInt = {
    links: [],
};
export const order: CommandInt = {
    name: "order",
    description: "Gửi bài nhạc từ link youtube cho bot music",
    parameters: ["youtubeUrl"],
    usage: ["`{prefix} order youtubeUrl` - Gửi bài nhạc từ link youtube cho bot music."],
    modCommand: false,
    command: async (_message, room, BOT) => {
        try {
            const start = Date.now();
            if (!_message.u || !_message.msg) {
                return;
            }
            const [url] = _message.msg!.split(" ").slice(2);

            if (!youtubeUrlRegex.test(url)){
                console.log("order fail");
                return;
            }
            req.links.push(url);
            req.links = req.links.filter(function(elem, index, self) {
                return index === self.indexOf(elem);
            })

            console.log("links", req.links);
            await orderService(req);
        } catch (err) {
            await logBotMessage(
                `${room} had an error with the \`order\` command. Check the logs for more info.`,
                BOT
            );
            console.error(err);
        }
    },
};
