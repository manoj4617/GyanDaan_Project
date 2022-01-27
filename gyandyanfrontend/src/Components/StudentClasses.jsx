import React, { useEffect, useState } from "react";
import { httpClient } from "../http/httpclient";
import { useSelector } from "react-redux";
import { decode } from "../utils/jwt";
import { days } from "./Constants/Constants";

export default function StudentClasses() {
  const authStatus = useSelector((state) => state.auth);
  var id = decode(authStatus.token);
  var name = id.unique_name;
  id = id.Id;

  const [studentOneReqData, setStudentOneReqData] = useState([]);
  const [studentGroupReqData, setstudentGroupReqData] = useState([]);

  useEffect(() => {
    httpClient
      .get(`RequirementTranscation/get-in-group/${id}`)
      .then((res) => {
        console.log(res);
        setstudentGroupReqData(res.data);
      })
      .catch((error) => {
        console.log(error);
      });

    httpClient
      .get(`RequirementTranscation/get-in-oneToOne/${id}`)
      .then((res) => {
        console.log(res);
        setStudentOneReqData(res.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  return (
    <>
      <h2>{name} your classes</h2>
      <div className="container h-75">
        <div className="row">
          <div className="col-md-6 p-3">
            <h4 style={{ color: "black" }}>One to One classes</h4>
            <hr />
            {studentOneReqData.map((item, index) => (
              <div className="row  justify-content-center">
                {item.studentRequirement !== null ? (
                  <>
                    <div className="col-lg-8">
                      <div className="card class-card p-3">
                        <div className="d-flex justify-content-between">
                          <div className="d-flex flex-row align-items-center">
                            <div className="ms-2 c-details">
                              <h6 className="mb-0">
                                {item.volunteerProfile.firstName}{" "}
                                {item.volunteerProfile.lastName}
                              </h6>
                              <span className="font-weight-light">
                                {item.volunteerProfile.email}
                                <br />
                                {item.volunteerProfile.mobileNumber}
                              </span>
                            </div>
                          </div>
                          <div className="badge">
                            {" "}
                            <span>Tutored</span>{" "}
                          </div>
                        </div>
                        <div className="mt-0">
                          <h4 className="heading">
                            Subject: {item.studentRequirement.subject}
                            <br />
                            Topic: {item.studentRequirement.topic}
                          </h4>
                          <div className="mt-3">
                            <p className="font-italic">
                              {days[item.studentRequirement.startDay]} to{" "}
                              {days[item.studentRequirement.endDay]}
                              <br />
                              {item.studentRequirement.startTime} to{" "}
                              {item.studentRequirement.endTime}
                            </p>
                          </div>
                        </div>
                      </div>
                    </div>
                  </>
                ) : null}
                {item.volunteerRequirement !== null ? (
                  <>
                    <div className="col-lg-8">
                      <div className="card class-card p-3">
                        <div className="d-flex justify-content-between">
                          <div className="d-flex flex-row align-items-center">
                            <div className="ms-2 c-details">
                              <h6 className="mb-0">
                                {
                                  item.volunteerRequirement.volunteerProfile
                                    .firstName
                                }{" "}
                                {
                                  item.volunteerRequirement.volunteerProfile
                                    .lastName
                                }
                              </h6>
                              <span className="font-weight-light">
                                {
                                  item.volunteerRequirement.volunteerProfile
                                    .email
                                }
                                <br />
                                {
                                  item.volunteerRequirement.volunteerProfile
                                    .mobileNumber
                                }
                              </span>
                            </div>
                          </div>
                          <div className="badge">
                            {" "}
                            <span>Tutored</span>{" "}
                          </div>
                        </div>
                        <div className="mt-0">
                          <h4 className="heading">
                            Subject: {item.volunteerRequirement.subject}
                            <br />
                            Topic: {item.volunteerRequirement.topic}
                          </h4>
                          <div className="mt-3">
                            <p className="font-italic">
                              {days[item.volunteerRequirement.startDay]} to{" "}
                              {days[item.volunteerRequirement.endDay]}
                              <br />
                              {item.volunteerRequirement.startTime} to{" "}
                              {item.volunteerRequirement.endTime}
                            </p>
                          </div>
                        </div>
                      </div>
                    </div>
                  </>
                ) : null}
              </div>
            ))}
          </div>

          <div className="col-md-6 p-3">
            <h4 style={{ color: "black" }}>Group classes</h4>
            <hr />
            {studentGroupReqData.map((item, index) => (
              <div className="row  justify-content-center">
                <div className="col-lg-8">
                  <div className="card class-card p-3">
                    <div className="d-flex justify-content-between">
                      <div className="d-flex flex-row align-items-center">
                        <div className="ms-2 c-details">
                          <h6 className="mb-0">
                            {item.volunteerProfile.firstName}{" "}
                            {item.volunteerProfile.lastName}
                          </h6>
                          <span className="font-weight-light">
                            {item.volunteerProfile.email}
                            <br />
                            {item.volunteerProfile.mobileNumber}
                          </span>
                        </div>
                      </div>
                      <div className="badge">
                        {" "}
                        <span>Tutored</span>{" "}
                      </div>
                    </div>
                    <div className="mt-0">
                    <h6 className="mb-0 fs-4">
                            {item.volunteerRequirement.subject}
                          </h6>
                          <span className="font-weight-light">
                            {item.volunteerRequirement.topic}
                          </span>
                      <p className="font-italic">
                        {days[item.volunteerRequirement.startDay]} to{" "}
                        {days[item.volunteerRequirement.endDay]}
                        <br />
                        {item.volunteerRequirement.startTime} to{" "}
                        {item.volunteerRequirement.endTime}
                      </p>
                    </div>
                    <hr />
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </>
  );
}
