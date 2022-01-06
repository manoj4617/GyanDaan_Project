import { Route, Routes } from 'react-router';
import PrivateRoute from '../routes/PrivateRoute';
import LoginAfterSignUp from './LoginAfterSignUp';
import Home from './Home';
import CommonSignup from './CommonSignup';
import StudentDashBoard from './StudentDashBoard';
import VolunteerDashBoard from './VolunteerDashBoard';
import JustLogin from './JustLogin'

import React from 'react'

export default function Container() {
    return (
        <>
            <div className='container p-5'>
                <Routes>
                    <Route path='/' element={<Home/>} />
                    <Route path='/login/:role/:message' element={<LoginAfterSignUp/>} />
                    <Route path='/login' element={<JustLogin/>} />
                    <Route path='/signup' element={<CommonSignup/>} />
                    <Route path='/logout' element={<Home/>} />
                    <Route path='/student-dash' element={
                            <PrivateRoute>
                                <StudentDashBoard/>
                            </PrivateRoute>
                        }>
                    </Route>
                    <Route path='/volunteer-dash' element={
                            <PrivateRoute>
                                <VolunteerDashBoard/>
                            </PrivateRoute>
                        }>    
                    </Route>
                </Routes>
            </div>
        </>
    )
}
