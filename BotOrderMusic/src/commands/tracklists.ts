import { driver } from "@rocket.chat/sdk";
import { CommandInt } from "../interfaces/CommandInt";

export const trackLists: CommandInt = {
    name: "tracklists",
    description: "Hiển thị danh sách các bài hát đang được order.",
    parameters: [] /*Array of command parameters*/,
    usage: [
        "`{prefix} tracklists` - Hiển thị danh sách các bài hát đang được order",
    ], /* Example use cases. The {prefix} is replaced with the bot's actual prefix automatically */
    modCommand: false,
    command: async (message, room, BOT) => {
        // TODO: Cần call đến api để biết bài nào đang được phát
        const list = BOT.listOrders.map(
            (track) => `${track.isPlaying?":cd:":"__"} \`${track.userName}\`: ${track.title} , Vote: \`${track.totalReact}\``
        );
        let response = "";
        if (list && list.length > 0) {
            response = `Danh sách các bài hát:\n${list
                .join("\n")}\n
                Để bình chọn bài hát các bạn hãy thả reaction \`:heart:\` vào các comment đã được bot đánh dấu là hợp lệ nhé.`;
        } else {
            response = `Hiện tại chưa có ai order nhạc cả.`;
        }
        await driver.sendToRoom(response, room);
        return;
    }
}