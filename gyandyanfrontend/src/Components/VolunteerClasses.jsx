import React, { useEffect, useState } from "react";
import { httpClient } from "../http/httpclient";
import { useSelector } from "react-redux";
import { decode } from "../utils/jwt";
import { days } from "./Constants/Constants";
import MeetLinks from "./MeetLinks";

export default function VolunteerClasses() {
  const authStatus = useSelector((state) => state.auth);
  var id = decode(authStatus.token);
  var name = id.unique_name;
  id = id.Id;

  const [volunteerReqData, setvolunteerReqData] = useState([]);
  const [checkOnetoOne, setcheckOnetoOne] = useState(false);

  useEffect(() => {
    httpClient
      .get(`requirement/get-volunteer-requirement/${id}`)
      .then((res) => {
        setvolunteerReqData(res.data);
      })
      .catch((error) => {
        console.log(error);
      });

    if (volunteerReqData) {
      volunteerReqData.forEach((element) => {
        if (element.oneToOnes !== null) {
          setcheckOnetoOne(true);
        }
      });
    }
  }, []);

  return (
    <>
      <h2>{name} your classes</h2>
      <div className="container h-75">
        <div className="row">
          <div className="col-md-6 p-3">
            <h4 style={{ color: "black" }}>One to One classes</h4>
            <hr />
            {checkOnetoOne ? (
              <div
                className="badge text-wrap"
                style={{ width: "15rem", backgroundColor: "red" }}
              >
                There is no one to one classes for you.
              </div>
            ) : null}
            {volunteerReqData.map((item, index) => (
              <div className="row  justify-content-center">
                {item.typeOfClass === 0 && item.oneToOnes !== null ? (
                  <div className="col-lg-8">
                    <div className="card class-card p-3">
                      <div className="d-flex justify-content-between">
                        <div className="d-flex flex-row align-items-center">
                          <div className="ms-2 c-details">
                            <h6 className="mb-0">
                              {item.oneToOnes.studentProfile.firstName}{" "}
                              {item.oneToOnes.studentProfile.lastName}
                            </h6>
                            <span className="font-weight-light">
                              {item.oneToOnes.studentProfile.email}
                              <br />
                              {item.oneToOnes.studentProfile.mobileNumber}
                            </span>
                          </div>
                        </div>
                        <div className="badge">
                          {" "}
                          <span>Tutoring</span>{" "}
                        </div>
                      </div>
                      <div className="mt-0">
                        <h4 className="heading">
                          Subject: {item.subject}
                          <br />
                          Topic: {item.topic}
                        </h4>
                        <div className="mt-3">
                          <p className="font-italic">
                            {days[item.startDay]} to {days[item.endDay]}
                            <br />
                            {item.startTime} to {item.endTime}
                          </p>
                        </div>
                      </div>
                      <hr />
                      <MeetLinks />
                    </div>
                  </div>
                ) : null}
              </div>
            ))}
          </div>
          <div className="col-md-6 p-3">
            <h4 style={{ color: "black" }}>Group classes</h4>
            <hr />
            {volunteerReqData.map((item, index) => (
              <div className="row  justify-content-center">
                {item.typeOfClass === 1 ? (
                  <div className="col-lg-8">
                    <div className="card class-card p-3">
                      <div className="d-flex justify-content-between">
                        <div className="d-flex flex-row align-items-center">
                          <div className="ms-2 c-details">
                            <h6 className="mb-0 fs-4">{item.subject}</h6>
                            <span className="font-weight-light">
                              {item.topic}
                            </span>
                          </div>
                        </div>
                        <div className="badge">
                          {" "}
                          <span>Tutoring</span>{" "}
                        </div>
                      </div>
                      <div className="mt-0">
                        <p className="font-italic">
                          {days[item.startDay]} to {days[item.endDay]}
                          <br />
                          {item.startTime} to {item.endTime}
                        </p>
                      </div>
                      <h4>
                        Students Enrolled{" "}
                        <span className="font-italic">
                          {" "}
                          {item.inGroupVolunteer.length}
                        </span>
                      </h4>

                      {item.inGroupVolunteer.length === 0 ? (
                        <h6 className="font-italic">No students enrolled</h6>
                      ) : null}
                      {item.inGroupVolunteer.map((groupItem, index) => (
                        <ul style={{ marginTop: "-18px" }}>
                          <li>
                            <span className="font-weight-bold">
                              {groupItem.studentProfile.firstName}{" "}
                              {groupItem.studentProfile.lastName}
                            </span>{" "}
                            <br />
                            <span className="item-except text-muted text-sm h-1x">
                              {groupItem.studentProfile.email} |{" "}
                              {groupItem.studentProfile.mobileNumber}
                            </span>
                          </li>
                        </ul>
                      ))}
                      <hr />
                      {item.inGroupVolunteer.length !== 0 ? (
                        <MeetLinks />
                      ) : null}
                    </div>
                  </div>
                ) : null}
              </div>
            ))}
          </div>
        </div>
      </div>
    </>
  );
}
