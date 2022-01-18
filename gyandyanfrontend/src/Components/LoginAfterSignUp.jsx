import React from 'react'
// import { useLocation } from 'react-router';
import { useParams } from 'react-router-dom';
import LoginStudent from './LoginStudent'
import LoginVolunteer from './LoginVolunteer'

export default function LoginAfterSignUp(props) {
    const { role,message } = useParams();
    
    return (
        <div>
            <div className="card w-25 mx-auto mb-3 h-25 bg-transparent text-center font-weight-bold fs-5">
                <div className="card-body">
                    {message}
                </div>
            </div>
            <ul className="nav nav-pills mb-3 justify-content-center" id="pills-tab" role="tablist">
                <li className="nav-item" role="presentation">
                    <button className={role === "student" ? "nav-link active": "nav-link"} 
                        id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" 
                        type="button" role="tab" 
                        aria-controls="pills-home" 
                        aria-selected="true">
                            Student Login
                    </button>
                </li>
                <li className="nav-item" role="presentation">
                    <button className={role === "volunteer" ? "nav-link active": "nav-link"} 
                        id="pills-profile-tab" 
                        data-bs-toggle="pill" 
                        data-bs-target="#pills-profile" 
                        type="button" role="tab" 
                        aria-controls="pills-profile" aria-selected="false">
                            Volunteer Login
                    </button>
                </li>
            </ul>
            <div className="tab-content" id="pills-tabContent">
                <div className={role === "student" ? "tab-pane fade show active": "tab-pane fade"} 
                    id="pills-home" role="tabpanel" 
                    aria-labelledby="pills-home-tab">
                        <LoginStudent />
                </div>
                <div className={role === "volunteer" ? "tab-pane fade show active": "tab-pane fade"} 
                    id="pills-profile" role="tabpanel" 
                    aria-labelledby="pills-profile-tab">
                        <LoginVolunteer />
                </div>
            </div>
        </div>
    )
}
