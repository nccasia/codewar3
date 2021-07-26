import { driver } from "@rocket.chat/sdk";
import * as schedule from "node-schedule";
import { BOT } from "..";
import { OrderInfoInt, OrderInt } from "../interfaces/OrderInt";
import { orderService } from "../services/orderService";

export const job = async (room) => {
    let rule = new schedule.RecurrenceRule();
    // order
    rule.dayOfWeek = [1, new schedule.Range(1, 5)];
    rule.hour = 17;
    rule.minute = 0;
    schedule.scheduleJob(rule, async function () {
        if (!BOT.canOrder) {
            BOT.canOrder = true;
            await driver.sendToRoom(
                "Đến giờ order nhạc rồi mọi người @all",
                BOT.botLogChannel || room[0]
            );
        }
    });

    // end order
    let ruleClose = new schedule.RecurrenceRule();
    ruleClose.dayOfWeek = [1, new schedule.Range(1, 5)];
    ruleClose.hour = 17;
    ruleClose.minute = 30;

    schedule.scheduleJob(ruleClose, async function () {
        if (BOT.canOrder) {
            BOT.canOrder = false;
            BOT.listOrders = BOT.listOrders.sort(function(a, b) {
                return b.totalReact - a.totalReact;
            });
            const topTrack = BOT.listOrders.map(
                (track) => `${track.isPlaying?":cd:":"__"} \`${track.userName}\`: ${track.title} , Vote: \`${track.totalReact}\``
            );
            let response = '';
            if (topTrack && topTrack.length > 0) {
                let req: OrderInt = {
                    links: [],
                };
                for (let i = 0; i < BOT.listOrders.length; i++) {
                    let data: OrderInfoInt = {
                        id: BOT.listOrders[i].msgId,
                        link: BOT.listOrders[i].url!
                    }
                    req.links.push(data);
                }               
                await orderService(req);
                response = `Đã hết giờ order, sau đây là các bài hát có lượt bình chọn nhiều nhất:\n${topTrack.join("\n")}\n
                Các bài hát hôm nay sẽ được phát ngay bây giờ.`;
            } else {
                response = `Đã hết giờ order, hôm nay thật buồn vì không có ai order nhạc cho mình cả.
                Hẹn gặp lại cả nhà vào lần đến.`;
            }
            BOT.listOrders = [];
            await driver.sendToRoom(response, BOT.botLogChannel || room[0]);
        }
    });
};