import React, { useState, useEffect } from "react";
import { useSelector } from "react-redux";
import { decode } from "../utils/jwt";
import { httpClient } from "../http/httpclient";
import { days, typeOfClass } from "./Constants/Constants";
import accepted from '../Components/images/accepted1.png';
import pending from '../Components/images/pending2.png';
import new_request from '../Components/images/new_request2.png';

export default function StudentDashBoard() {
  const authStatus = useSelector((state) => state.auth);
  const userInfo = decode(authStatus.token);
  const [userData, setuserData] = useState([]);
  const [volunteerData, setvolunteerData] = useState([]);
  const [inboxData, setinboxData] = useState([]);
  const [message, setmessage] = useState("nothing");



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
    <div className="row">
        <div className="col-sm">
          <div className="card w-50 bg-transparent fs-3 m-2">
            <div className="dis-name card-body">
              Welcome {userInfo.unique_name} !
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
                  <h5 className="card-title">Accepted Requests </h5>
                  <p className="card-text">Shows the detail view of accepted requests from volunteer.</p>
                  <a href="#" className="btn btn-primary">View</a>
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
                  <a href="#" className="btn btn-primary">View</a>
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

      <div className="container">
        <div className="row">
          <div className="col-md-offset-1 col-md-12">
            <div className="panel">

              <div className="panel-heading">
                <div className="row">
                  {userData === null ? (
                    <div className="col col-sm-3 col-xs-12">
                      <p className="text-white">No Requirements</p>
                    </div>
                  ) : (
                    <div className="col col-sm-3 col-xs-12">
                      <p className="text-white">Your Requirements</p>
                    </div>
                  )}


                  <div className="col-sm-9 col-xs-12 text-left">
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
                      <th>Subject</th>
                      <th>Topic</th>
                      <th>From Day</th>
                      <th>Till Day</th>
                      <th>From</th>
                      <th>Till</th>
                      <th>Type Of Class</th>
                      <th>Status</th>
                      <th>Edit</th>
                    </tr>
                  </thead>

                  <tbody>
                    {userData.map((item, index) => (
                      <tr>
                        <td>{index + 1}</td>
                        <td>{item.subject}</td>
                        <td>{item.topic}</td>
                        <td>{days[item.startDay]}</td>
                        <td>{days[item.endDay]}</td>
                        <td>{item.startTime}</td>
                        <td>{item.endTime}</td>
                        <td>{typeOfClass[item.typeOfClass]}</td>
                        {item.oneToOne != null ? (
                          <>
                            <td className="text-success p-1">
                              <button
                                className="btn btn-success"
                                type="button">
                                Accepted
                              </button>
                            </td>
                          </>
                        ) : item.group != null ? (
                          <>
                            <td className="text-success p-1">
                              <a
                                href="/"
                                className="text-success"
                              >
                                Accepted
                              </a>
                            </td>
                          </>
                        ) : item.oneToOne === null && item.group === null ? (
                          <>
                            <td className="text-warning p-1">Not yet Accepted</td>
                            <td><i className="fa fa-edit"></i></td>
                          </>
                        ) : null}
                      </tr>
                    ))}
                  </tbody>

                </table>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div className="space" style={{ "paddingTop": "20px", "paddingBottom": "20px" }}></div>

      <div className="container">
        <div className="row">
          <div className="col-md-offset-1 col-md-12">
            <div className="panel">
              <div className="panel-heading">
                <div className="row">
                  <div className="col col-sm-3 col-xs-12">
                    <p className="text-white">Pending Requests</p>
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
                      <th>Subject</th>
                      <th>Topic</th>
                      <th>From Day</th>
                      <th>Till Day</th>
                      <th>From</th>
                      <th>Till</th>
                      <th>Type Of Class</th>
                      <th>Status</th>
                      <th>View</th>


                    </tr>
                  </thead>
                  <tbody>
                    {result.map((item, index) => (
                      <tr>
                        <td>{index + 1}</td>
                        <td>{item.subject}</td>
                        <td>{item.topic}</td>
                        <td>{days[item.startDay]}</td>
                        <td>{days[item.endDay]}</td>
                        <td>{item.startTime}</td>
                        <td>{item.endTime}</td>
                        <td>{typeOfClass[item.typeOfClass]}</td>
                        <td className="text-warning">Pending</td>
                        <td><i className="fa fa-eye" aria-hidden="true" href="#"></i></td>

                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>

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
