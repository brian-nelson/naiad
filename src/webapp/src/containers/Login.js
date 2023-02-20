import "./Login.css";
import React, {Component} from "react";
import NaiadService from "../services/NaiadService";
import {Form} from "react-bootstrap";
import LoaderButton from "../components/LoaderButton";
import {toast} from "react-toastify";
import { Navigate } from "react-router-dom";

export default class Login extends Component {
  constructor(props) {
    super(props);

    let username = NaiadService.getCookie("username");

    this.state = {
      isLoggedIn: false,
      isLoading: false,
      username: username,
      showNewPassword: false,
      newPassword: "",
      password: ""
    };
  }

  validateForm() {
    return this.state.username.length > 0
      && this.state.password.length > 0;
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  handleSubmit = event => {
    event.preventDefault();

    try {
      NaiadService.login(this.state.username, this.state.password)
        .then(r => {
          let jwt = r.JWT;
          NaiadService.setCookie("username", this.state.username);
          NaiadService.setJwt(jwt);

          this.props.userHasAuthenticated(true);

          this.setState({ isLoggedIn : true});
        })
        .catch(e => {
          toast.error(e.message);
        })
    } catch (e) {
      toast.error(e.message);
    }
  };

  render() {
    if (this.state.isLoggedIn)
    {
      return (<Navigate to="/" replace={true} />);
    }
    else
    {
      return (
        <div className="Login">
          <Form onSubmit={this.handleSubmit}>
            <Form.Group controlId="username" size="large">
              <Form.Label>Username</Form.Label>
              <Form.Control
                autoFocus
                type="text"
                value={this.state.username}
                onChange={this.handleChange}
              />
            </Form.Group>
            <Form.Group controlId="password" size="large">
              <Form.Label>Password</Form.Label>
              <Form.Control
                value={this.state.password}
                onChange={this.handleChange}
                type="password"
              />
            </Form.Group>
            <div className="BottomBar">
              <LoaderButton
                block
                variant="secondary"
                size="large"
                disabled={!this.validateForm()}
                type="submit"
                isLoading={this.state.isLoading}
                text="Login"
                loadingText="Logging in…"
              />
            </div>
          </Form>
        </div>
      );
    }
  }
}