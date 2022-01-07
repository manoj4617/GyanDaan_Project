import React from 'react'
import { Navbar, Nav, Container  } from 'react-bootstrap'
import { useSelector,useDispatch } from "react-redux";
import {authSlice} from '../redux/auth'


export default function NavbarItem() {

    const dispatch = useDispatch();
    const handleLogout = () => {
        sessionStorage.removeItem('token');
        dispatch(authSlice.actions.logout());
      };

    const authStatus = useSelector((state) => state.auth);
    return (
        <>
          <Navbar bg="dark" expand="lg">
            <Container>
                <Navbar.Brand className="fs-2 text-primary" href="#home">Gyan Dyan</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="text-white fs-3 nav-text ms-auto">
                    {!authStatus.isLoggedin ? 
                        <>
                            <Nav.Link className="text-white" href="/">Home</Nav.Link>
                            <Nav.Link className="text-white" href="/login">Login</Nav.Link>
                            <Nav.Link className="text-white" href="/signup">Sign Up</Nav.Link>
                        </>
                     : null }
                    {authStatus.isLoggedin ? 
                            <Nav.Link className="text-white" onClick={handleLogout} href="/logout">Logout</Nav.Link>
                    : null}

                    <Nav.Link className="text-white" href="#link">About</Nav.Link>
                </Nav>
                </Navbar.Collapse>
            </Container>
            </Navbar>
        </>
    )
}
