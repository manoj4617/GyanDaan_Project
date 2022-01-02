import axios from 'axios';
import { appStore } from '../redux/store';

export const httpClient = axios.create({
    baseURL: 'http://localhost:5000/api/',
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
