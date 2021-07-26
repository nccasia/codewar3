import { api } from "@rocket.chat/sdk";
import { UserInfoInt } from "../interfaces/apiInt";
import { BotInt, ModInt } from "../interfaces/BotInt";

/**
 * Queries the Rocket.Chat API to determine if a given user
 * has at least one role in the moderator roles list set
 * through the .env
 * @param {string} userId The user ID to check
 * @param {BotInt} BOT Bot configuration object
 * @returns {Promise<boolean>} Boolean value for "does user have moderator role"
 */
export const isModerator = async (
  userName: string,
  userMod: ModInt
): Promise<boolean> => {
    if (userMod.users.includes(userName)) {
      return true;
    }
  return false;
};
