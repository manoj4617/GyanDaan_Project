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
      <nav className="navbar navbar-expand-lg navbar-light w-100 mx-auto">
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
          <ul className="navbar-nav ms-auto align-items-center">
            {Object.keys(props.invites).length !== 0 ? (
              <>
                <li className="nav-item">
                  <a className="nav-link mx-2 " href="#!">
                    <i
                      className="fas fa-bell pe-2"
                      data-toggle="modal"
                      data-target="#exampleModalCenter"
                    >
                      <span className="badge badge-pill badge-info">
                        {Object.keys(props.invites).length}
                      </span>
                    </i>
                    Alerts
                  </a>
                </li>

                {role === "Student" ? (
                  <StudentModal invites={props.invites} />
                ) : (
                  <VolunteerModal invites={props.invites} />
                )}
              </>
            ) : (
              <li className="nav-item">
                <a className="nav-link mx-2 " href="#!">
                  <i className="fas fa-bell pe-2">
                    {Object.keys(props.invites).length}
                  </i>
                  Alerts
                </a>
              </li>
            )}

            <li className="nav-item ms-3 ">
              <a className="btn btn-black btn-rounded" href="/update-profile">
                Update Profile
              </a>
            </li>
          </ul>
        </div>
      </nav>
    </>
  );
}
