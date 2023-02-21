import './App.css';
import React, {Component} from "react";
import {Link, Navigate} from "react-router-dom";
import {LinkContainer} from "react-router-bootstrap";
import {Container, Nav, Navbar, NavDropdown} from "react-bootstrap";
import {ToastContainer} from 'react-toastify';
import AppRoutes from "./AppRoutes";
import NaiadService from "./services/NaiadService";
import 'react-toastify/dist/ReactToastify.css';

export default class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      redirect: false
    };
  }

  userHasAuthenticated = authenticated => {
    this.setState({isAuthenticated: authenticated}, () => {
      if (!this.state.isAuthenticated) {
        this.setState( { redirect: true}, () =>{
          this.setState({ redirect: false})
        });
      }
    });
  };

  handleLogout = event => {
    NaiadService.logout();
    this.userHasAuthenticated(false);
  };

  renderRedirect = () => {
    //Return home
    if (this.state.redirect) {
      return <Navigate to={`/`} />
    }
  };

  renderLoggedInNavBarStart() {
    return (
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="mr-auto">
          <NavDropdown title="Data" id="structured-data-dropdown">
            <NavDropdown.Item to="/definitions" as={Link}>
              Define Structured Data Type
            </NavDropdown.Item>
            <NavDropdown.Item to="/datafiles" as={Link}>
              List Data Files
            </NavDropdown.Item>
            <NavDropdown.Item to="/collections" as={Link}>
              View Collections
            </NavDropdown.Item>
          </NavDropdown>
        </Nav>
      </Navbar.Collapse>
    );
  }

  renderLoggedInNavBarEnd() {
    return (
      <Navbar.Collapse id="basic-navbar-nav" className="justify-content-end">
        <Nav>
          <Nav.Item>
            <LinkContainer to="/users">
              <Nav.Link>
                Users
              </Nav.Link>
            </LinkContainer>
          </Nav.Item>
          <Nav.Item onClick={this.handleLogout} className="justify-content-end">
            <Nav.Link>
              Logout
            </Nav.Link>
          </Nav.Item>
        </Nav>
      </Navbar.Collapse>
    );
  }

  renderNotLoggedInNavBar() {
    return (
      <Navbar.Collapse className="justify-content-end">
        <Nav className="mr-auto">
        </Nav>
        <Nav>
          <Nav.Item>
            <LinkContainer to="/login">
              <Nav.Link>Login</Nav.Link>
            </LinkContainer>
          </Nav.Item>
        </Nav>
      </Navbar.Collapse>
    );
  }

  renderNavbar() {
    return (
      <Navbar bg="light" expand="lg">
        <Container>
          <Navbar.Brand>
            <Link to="/">Naiad Data Manager</Link>
          </Navbar.Brand>
          {this.state.isAuthenticated ? this.renderLoggedInNavBarStart() : this.renderNotLoggedInNavBar()}
          {this.state.isAuthenticated ? this.renderLoggedInNavBarEnd() : ""}
        </Container>
      </Navbar>
    );
  }

  render() {
    const childProps = {
      userHasAuthenticated: this.userHasAuthenticated,
      isAuthenticated: this.state.isAuthenticated
    };

    return (
      <div className="App container">
        <ToastContainer
          position="top-center"
          autoClose={false}
          closeOnClick
        />
        { this.renderRedirect() }
        { this.renderNavbar() }
        <AppRoutes childProps={childProps}/>
      </div>
    );
  }
}
