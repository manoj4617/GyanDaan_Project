import axios from 'axios';
import { appStore } from '../redux/store';

export const httpClient = axios.create({
    baseURL: 'https://localhost:5001/api/',
});


httpClient.interceptors.request.use(
    (config) => {
        if(appStore.getState().auth.token){
            config.headers['Authorization'] = `Bearer ${appStore.getState().auth.token}`;
        }

        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);
