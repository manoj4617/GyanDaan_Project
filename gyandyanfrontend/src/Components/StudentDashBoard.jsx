import React, { useState, useEffect } from "react";
import { useSelector } from "react-redux";
import { decode } from "../utils/jwt";
import { httpClient } from "../http/httpclient";
import { days, typeOfClass } from "./Constants/Constants";

export default function StudentDashBoard() {
  const authStatus = useSelector((state) => state.auth);
  const userInfo = decode(authStatus.token);
  const [userData, setuserData] = useState([]);
  const [volunteerData, setvolunteerData] = useState([]);
  const [inboxData, setinboxData] = useState([]);
  const [message, setmessage] = useState("nothing");

  useEffect(() => {
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
  });

  if(volunteerData !== null && inboxData !== null){
      var result = volunteerData.filter(id => 
        inboxData.find(x => x.volunteerRequirementId === id.id));

      // var volunteer = volunteerData.filter(id => inboxData.find(x => id.id !== x.volunteerRequirementId));
      // console.log(volunteer);
  }


  const sendRequest = async (volunteerId, requirementId, studentId) => {
      await httpClient
      .get(
        `RequirementTranscation/send-request/${volunteerId}/${requirementId}/${studentId}`
      )
      .then((res) => {
        console.log(res);
        setmessage(res.data);
        //window.location.reload();
      });
  };


  return (
    <>
      <div className="container">
        <div className="row">
          <div className="col-sm">
            <div className="card w-100 bg-transparent fs-3 m-2">
              <div className="dis-name card-body">
                Welcome {userInfo.unique_name} !
              </div>
            </div>
          </div>
        </div>
        {message !== "nothing" ? (
          <div
            className="alert alert-success fs-6 bg-warning w-25 mx-auto"
            role="alert"
          >
            {message}
          </div>
        ) : null}

        <div className="row justify-content-around">
          <div className="col-sm h-50 overflow-auto">
            <div className="m-2 p-1">
              <h3>Your Requirements</h3>
            </div>
            <div className="container mt-3">
              {userData === null ? (
                <div className="card w-25 mx-auto mb-3 h-25 text-center font-weight-bold fs-5">
                  <div className="card-body">No Requirement</div>
                </div>
              ) : (
                <table className="table table-dark table-striped table-hover table-borderless">
                  <thead>
                    <tr>
                      <th scope="col">Sl no:</th>
                      <th scope="col">Subject</th>
                      <th scope="col">Topic</th>
                      <th scope="col">From Day</th>
                      <th scope="col">Till day</th>
                      <th scope="col">From</th>
                      <th scope="col">Till</th>
                      <th scope="col">Type of className</th>
                      <th scope="col">Status</th>
                      <th scope="col">Edit</th>
                    </tr>
                  </thead>
                  {userData.map((item, index) => (
                    <tbody>
                      <tr>
                        <th className="p-1" scope="row">{index + 1}</th>
                        <td className="p-1">{item.subject}</td>
                        <td className="p-1">{item.topic}</td>
                        <td className="p-1">{days[item.startDay]}</td>
                        <td className="p-1">{days[item.endDay]}</td>
                        <td className="p-1">{item.startTime}</td>
                        <td className="p-1">{item.endTime}</td>
                        <td className="p-1">{typeOfClass[item.typeOfClass]}</td>
                        {item.oneToOne !== null ? (
                          <>
                          <td className="text-success p-1">
                            <button 
                              className="btn btn-success"
                              type="button">
                              Accepted
                            </button>
                          </td>
                          <td></td>
                           </>
                        ) : item.group !== null ? (
                          <>
                          <td className="text-success p-1">
                            <a
                              href="/"
                              className="text-success"
                            >
                              Accepted
                            </a>
                          </td>
                          <td></td>
                          </>
                        ) : item.oneToOne === null && item.group === null ? (
                          <>
                            <td className="text-danger p-1">Not yet Accepted</td>
                            <td><i className="fa fa-edit"></i></td>
                          </>
                        ) : null}
                      </tr>
                    </tbody>
                  ))}
                </table>
              )}
            </div>
          </div>

          <div className="m-2 p-1">
            <h3>Pending Requests</h3>
            <div className="container mt-4">
              <table className="table table-dark table-striped table-hover table-borderless">
                <thead>
                  <tr>
                    <th scope="col">Sl no:</th>
                    <th scope="col">Subject</th>
                    <th scope="col">Topic</th>
                    <th scope="col">From Day</th>
                    <th scope="col">Till day</th>
                    <th scope="col">From</th>
                    <th scope="col">Till</th>
                    <th scope="col">Type of className</th>
                    <th scope="col">Status</th>
                  </tr>
                </thead>
                {result.map((item, index) => (
                  <tbody>
                    <tr>
                      <th  className ="p-1" scope="row">{index + 1}</th>
                      <td className ="p-1" >{item.subject}</td>
                      <td className ="p-1" >{item.topic}</td>
                      <td className ="p-1" >{days[item.startDay]}</td>
                      <td className ="p-1" >{days[item.endDay]}</td>
                      <td className ="p-1" >{item.startTime}</td>
                      <td className ="p-1" >{item.endTime}</td>
                      <td className ="p-1" >{typeOfClass[item.typeOfClass]}</td>
                      <td className="p-1 text-warning">Pending</td>
                    </tr>
                  </tbody>
                ))}
              </table>
            </div>
          </div>
          
          <div className="m-2 p-1">
            <h3>You might be interested in these...</h3>
            <div className="container mt-4">
              
              <table className="table table-dark table-striped table-hover table-borderless">
                <thead>
                  <tr>
                    <th scope="col">Sl no:</th>
                    <th scope="col">From Volunteer</th>
                    <th scope="col">Subject</th>
                    <th scope="col">Topic</th>
                    <th scope="col">Respond</th>
                  </tr>
                </thead>
                {volunteerData.map((item, index) => (
                  <tbody>
                    <tr>
                      <th className ="p-1" scope="row">{index + 1}</th>
                      <th className = "p-1">{item.volunteerProfile.firstName}</th>
                      <td className ="p-1" >{item.subject}</td>
                      <td className ="p-1" >{item.topic}</td>
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
                  </tbody>
                ))}
              </table>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
