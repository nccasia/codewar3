import { CommandInt } from "../interfaces/CommandInt";
import { close } from "./close";
import { help } from "./help";
import { modHelp } from "./modHelp";
import { open } from "./open";
import { order } from "./order";
import { ping } from "./ping";
import { play } from "./play";
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
  play,
  open,
  order,
  trackLists
];
