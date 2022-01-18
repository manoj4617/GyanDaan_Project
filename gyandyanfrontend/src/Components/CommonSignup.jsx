import React from 'react'
import SignupStudent from './SignupStudent'
import SignupVolunteer from './SignupVolunteer'

export default function CommonSignup() {
    return (
        <div>
            
            <ul className="nav nav-pills mb-3 justify-content-center" id="pills-tab" role="tablist">
                <li className="nav-item" role="presentation">
                    <button className="nav-link active" id="pills-home-tab" data-bs-toggle="pill" data-bs-target="#pills-home" type="button" role="tab" aria-controls="pills-home" aria-selected="true">Student Sign Up</button>
                </li>
                <li className="nav-item" role="presentation">
                    <button className="nav-link" id="pills-profile-tab" data-bs-toggle="pill" data-bs-target="#pills-profile" type="button" role="tab" aria-controls="pills-profile" aria-selected="false">Volunteer Sign Up</button>
                </li>
            </ul>
            <div className="tab-content" id="pills-tabContent">
                <div className="tab-pane fade show active" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                    <SignupStudent />
                </div>
                <div className="tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                    <SignupVolunteer />
                </div>
            </div>
        </div>
    )
}
