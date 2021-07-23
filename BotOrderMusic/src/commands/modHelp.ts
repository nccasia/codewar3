import { driver } from "@rocket.chat/sdk";
import { MODLIST } from "..";
import { logBotMessage } from "../helpers/botLogging";
import { getModerators } from "../helpers/getModerators";
import { isModerator } from "../helpers/isModerator";
import { CommandInt } from "../interfaces/CommandInt";
import { CommandList } from "./_CommandList";

export const modHelp: CommandInt = {
  name: "modHelp",
  description: "trả về danh sách cách lệnh mà mod của kênh có thể dùng.",
  parameters: ["?command"],
  usage: [
    "`{prefix} modHelp` - trả về danh sách cách lệnh mà mod của kênh có thể dùng",
    "`{prefix} modHelp ?command` - trả về thông tin chi tiết của lệnh mà mod của kênh có thể dùng.",
  ],
  modCommand: true,
  command: async (message, room, BOT) => {
    try {
      const commands = CommandList.filter((command) => command.modCommand);

      const commandList = commands.map(
        (command) => `\`${BOT.prefix} ${command.name}\`: ${command.description}`
      );
      MODLIST.users = await getModerators(BOT, room);

      const isMod = await isModerator(message.u!.username, MODLIST);
      if (!isMod) {
        await driver.sendToRoom(
          `@ ${message.u!.username} Bạn không phải là mod của kênh nên không thể dùng lệnh này.`,
          room
        );
        return;
      }

      const [query] = message.msg!.split(" ").slice(2);

      if (!query) {
        const response = `Đây là danh sách cách lệnh mà mod có thể dùng trong kênh:\n${commandList
          .sort()
          .join("\n")}`;
        await driver.sendToRoom(response, room);
        return;
      }

      const targetCommand = commands.find((el) => el.name === query);

      if (!targetCommand) {
        const response = `lệnh ${query} không hợp lệ.`;
        await driver.sendToRoom(response, room);
        return;
      }

      const response = `*Thông tin chi tiết của lệnh \`${query}\`:*
      _Description_
      ${targetCommand.description}

      _Parameters_
      ${
        targetCommand.parameters.length ? targetCommand.parameters.join(", ") : "none"
      }

      _Example Uses_
      ${targetCommand.usage
        .map((use) => use.replace(/\{prefix\}/g, BOT.prefix))
        .join("\n")}`;

      await driver.sendToRoom(response, room);
    } catch (err) {
      await logBotMessage(
        `${room} had an error with the \`modHelp\` command. Check the logs for more info.`,
        BOT
      );
      console.error(err);
    }
  },
};
