import axios from "axios";

const apiUrlBase = process.env.SERVER_API_URL;

export const stopStreamService = (): void => {
    axios.get(apiUrlBase + "/stop")
    .then(response => {
        console.log(response.status);
    }).catch((ex) => console.log(ex));
};