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
       
    }
}