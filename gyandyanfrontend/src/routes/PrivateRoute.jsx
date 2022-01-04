import React from 'react';
import { useSelector } from 'react-redux';
import { Navigate, Outlet } from 'react-router';

export default function PrivateRoute(){
    const authStatus = useSelector((state) => state.auth);
    return authStatus ? <Outlet /> : <Navigate to="/login" />;
}