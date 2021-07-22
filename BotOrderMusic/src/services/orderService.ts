import axios from "axios";
import { OrderInt } from "../interfaces/OrderInt";

const apiUrlBase = process.env.SERVER_API_URL;

export const orderService = (data: OrderInt): void => {
    axios.post<OrderInt>(apiUrlBase + "/save-music", data)
    .then(response => {
        console.log(response.status);
    }).catch((ex) => console.log(ex));
};