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
  }, []);

    const sendRequest = async (volunteerId,requirementId,studentId) => {
    const response = await httpClient.get(
        `RequirementTranscation/send-request/${volunteerId}/${requirementId}/${studentId}`
    )
    .then(res => {
        console.log(res)
        setmessage(res.data);
    })}

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
            <div class="alert alert-success fs-6 bg-warning w-25 mx-auto"  role="alert">{message}</div>
        ) : null}
        <div className="row justify-content-around">
          <div className="col-sm h-50 overflow-auto">
            <div className="m-2 p-1">
              <h3>Your Requirements</h3>
            </div>
            <div className="container mt-3">
              {userData.map((item) => {
                return (
                  <div
                    className="custom-card card w-75  fs-3 m-2"
                    key={item.Id}
                  >
                    <div className="card-body" key={item.Id}>
                      <h5 className="card-title fs-2">{item.topic}</h5>
                      <hr />
                      <p className="card-text fs-5" key={item.Id}>
                        Starts on {days[item.startDay]} of every week
                      </p>
                      <p className="card-text fs-5" key={item.Id}>
                        Lasts till {days[item.endDay]} of every week
                      </p>
                      <p className="card-text fs-5" key={item.Id}>
                        Time of class {item.timeOfStart}
                      </p>
                      <p className="card-text fs-5" key={item.Id}>
                        {typeOfClass[item.typeOfClass]} class
                      </p>
                      {item.oneToOne !== null ? (
                        <p
                          key={item.Id}
                          id="accepted"
                          className="card-text fs-4"
                        >
                          One to One class with volunteer{" "}
                          {item.oneToOne.volunteerProfile.firstName}{" "}
                          {item.oneToOne.volunteerProfile.lastName}
                        </p>
                      ) : (
                        <p
                          key={item.Id}
                          id="rejected"
                          className="card-text fs-4"
                        >
                          Not Accepted by any volunteer yet
                        </p>
                      )}
                    </div>
                  </div>
                );
              })}
            </div>
          </div>
          <div className="col-sm h-50 overflow-auto">
            <div className="m-2 p-1">
              <h3>You might be interested in these...</h3>
              <div className="container mt-4">
              {volunteerData.map((item) => {
                return (
                  <div
                    className="custom-card card w-75 fs-3 m-2"
                    key={item.Id}
                  >
                    <div className="card-body" key={item.Id}>
                      <h5 className="card-title fs-2">{item.areaOfSpecialization}</h5>
                      <hr />
                      <p className="card-text fs-5" key={item.Id}>
                        Starts on {days[item.startDay]} of every week
                      </p>
                      <p className="card-text fs-5" key={item.Id}>
                        Lasts till {days[item.endDay]} of every week
                      </p>
                      <p className="card-text fs-5" key={item.Id}>
                        Timing of class from {item.startTime} to {item.endTime}
                      </p>
                      <p className="card-text fs-5" key={item.Id}>
                        {typeOfClass[item.typeOfClass]} class
                      </p>
                      <hr />
                        <div class="row justify-content-between">
                            <div class="col-4">
                                <button type="button"
                                 className="btn btn-warning"
                                 onClick={()=>sendRequest(item.volunteerProfileId,item.id,userInfo.Id)}>Interested</button>
                            </div>
                        </div>
                    </div>
                  </div>
                );
              })}
            </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
