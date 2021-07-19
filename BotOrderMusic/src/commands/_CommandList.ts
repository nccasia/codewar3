import { CommandInt } from "../interfaces/CommandInt";
import { close } from "./close";
import { help } from "./help";
import { modHelp } from "./modHelp";
import { order } from "./order";
import { ping } from "./ping";
import { trackLists } from "./tracklists";

/**
 * Construct an array of the commands for the handler
 * to iterate through
 */
export const CommandList: CommandInt[] = [
  help,
  close,
  modHelp,
  ping,
  order,
  trackLists
];
