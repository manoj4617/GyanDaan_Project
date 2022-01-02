import {configureSore} from '@reduxjs/toolkit';
import {authSlice} from './auth';

export const appStore = configureSore({
    reducer: {
        auth: authSlice.reducer,
    },
});

