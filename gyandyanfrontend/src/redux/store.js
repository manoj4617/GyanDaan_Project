import {configureStore} from '@reduxjs/toolkit';
import {authSlice} from './auth';

export const appStore = configureStore({
    reducer: {
        auth: authSlice.reducer,
    },
});

