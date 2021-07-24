import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:8000'
})

const getListEmployees = async () => {
    return await api.get(`/api/employees`);
}

export default {
    getListEmployees,
};