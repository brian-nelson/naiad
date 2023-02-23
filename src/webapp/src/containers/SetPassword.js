import "./SetPassword.css";
import React, {Component} from "react";
import NaiadService from "../services/NaiadService";
import {Row, Col, Form, Button} from "react-bootstrap";
import { Link, Navigate } from 'react-router-dom'
import { toast } from "react-toastify";

export default class SetPassword extends Component {
  constructor(props) {
    super(props);

    let userId = this.props.params.userId;

    this.state = {
      redirect: false,
      userId: userId,
      email: "",
      password: ""
    };

    this.loadUser = this.loadUser.bind(this);
  }

  componentDidMount() {
    if (this.state.userId !== 'new') {
      this.loadUser(this.state.userId);
    }
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  loadUser(userId) {
    NaiadService.getUser(userId)
      .then(r => {
        this.setState({
          email: r.Email ?? '',
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  renderRedirect = () => {
    if (this.state.redirect) {
      return <Navigate to={`/users`} />
    }
  };

  handleSubmit = event => {
    event.preventDefault();

    try {
      let userId = this.state.userId;

      let spr = {
        NewPassword: this.state.password
      };

      NaiadService.setPassword(userId, spr)
        .then(r2 => {
          this.setState({
            redirect: true
          });
        })
        .catch(e => {
          toast.error(e.message);
        });
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
            <h3>User: {`${this.state.email}`.trim()}</h3>
          </Col>
          <Col>
            <div style={{ display: 'flex', justifyContent: 'flex-end'}}>
              <Link to={`/users`}>Return to Users</Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Form onSubmit={this.handleSubmit}>
              <Form.Group controlId="password">
                <Form.Label>User Password</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter users's password"
                  value={this.state.password}
                  onChange={this.handleChange}
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