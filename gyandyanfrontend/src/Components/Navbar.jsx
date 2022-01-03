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
                <Nav className="fs-3 text-secondary ms-auto">
                    <Nav.Link className="text-secondary" href="#home">Home</Nav.Link>
                    <Nav.Link className="text-secondary" href="#link">Login</Nav.Link>
                    <Nav.Link className="text-secondary" href="#link">Sign Up</Nav.Link>
                    <Nav.Link className="text-secondary" href="#link">About</Nav.Link>
                </Nav>
                </Navbar.Collapse>
            </Container>
            </Navbar>
        </>
    )
}
