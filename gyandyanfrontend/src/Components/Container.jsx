import { Route, Routes } from 'react-router';
import PrivateRoute from '../routes/PrivateRoute';
import CommonLogin from './CommonLogin';
import Home from './Home';
import CommonSignup from './CommonSignup';
import StudentDashBoard from './StudentDashBoard';
import VolunteerDashBoard from './VolunteerDashBoard';

import React from 'react'

export default function Container() {
    return (
        <>
            <div className='container p-5'>
                <Routes>
                    <Route path='/' element={<Home/>} />
                    <Route path='/login' element={<CommonLogin/>} />
                    <Route path='/signup' element={<CommonSignup/>} />
                    <Route path='/student-dash' element={<PrivateRoute/>}>
                        <Route path='/student-dash' element={<StudentDashBoard/>} />
                    </Route>
                    <Route path='/volunteer-dash' element={<PrivateRoute/>}>
                        <Route path='/volunteer-dash' element={<VolunteerDashBoard/>} />
                    </Route>
                </Routes>
            </div>
        </>
    )
}
