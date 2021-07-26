import { driver } from "@rocket.chat/sdk";
import { logBotMessage } from "../helpers/botLogging";
import { CommandInt } from "../interfaces/CommandInt";
import { CommandList } from "./_CommandList";

export const help: CommandInt = {
  name: "help",
  description: "Trả về danh sách các lệnh public có thể dùng.",
  parameters: ["?command"],
  usage: [
    "`{prefix} help` - sẽ trả về các lệnh public.",
    "`{prefix} help ?command` - sẽ trả về mô tả chi tiết lệnh đó.",
  ],
  modCommand: false,
  command: async (message, room, BOT) => {
    try {
      const commands = CommandList.filter((command) => !command.modCommand);

      const commandList = commands.map(
        (command) => `\`${BOT.prefix} ${command.name}\`: ${command.description}`
      );

      const [query] = message.msg!.split(" ").slice(2);

      if (!query) {
        const response = `Danh sách các lệnh mà bạn có thể dùng để tương tác với bot:\n${commandList
          .sort()
          .join("\n")}\n
          Để bình chọn bài hát các bạn hãy thả reaction \`:heart:\` vào các comment đã được bot đánh dấu là hợp lệ nhé.`;
        await driver.sendToRoom(response, room);
        return;
      }

      const targetCommand = commands.find((el) => el.name === query);

      if (!targetCommand) {
        const response = `Bạn không có quyền sử dụng lệnh ${query} này.`;
        await driver.sendToRoom(response, room);
        return;
      }

      const response = `*Information on my \`${query}\` command:*
      _Description_
      ${targetCommand.description}

      _Parameters_
      ${targetCommand.parameters.length ? targetCommand.parameters.join(" ") : "none"}

      _Example Uses_
      ${targetCommand.usage
        .map((use) => use.replace(/\{prefix\}/g, BOT.prefix))
        .join("\n")}`;

      await driver.sendToRoom(response, room);
    } catch (err) {
      await logBotMessage(
        `${room} had an error with the \`help\` command. Check the logs for more info.`,
        BOT
      );
      console.error(err);
    }
  },
};
