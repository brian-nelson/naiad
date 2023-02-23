import "./Users.css";
import React, {Component} from "react";
import NaiadService from "../services/NaiadService";
import {Row, Col, Table} from "react-bootstrap";
import { BsFillPersonPlusFill } from "react-icons/bs";
import { Link } from 'react-router-dom'
import { toast } from "react-toastify";

export default class Users extends Component {
  constructor(props) {
    super(props);

    this.state = {
      users: []
    };

    this.loadUsers = this.loadUsers.bind(this);
    this.renderTableBody = this.renderTableBody.bind(this);
  }

  componentDidMount() {
    this.loadUsers();
  }

  loadUsers() {
    NaiadService.getUsers()
      .then(r => {
        this.setState({
          users: r
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  renderTableHeader() {
      return (
        <thead>
        <tr>
          <th>Email</th>
          <th>Name</th>
          <th>Role</th>
          <th>Enabled</th>
          <th>Action</th>
        </tr>
        </thead>);
  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {
        let url = `/user/${item.Id}`;
        let setPasswordUrl = `/user/setpassword/${item.Id}`

        let email = item.Email;
        let role = item.UserRole;

        let isEnabled = "No";
        if (item.IsEnabled) {
          isEnabled = "Yes";
        }

        if (role === "ReadOnly") {
          role = "Read Only";
        } else if (role === "ReadWrite") {
          role = "Read/Write";
        }

        let firstName = item.GivenName ?? '';
        let lastName = item.FamilyName ?? '';

        let name = `${firstName} ${lastName}`.trim();

        return (
          <tr key={item.Id}>
            <td>
              <Link to={url}>
                {email}
              </Link>
            </td>
            <td>{name}</td>
            <td>{role}</td>
            <td>{isEnabled}</td>
            <td>
              <Link to={setPasswordUrl}>
                Set Password
              </Link>
            </td>
          </tr>);
      });

      return (
        <tbody>
        {rows}
        </tbody>);
    }

    return(
      <tbody/>);
  }

  render() {
    return (
      <div className="Users">
        <Row>
          <Col>
            <h3>Users</h3>
          </Col>
          <Col>
            <div style={{ display: 'flex', justifyContent: 'flex-end'}}>
              <Link to={`/users/new`}>
                <BsFillPersonPlusFill
                  size={24}
                />
              </Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover>
              { this.renderTableHeader() }
              { this.renderTableBody(this.state.users) }
            </Table>
          </Col>
        </Row>
      </div>);
  }
}