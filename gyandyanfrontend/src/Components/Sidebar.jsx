import React from 'react'
import { useSelector,useDispatch } from "react-redux";
import {authSlice} from '../redux/auth'

export default function Sidebar() {

    const dispatch = useDispatch();
    const handleLogout = () => {
        sessionStorage.removeItem('token');
        dispatch(authSlice.actions.logout());
      };

    const authStatus = useSelector((state) => state.auth);

    return (
        <>
            <div className="sidebar" data-color="purple" data-background-color="black">

                {/* Tip 1: You can change the color of the sidebar using: data-color="purple | azure | green | orange | danger"

                Tip 2: you can also add an image using data-image tag */}

            <div className="logo"><a href="/" className="simple-text logo-normal">
                Gyan Dyan
                </a>
            </div>
                <div className="sidebar-wrapper">
                    <ul className="nav">
                    {!authStatus.isLoggedin ? 
                        <>
                            <li className="nav-item active  ">
                                <a className="nav-link" href="/">
                                <p>Home</p>
                                </a>
                            </li>
                            <li className="nav-item ">
                                <a className="nav-link" href="/login">
                                <p>Login</p>
                                </a>
                            </li>
                            <li className="nav-item ">
                                <a className="nav-link" href="/signup">
                                <p>Sign Up</p>
                                </a>
                            </li>
                        </>
                     : null }
                         {authStatus.isLoggedin ? 
                            <>
                                <li className="nav-item ">
                                    <a className="nav-link" href="/">
                                    <p>New Requests</p>
                                    </a>
                                </li>
                                <li className="nav-item ">
                                    <a className="nav-link" href="/">
                                    <p>Pending Requests</p>
                                    </a>
                                </li>
                                <li className="nav-item"  onClick={handleLogout}>
                                        <a className="nav-link" href="/logout">
                                        <p>Logout</p>
                                        </a>
                                </li>
                            </>
                        : null}
                        <li className="nav-item ">
                            <a className="nav-link" href="/">
                            <p>About</p>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </>
    )
}
