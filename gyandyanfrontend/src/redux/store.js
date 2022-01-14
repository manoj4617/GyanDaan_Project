import {configureStore} from '@reduxjs/toolkit';
import {authSlice} from './auth';
import storage from 'redux-persist/lib/storage'
import { persistReducer, FLUSH,
    REHYDRATE,
    PAUSE,
    PERSIST,
    PURGE,
    REGISTER, } from 'redux-persist'

const persistConfig = {
    key: 'root',
    storage
};
const persistedReducer = persistReducer(persistConfig, authSlice.reducer);
export const appStore = configureStore({
    reducer: {
        auth: persistedReducer,
    },
    middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    }),
});

