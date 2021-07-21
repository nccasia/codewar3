import { driver } from "@rocket.chat/sdk";
import * as schedule from "node-schedule";
import { BOT } from "..";

export const job = async (room) => {
    let rule = new schedule.RecurrenceRule();
    // order
    rule.dayOfWeek = [1, new schedule.Range(1, 5)];
    rule.hour = 3;
    rule.minute = 1;
    schedule.scheduleJob(rule, async function () {
        if (!BOT.canOrder) {
            BOT.canOrder = true;
            await driver.sendToRoom(
                "Đến giờ order nhạc rồi mọi người",
                BOT.botLogChannel || room[0]
            );
        }
    });

    // end vote
    let ruleClose = new schedule.RecurrenceRule();
    ruleClose.dayOfWeek = [1, new schedule.Range(1, 5)];
    ruleClose.hour = 3;
    ruleClose.minute = 2;

    schedule.scheduleJob(ruleClose, async function () {
        if (BOT.canOrder) {
            BOT.canOrder = false;
            const max = Math.max(...BOT.listOrders.map(({ totalReact }) => totalReact));
            const topTrack = BOT.listOrders.filter(({ totalReact }) => totalReact === max).map(
                (track) => `\`${track.userName}\`: ${track.title} , Vote: \`${track.totalReact}\``
            );
            let response = '';
            if (topTrack && topTrack.length > 0) {
                response = `Đã hết giờ order, sau đây là các bài hát có lượt bình chọn nhiều nhất:\n${topTrack.join("\n")}\n
                Hẹn gặp lại cả nhà vào lần đến.`;
            } else {
                response = `Đã hết giờ order, hôm nay thật buồn vì không có ai order nhạc cho mình cả.
                Hẹn gặp lại cả nhà vào lần đến.`;
            }
            BOT.listOrders = [];
            await driver.sendToRoom(response, BOT.botLogChannel || room[0]);
        }
    });
};