import { Route, Routes } from 'react-router';
import PrivateRoute from '../routes/PrivateRoute';
import LoginAfterSignUp from './LoginAfterSignUp';
import Home from './Home';
import CommonSignup from './CommonSignup';
import StudentDashBoard from './StudentDashBoard';
import VolunteerDashBoard from './VolunteerDashBoard';
import JustLogin from './JustLogin'
import RequirementAdding from './RequirementAdding';
import UpdateReqTable from './UpdateReqTable';
import React from 'react'
import UpdateProfile from './UpdateProfile';

export default function Container() {

    return (
        <>
            <div className='container p-5'>
                <Routes>
                    <Route path='/' element={<Home/>} />
                    <Route path='/login/:role/:message' element={<LoginAfterSignUp/>} />
                    <Route path='/login' element={<JustLogin/>} />
                    <Route path='/signup' element={<CommonSignup/>} />
                    <Route path='/logout' element={<JustLogin/>} />
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
                    <Route path='/requirement' element={
                            <PrivateRoute>
                                <RequirementAdding/>
                            </PrivateRoute>
                        }>    
                    </Route>
                    <Route path='/update-requirement/:data' element={
                            <PrivateRoute>
                                <UpdateReqTable/>
                            </PrivateRoute>
                        }>    
                    </Route>
                    <Route path='/update-profile' element={
                            <PrivateRoute>
                                <UpdateProfile/>
                            </PrivateRoute>
                        }>    
                    </Route>
                </Routes>
            </div>
        </>
    )
}
