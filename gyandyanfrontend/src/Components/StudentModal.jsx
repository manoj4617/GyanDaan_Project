import React, { useState } from "react";
import { typeOfClass, days } from "./Constants/Constants";
import { httpClient } from "../http/httpclient";

export default function StudentModal(props) {
  const [message, setmessage] = useState();

  const acceptinvite = (id) => {
    httpClient.get(`RequirementTranscation/accept-invite/${id}`).then((res) => {
      console.log(res.data);
      setmessage(res.data);
    });
  };

  const rejectInvite = (id) => {
    httpClient.get(`RequirementTranscation/reject-invite/${id}`).then((res) => {
      setmessage(res.data);
    });
  };

  return (
    <>
      <div
        className="modal fade"
        id="exampleModalCenter"
        tabIndex="-1"
        role="dialog"
        aria-labelledby="exampleModalCenterTitle"
        aria-hidden="true"
      >
        <div className="modal-dialog modal-dialog-centered" role="document">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title" id="exampleModalLongTitle">
                Your Invites
              </h5>
              <button
                type="button"
                className="close"
                data-dismiss="modal"
                aria-label="Close"
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div className="modal-body">
              {message != null ? (
                <h5 className="text-center font-weight-bold text-primary">
                  {message}
                </h5>
              ) : null}

              {props.invites.map((item, index) => (
                <ul class="list-group">
                  <li class="list-group-item fs-6">
                    <p>
                      You have been invited by{" "}
                      {item.volunteerRequirement.volunteerProfile.firstName} to
                      join {item.volunteerRequirement.subject}{" "}
                      {typeOfClass[item.volunteerRequirement.typeOfClass]} class
                      for your {item.studentRequirement.subject} requirement
                      which is from {days[item.volunteerRequirement.startDay]}{" "}
                      to {days[item.volunteerRequirement.endDay]} timing from{" "}
                      {item.volunteerRequirement.startTime} till{" "}
                      {item.volunteerRequirement.endTime}.
                    </p>
                    <button
                      type="button"
                      onClick={() => acceptinvite(item.id)}
                      className="btn btn-pill btn-success m-1"
                    >
                      Accept
                    </button>
                    <button
                      type="button"
                      onClick={() => rejectInvite(item.id)}
                      className="btn btn-pill btn-danger m-1"
                    >
                      Reject
                    </button>
                  </li>
                </ul>
              ))}
            </div>
            <div className="modal-footer">
              <button
                type="button"
                className="btn btn-secondary"
                data-dismiss="modal"
              >
                Close
              </button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
