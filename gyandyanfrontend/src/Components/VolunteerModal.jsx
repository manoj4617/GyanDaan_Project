import React,{useState} from 'react'
import {typeOfClass} from './Constants/Constants'
import { httpClient } from "../http/httpclient";


export default function VolunteerModal(props) {

    const [message, setmessage] = useState();

    const acceptRequest = (volunteerId,reqId,studentID) => {
        httpClient
        .get(`RequirementTranscation/accept-request/${volunteerId}/${reqId}/${studentID}`)
        .then((res) => {
            console.log(res.data)
            setmessage(res.data)
        })
        .catch((err) => {
            console.log(err)
        });
    }

    const rejectRequest = (volunteerId,reqId,studentID) => {
        httpClient
        .get(`RequirementTranscation/reject-request/${volunteerId}/${reqId}/${studentID}`)
        .then((res) => {
            setmessage(res.data)
        })
        .catch((err) => {
            console.log(err)
        });
    }

    return (
        <>
            <div className="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div className="modal-dialog modal-dialog-centered" role="document">
                    <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title" id="exampleModalLongTitle">Your Requests</h5>
                        <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div className="modal-body">
                        {message != null ? (
                            <h5 className='text-center font-weight-bold text-primary'>{message}</h5>
                        ) : null}

    
                        {props.invites.map((item,index) => (
                            <ul class="list-group">
                                <li class="list-group-item fs-6">
                                    <p>You have a request from {item.studentProfile.firstName} requesting join {item.volunteer.subject} {typeOfClass[item.volunteer.typeOfClass]} class</p>
                                    <button type="button" onClick={() => acceptRequest(item.volunteer.volunteerProfileId,item.volunteer.id,item.studentProfile.id)} className="btn btn-pill btn-success m-1">Accept</button>
                                    <button type="button" onClick={() => rejectRequest(item.volunteer.volunteerProfileId,item.volunteer.id,item.studentProfile.id)} className="btn btn-pill btn-danger m-1">Reject</button>
                                </li>
                            </ul>
                        ))}
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                    </div>
                </div>
                </div>
        </>
    )
}
