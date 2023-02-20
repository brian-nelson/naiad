import "./User.css";
import React, {Component} from "react";
import {Row, Col, Form, Button} from "react-bootstrap";
import { Link, Navigate } from 'react-router-dom'
import { v4 as uuidv4} from 'uuid';
import {toast} from "react-toastify";
import NaiadService from "../services/NaiadService";

export default class User extends Component {
  constructor(props) {
    super(props);

    let userId = this.props.params.userId;

    this.state = {
      redirect: false,
      UserId: userId,
      Email: "",
      GivenName: "",
      FamilyName: "",
      UserRole: "ReadOnly",
      IsEnabled: true,
      MustChangePassword: false
    };

    this.loadUser = this.loadUser.bind(this);
  }

  componentDidMount() {
    if (this.state.UserId !== 'new') {
      this.loadUser(this.state.UserId);
    }
  }

  loadUser(userId) {
    NaiadService.getUser(userId)
      .then(r => {
        this.setState({
          UserId: r.Id,
          GivenName: r.GivenName ?? '',
          FamilyName: r.FamilyName ?? '',
          Email: r.Email ?? '',
          UserRole: r.UserRole ?? 'ReadOnly',
          IsEnabled: r.IsEnabled,
          MustChangePassword: r.MustChangePassword
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  handleCheckedChange = event => {
    this.setState({
      [event.target.id]: event.target.checked
    });
  };

  renderRedirect = () => {
    if (this.state.redirect) {
      return <Navigate to={`/users`} />
    }
  };

  handleSubmit = event => {
    event.preventDefault();

    try {
      let userId = this.state.UserId;

      if (userId==='new'){
        userId = uuidv4();
      }

      let givenName = this.state.GivenName;

      let familyName = this.state.FamilyName;
      if (familyName === "") {
        familyName = null;
      }

      let email = this.state.Email;
      if (email === "") {
        email = null;
      }

      let userRole = this.state.UserRole;
      if (userRole === "") {
        userRole = "ReadOnly";
      }

      let isEnabled = this.state.IsEnabled;
      if (isEnabled === "") {
        isEnabled = false;
      }

      let mustChangePassword = this.state.MustChangePassword;
      if (mustChangePassword === "") {
        mustChangePassword = false;
      }

      let user = {
        Id: userId,
        Email: email,
        GivenName: givenName,
        FamilyName: familyName,
        IsEnabled: isEnabled,
        MustChangePassword: mustChangePassword,
        UserRole: userRole
      };

      NaiadService.saveUser(user)
        .then(r => {
          this.setState({
              redirect: true
          });
        })
        .catch(e => {
          toast.error(e.message);
        })
    } catch (e) {
      toast.error(e.message);
    }
  };

  render() {
    return (
      <div className="User">
        {this.renderRedirect()}
        <Row>
          <Col>
            <h3>User: {`${this.state.GivenName} ${this.state.FamilyName}`.trim()}</h3>
          </Col>
          <Col>
            <div className="float-md-right">
              <Link to={`/users`}>Return to Users</Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Form onSubmit={this.handleSubmit}>
              <Form.Group className="mb-3" controlId="GivenName">
                <Form.Label>First Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter user first name"
                  value={this.state.GivenName}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group className="mb-3" controlId="FamilyName">
                <Form.Label>Last Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter user last name"
                  value={this.state.FamilyName}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group className="mb-3" controlId="Email">
                <Form.Label>Email</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter user's email address"
                  value={this.state.Email}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group className="mb-3" controlId="UserRole">
                <Form.Label>User Role</Form.Label>
                <Form.Select
                  onChange={this.handleChange}
                  value={this.state.UserRole}
                >
                  <option value="ReadOnly">Read Only</option>
                  <option value="ReadWrite">Read/Write</option>
                  <option value="Administrator">Administrator</option>
                </Form.Select>
              </Form.Group>
              <Form.Group className="mb-3" controlId="IsEnabled">
                <Form.Check
                  type="checkbox"
                  label="Enabled"
                  checked={this.state.IsEnabled}
                  onChange={this.handleCheckedChange}
                />
              </Form.Group>
              <div className="BottomBar">
                <Button variant="primary" type="submit">
                  Save
                </Button>
              </div>
            </Form>
          </Col>
        </Row>
      </div>
    );
  }
}