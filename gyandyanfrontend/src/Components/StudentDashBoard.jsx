import React, { useState, useEffect } from "react";
import { useSelector } from "react-redux";
import { decode } from "../utils/jwt";
import { httpClient } from "../http/httpclient";
import accepted from '../Components/images/accepted1.png';
import pending from '../Components/images/pending2.png';
import new_request from '../Components/images/new_request2.png';
import ReqTable from './ReqTable'
import PendingReq from "./PendingReq";
import Modal from "./Modal";

export default function StudentDashBoard() {
  const authStatus = useSelector((state) => state.auth);
  const userInfo = decode(authStatus.token);
  const [userData, setuserData] = useState([]);
  const [volunteerData, setvolunteerData] = useState([]);
  const [inboxData, setinboxData] = useState([]);
  const [message, setmessage] = useState("nothing");
  const [showReqTable, setShowReqTable] = useState(false);
  const [showPendingTable, setshowPendingTable] = useState(false);
  const [invitation, setinvitation] = useState([]);

  useEffect(() => {
    let isMounted = true;
    if (isMounted) {
      httpClient
        .get(`requirement/get-student-requirement/${userInfo.Id}`)
        .then((res) => {
          const userInfo = res.data;
          setuserData(userInfo);
        });

      httpClient
        .get(`requirement/get-all-student-requirement/${userInfo.Id}`)
        .then((res) => {
          setvolunteerData(res.data);
        });

      httpClient
        .get(`RequirementTranscation/get-pending/${userInfo.Id}`)
        .then((res) => {
          setinboxData(res.data);
        });

        httpClient
        .get(`RequirementTranscation/get-invitesfor-student/${userInfo.Id}`)
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
      inboxData.find(x => x.volunteerRequirementId === id.id));
  }

  const sendRequest = async (volunteerId, requirementId, studentId) => {
    await httpClient
      .get(
        `RequirementTranscation/send-request/${volunteerId}/${requirementId}/${studentId}`
      )
      .then((res) => {
        console.log(res);
        setmessage(res.data)
        //window.location.reload();
      });
  };


  return (
    <>
      <div className="container dis-name">
        <div className="row align-items-center">
          <div className="col-6">
          <div className="card w-100 bg-transparent fs-3 m-2">
            <div className="d-inline dis-name card-body">
              Welcome {userInfo.unique_name} !
            </div>
          </div>
          </div>
          <div className="col-sm ml-5 justify-content-end">
            <a href="/update-profile" className="update-profile">Update Profile</a>
          </div>
          <div className="col-sm justify-content-center">
            <div className="d-inline">
              {Object.keys(invitation).length !== 0 ? (
                <>
                  <button type="button"  className="btn btn-primary" data-toggle="modal" data-target="#exampleModalCenter">
                    <i  className="inbox fa fa-inbox"></i>
                      <span className="counter counter-lg">{Object.keys(invitation).length}</span>&nbsp;&nbsp;
                  </button>
                  <Modal invites = {invitation}/>
                </>
                ) : (
                  <i className="inbox fa fa-inbox"></i>
                )}
            </div>
          </div>
        </div>
      </div>

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
                    <p className="text-white">You might be interested in these courses</p>
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
                      <th>Respond</th>


                    </tr>
                  </thead>
                  <tbody>
                    {volunteerData.map((item, index) => (
                      <tr>
                        <td>{index + 1}</td>
                        <td>{item.volunteerProfile.firstName}</td>
                        <td>{item.subject}</td>
                        <td>{item.topic}</td>
                        <td>
                          <button
                            type="button"
                            className="btn btn-warning"
                            onClick={() =>
                              sendRequest(
                                item.volunteerProfileId,
                                item.id,
                                userInfo.Id
                              )
                            }
                          >
                            Interested
                          </button>

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
  );
}
