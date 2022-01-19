import { type } from '@testing-library/user-event/dist/type'
import React,{useState} from 'react'
import { typeOfClass } from './Constants/Constants'
import { httpClient } from '../http/httpclient'

export default function RequestModal(props) {
    const [message, setmessage] = useState();

    const sendInvite = (studentRequirementId,volunteerRequirementId) =>{
        httpClient
            .get(`RequirementTranscation/send-inviteTo-student/${studentRequirementId}/${volunteerRequirementId}`)
            .then((res) => {
              console.log(res.data);
              setmessage(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    }

    return (
        <>
            <div className="modal fade" id="ModalCenter" tabindex="-1" role="dialog" aria-labelledby="ModalCenterTitle" aria-hidden="true">
                <div className="modal-dialog modal-dialog-centered" role="document">
                    <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title text-black" id="ModalLongTitle">You can Invite to these requirement</h5>
                        <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div className="modal-body">
                        {message != null ? (
                            <h5 className='text-center font-weight-bold text-primary'>{message}</h5>
                        ) : null}

    
                        {props.requests.map((item,index) => (
                            <ul className="list-group">
                                <li className="list-group-item fs-6">
                                    <div className="card" style={{"width": "24rem"}}>
                                        <div className="card-body">
                                            <h5 className="card-title">{item.subject}</h5>
                                            <h6 className="card-subtitle mb-2 text-muted">{item.topic}</h6>
                                            <p className="card-text">Class type : {typeOfClass[item.typeOfClass]}</p>
                                            <button  className="card-link btn btn-warning" 
                                                onClick={()=>sendInvite(props.studentRequirementId,item.id)}   
                                            >Invite</button>
                                        </div>
                                    </div>
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
