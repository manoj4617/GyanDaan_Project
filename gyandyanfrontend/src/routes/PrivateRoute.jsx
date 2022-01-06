import React from 'react';
import { useSelector } from 'react-redux';
import { Navigate } from 'react-router';

export default function PrivateRoute({children}){
    const authStatus = useSelector((state) => state.auth);
    console.log(authStatus)
    return authStatus.isLoggedin ? children  : <Navigate to="/login" />;
}