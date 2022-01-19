import React, { useState, useEffect } from "react";
import { useSelector } from "react-redux";
import { decode } from "../utils/jwt";
import { httpClient } from "../http/httpclient";
import accepted from '../Components/images/accepted1.png';
import pending from '../Components/images/pending2.png';
import new_request from '../Components/images/new_request2.png';
import ReqTable from './ReqTable'
import PendingReq from "./PendingReq";
import SecondNavbar from "./SecondNavbar";
import RequestModal from "./RequestModal";
import {typeOfClass} from './Constants/Constants'

export default function VolunteerDashBoard() {
    const authStatus = useSelector((state) => state.auth);
    const userInfo = decode(authStatus.token);
    const [userData, setuserData] = useState([]);
    const [volunteerData, setvolunteerData] = useState([]);
    const [inboxData, setinboxData] = useState([]);
    const [message, setmessage] = useState("nothing");
    const [showReqTable, setShowReqTable] = useState(false);
    const [showPendingTable, setshowPendingTable] = useState(false);
    const [invitation, setinvitation] = useState([]);
    const [reqListToAccept, setreqListToAccept] = useState([]);


    useEffect(() => {
        let isMounted = true;
        if (isMounted) {
          httpClient
            .get(`requirement/get-volunteer-requirement/${userInfo.Id}`)
            .then((res) => {
              const userInfo = res.data;
              setuserData(userInfo);
            });
    
          httpClient
            .get(`requirement/get-all-volunteer-requirement/${userInfo.Id}`)
            .then((res) => {
              setvolunteerData(res.data);
            });
    
          httpClient
            .get(`RequirementTranscation/get-pending-for-volunteer/${userInfo.Id}`)
            .then((res) => {
              setinboxData(res.data);
            });
    
            httpClient
            .get(`RequirementTranscation/get-notifications/${userInfo.Id}`)
            .then((res) => {
              setinvitation(res.data);
              console.log((res.data).length );
            });
        }
        return () => {
          isMounted = false;
        }
      }, []);

      if (volunteerData !== null && inboxData !== null) {
        var result = volunteerData.filter(id =>
          inboxData.find(x => x.studentRequirementId === id.id));
          console.log(result);
      }


      const sendInvite = async (studentRequirementId, volunteerId) => {
        await httpClient
          .get(
            `RequirementTranscation/accept-student-request/${studentRequirementId}/${volunteerId}`
          )
          .then((res) => {
            setreqListToAccept(res.data);
            console.log(res.data);
          });
      };  
    return (
        <>
            <SecondNavbar 
                name = {userInfo.unique_name}
                invites = {invitation}
            />
            {/* Card  */}
            <section>
                <div className="container">
                <div className="row">
                    <div className="col-md-4 mt-4">
                    <div className="card profile-card-5" >
                        <div className="card-img-block">
                        <img className="card-img-top" src={accepted} alt="Card image cap" />
                        </div>
                        <div className="card-body pt-0">
                        <h5 className="card-title">Your Requests </h5>
                        <p className="card-text">Shows the detail view of accepted requests from volunteer.</p>
                        <button className="btn btn-primary" onClick={()=>setShowReqTable(true)}>View</button>
                        </div>
                    </div>
                    </div>
                    <div className="col-md-4 mt-4">
                    <div className="card profile-card-5" >
                        <div className="card-img-block">
                        <img className="card-img-top" src={pending} alt="Card image cap" />
                        </div>
                        <div className="card-body pt-0">
                        <h5 className="card-title">Pending Requests </h5>
                        <p className="card-text">Shows the detail view of pending requests from volunteer.</p>
                        <button  className="btn btn-primary" onClick={()=>setshowPendingTable(true)}>View</button>
                        </div>
                    </div>
                    </div>
                    <div className="col-md-4 mt-4">
                    <div className="card profile-card-5" >
                        <div className="card-img-block">
                        <img className="card-img-top" src={new_request} alt="Card image cap" />
                        </div>
                        <div className="card-body pt-0">
                        <h5 className="card-title">Add New Requriment </h5>
                        <p className="card-text">Add new requirement of your interest.</p>
                        <a href="/requirement" className="btn btn-primary">ADD</a>
                        </div>
                    </div>
                    </div>
                </div>
                </div>
            </section>

            <div className="space" style={{ "paddingTop": "20px", "paddingBottom": "20px" }}></div>

            {showReqTable ? (<ReqTable userData = {userData}/> ) : null}
            
            <div className="space" style={{ "paddingTop": "20px", "paddingBottom": "20px" }}></div>
            {showPendingTable ? (<PendingReq result={result}/>) : null}

            <div className="space" style={{ "paddingTop": "20px", "paddingBottom": "20px" }}></div>

            {message !== "nothing" ? (
                <>
                <div
                    className="alert alert-warning alert-dismissible fade show"

                    role="alert"
                >
                    <button type="button" className="close" data-dismiss="alert">&times;</button>
                    {message}
                </div>

                </>
            ) : null}

            <div className="container">
                    <div className="row">
                    <div className="col-md-offset-1 col-md-12">
                        <div className="panel">
                        <div className="panel-heading">
                            <div className="row">
                            <div className="col col-sm-3 col-xs-12">
                                <p className="text-white">You might want to take a look at these...</p>
                            </div>
                            <div className="col-sm-9 col-xs-12 text-right">
                                <div className="btn_group">
                                <input type="text" className="form-control" placeholder="Search" />
                                </div>
                            </div>
                            </div>
                        </div>
                        <div className="panel-body table-responsive">
                            <table className="table">
                            <thead>
                                <tr>
                                <th>SL No</th>
                                <th>From Volunteer</th>
                                <th>Subject</th>
                                <th>Topic</th>
                                <th>From</th>
                                <th>Till</th>
                                <th>Type of Class</th>
                                <th>Respond</th>
                                </tr>
                            </thead>
                            <tbody>
                                {volunteerData.map((item, index) => (
                                <tr>
                                    <td>{index + 1}</td>
                                    <td>{item.studentProfile.firstName}</td>
                                    <td>{item.subject}</td>
                                    <td>{item.topic}</td>
                                    <td>{item.startTime}</td>
                                    <td>{item.endTime}</td>
                                    <td>{typeOfClass[item.typeOfClass]}</td>
                                    <td>
                                    <button
                                        type="button"
                                        className="btn btn-warning"
                                        data-toggle="modal" data-target="#ModalCenter"
                                        onClick={() =>
                                        sendInvite(
                                            item.id,
                                            userInfo.Id
                                          )
                                        }
                                    >
                                        Invite
                                    </button>
                                        {reqListToAccept !== null ? (
                                          <RequestModal requests = {reqListToAccept} studentRequirementId={item.id}/>
                                        ) : null}
                                    </td>

                                </tr>
                                ))}
                            </tbody>
                            </table>
                        </div>
                        </div>
                    </div>
                    </div>
                </div>
        </>
    )
}
