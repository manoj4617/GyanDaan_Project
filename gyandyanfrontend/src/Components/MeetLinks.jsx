import React from "react";
import meet from "../Components/images/meet.png";
import zoom from "../Components/images/zoom.png";
import webex from "../Components/images/webex.png";
import teams from "../Components/images/teams.png";

export default function MeetLinks() {
  return (
    <>
      <h5>Conduct Class Thourgh</h5>
      <ul className="list-group list-group-horizontal-lg">
        <li className="list-group-item bg-transparent">
          <a href="https://meet.google.com/" target="_blank">
            <img className="img-fluid" src={meet} alt="Google meet" srcset="" />
          </a>
        </li>
        <li className="list-group-item bg-transparent">
          <a
            href="https://teams.microsoft.com/?culture=en-in&country=IN&lm=deeplink&lmsrc=homePageWeb&cmpid=WebSignIn"
            target="_blank"
          >
            <img className="img-fluid" src={teams} alt="Zoom" srcset="" />
          </a>
        </li>
        <li className="list-group-item bg-transparent">
          <a href="https://zoom.us/signin" target="_blank">
            <img
              className="img-fluid"
              src={zoom}
              alt="Microsoft Teams"
              srcset=""
            />
          </a>
        </li>
        <li className="list-group-item bg-transparent">
          <a href="https://cart.webex.com/" target="_blank">
            <img
              className="img-fluid"
              src={webex}
              alt="Cisco Webex"
              srcset=""
            />
          </a>
        </li>
      </ul>
    </>
  );
}
