import React from 'react';
import { useSelector } from 'react-redux';
import { Navigate } from 'react-router';
import {isAlive } from '../utils/jwt';

export default function PrivateRoute({children}){
    const authStatus = useSelector((state) => state.auth);
    var alive = false;
    if(authStatus.token){
        alive = isAlive(authStatus.token)
    }
    return authStatus.isLoggedin && alive ? children  : <Navigate to="/login" />;
}