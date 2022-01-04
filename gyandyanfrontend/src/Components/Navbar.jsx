import React from 'react'
import { Navbar, Nav, Container  } from 'react-bootstrap'

export default function NavbarItem() {
    return (
        <>
          <Navbar bg="dark" expand="lg">
            <Container>
                <Navbar.Brand className="fs-2 text-primary" href="#home">Gyan Dyan</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="text-white fs-3 nav-text ms-auto">
                    <Nav.Link className="text-white" href="/">Home</Nav.Link>
                    <Nav.Link className="text-white" href="/login">Login</Nav.Link>
                    <Nav.Link className="text-white" href="/signup">Sign Up</Nav.Link>
                    <Nav.Link className="text-white" href="#link">About</Nav.Link>
                </Nav>
                </Navbar.Collapse>
            </Container>
            </Navbar>
        </>
    )
}
