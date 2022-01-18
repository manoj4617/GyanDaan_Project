import { createSlice } from '@reduxjs/toolkit';

export const authSlice = createSlice({
    name: 'authSlice',
    initialState: {
        isLoggedin: false,
        currentUser: null,
        token: null,
    },
    reducers: {
        login: (state, action) => {
            state.isLoggedin = true;
            state.currentUser = action.payload.email;
            state.token = action.payload.token;
        },
        logout: (state, action) => {
            state.isLoggedin = false;
            state.currentUser = null;
            state.token = null;
        },
    },
});
