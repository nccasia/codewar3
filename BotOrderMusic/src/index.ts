import dotenv from "dotenv";
import { api, driver, settings } from "@rocket.chat/sdk";
import { CommandHandler } from "./commands/_CommandHandler";
import { BotInt, ModInt } from "./interfaces/BotInt";
import packageData from "../package.json";
import { job } from "./helpers/job";
import { getModerators } from "./helpers/getModerators";

dotenv.config();

const {
  ROCKETCHAT_URL,
  ROCKETCHAT_USER,
  ROCKETCHAT_PASSWORD,
  BOTNAME,
  MOD_LOG_CHANNEL,
  BOT_LOG_CHANNEL,
  BOT_RATE_LIMIT,
  SERVER_API_URL
} = process.env;

console.log(process.env);

const ROOMS = (process.env.ROCKETCHAT_ROOM || "general")
  .split(",")
  .map((el) => el.trim());

if (!ROCKETCHAT_URL || !ROCKETCHAT_USER || !ROCKETCHAT_PASSWORD || !BOTNAME) {
  console.error("Missing required environment variables.");
  process.exit(1);
}

if (!MOD_LOG_CHANNEL || !BOT_LOG_CHANNEL) {
  console.warn(
    "Missing log channel settings. Logging will be disabled for this instance!"
  );
}

export const BOT: BotInt = {
  botId: "",
  botName: BOTNAME,
  hostPath: ROCKETCHAT_URL,
  modLogChannel: MOD_LOG_CHANNEL || "",
  botLogChannel: BOT_LOG_CHANNEL || "",
  version: packageData.version,
  prefix: process.env.PREFIX || "!bot",
  modRoles: process.env.ROLE_LIST?.split(",").map((el) => el.trim()) || [
    "none",
  ],
  botRateLimit: parseInt(BOT_RATE_LIMIT || "10"),
  lastCommandCalled: 0,
  listOrders: [],
  canOrder: false
};

export const MODLIST: ModInt = {
  users: []
};

/**
 * The primary driver to run the bot.
 */
const runBot = async () => {
  const ssl = process.env.ROCKETCHAT_USE_SSL ? true : false;

  // force lib to use host?
  settings.host = ROCKETCHAT_URL;

  // Connect to server, log in.
  await driver.connect({ host: ROCKETCHAT_URL, useSsl: ssl });
  BOT.botId = await driver.login({
    username: ROCKETCHAT_USER,
    password: ROCKETCHAT_PASSWORD,
  });

  // Auth to REST API
  await api.login({ username: ROCKETCHAT_USER, password: ROCKETCHAT_PASSWORD });

  // Join configured rooms.
  // await driver.joinRooms(ROOMS.concat([BOT.modLogChannel, BOT.botLogChannel]));
  // console.log("joined rooms");

  // Listen for messages.
  const subscribed = await driver.subscribeToMessages();
  console.log(subscribed);

  // Pass received messages to command handler
  await driver.reactToMessages(CommandHandler);
  console.log("connected and waiting for messages");
  MODLIST.users = await getModerators(BOT, ROOMS[0]);
  // await driver.sendToRoom(
  //   "Hi @all!!",
  //   BOT.botLogChannel || ROOMS[0]
  // );
  BOT.canOrder = false;
  console.log("Greeting message sent.");

  await job(ROOMS);
};


runBot();
