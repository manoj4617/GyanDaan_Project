import React,{useState,useEffect} from 'react'
import StudentModal from './StudentModal'
import VolunteerModal from './VolunteerModal'
import { useSelector } from "react-redux";
import { decode } from '../utils/jwt';

export default function SecondNavbar(props) {
    const [userrole, setuserrole] = useState(null);

    const authStatus = useSelector((state) => state.auth);
    const userInfo = decode(authStatus.token);
    const role = userInfo.Roles;

    useEffect(() => {
        if (role === "Student") {
            setuserrole(role);
        }
        else if (role === "Volunteer") {
            setuserrole(role);
        }
    }, [role]);
    return (
        <>
            <nav class="navbar navbar-expand-lg navbar-light">
                <p class="navbar-brand fs-4">Welcome {props.name}!!!</p>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav  ms-auto">
                    <li class="nav-item mr-5 fs-4 p-2">
                            <a class="nav-link" href="/update-profile">Update Profile</a>
                    </li>
                    <li class="nav-item m-auto">
                    {Object.keys(props.invites).length !== 0 ? (
                            <>
                                <button type="button"  className="btn btn-success" data-toggle="modal" data-target="#exampleModalCenter">
                                    Invites <span className="badge badge-pill badge-info">{Object.keys(props.invites).length}</span>
                                </button>
                                {role === "Student" ? (
                                        <StudentModal invites={props.invites}/>
                                    ) : (
                                        <VolunteerModal invites={props.invites}/>
                                    )}
                            </>
                            ) : (
                                <button type="button"  className="btn btn-success disabled" data-toggle="modal" data-target="#exampleModalCenter">
                                Invites <span className="badge badge-pill badge-info">{Object.keys(props.invites).length}</span>
                            </button>
                        )}
                    </li>
                    </ul>
                </div>
                </nav>
        </>
    )
}
