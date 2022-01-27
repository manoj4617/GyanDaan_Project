import React, { useState, useEffect } from "react";
import StudentModal from "./StudentModal";
import VolunteerModal from "./VolunteerModal";
import { useSelector } from "react-redux";
import { decode } from "../utils/jwt";

export default function SecondNavbar(props) {
  const [userrole, setuserrole] = useState(null);

  const authStatus = useSelector((state) => state.auth);
  const userInfo = decode(authStatus.token);
  const role = userInfo.Roles;

  useEffect(() => {
    if (role === "Student") {
      setuserrole(role);
    } else if (role === "Volunteer") {
      setuserrole(role);
    }
  }, [role]);
  return (
    <>
      <nav className="navbar navbar-expand-lg navbar-light">
        <p className="navbar-brand fs-4">Welcome {props.name}!!!</p>
        <button
          className="navbar-toggler"
          type="button"
          data-toggle="collapse"
          data-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav  ms-auto">
            <li className="nav-item m-auto">
              {Object.keys(props.invites).length !== 0 ? (
                <>
                  <button
                    type="button"
                    className="btn btn-success"
                    data-toggle="modal"
                    data-target="#exampleModalCenter"
                  >
                    Invites{" "}
                    <span className="badge badge-pill badge-info">
                      {Object.keys(props.invites).length}
                    </span>
                  </button>
                  {role === "Student" ? (
                    <StudentModal invites={props.invites} />
                  ) : (
                    <VolunteerModal invites={props.invites} />
                  )}
                </>
              ) : (
                <button
                  type="button"
                  className="btn btn-success disabled"
                  data-toggle="modal"
                  data-target="#exampleModalCenter"
                >
                  Invites{" "}
                  <span className="badge badge-pill badge-info">
                    {Object.keys(props.invites).length}
                  </span>
                </button>
              )}
            </li>
            <li className="nav-item mr-5 fs-4 p-2">
              <a className="nav-link" href="/update-profile">
                Update Profile
              </a>
            </li>
            <li className="nav-item m-auto fs-4">
              {role === "Student" ? (
                <a className="nav-link" href="/classes/student">
                Your Classes
              </a>
              ) : (
                <a className="nav-link" href="/classes/volunteer">
                Your Classes
              </a>
              )}
            </li>
          </ul>
        </div>
      </nav>
    </>
  );
}
