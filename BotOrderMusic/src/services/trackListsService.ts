import axios from "axios";

const apiUrlBase = process.env.SERVER_API_URL;

export const trackListsService = (): void => {
    axios.get(apiUrlBase + "/lists")
    .then(response => {
        console.log(response.status);
    }).catch((ex) => console.log(ex));
};