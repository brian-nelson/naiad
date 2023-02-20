import "./Home.css";
import React, {Component} from "react";
import {Row, Col} from "react-bootstrap";

export default class Home extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isLoading: true,
      redirectToSetup: false
    };
  }

  componentDidMount() {

  }

  renderLander() {
    return (
      <div className="lander">
        <h1>Naiad Database Management</h1>
        <p>A scalable networked data lake.</p>
      </div>
    );
  }

  renderProjects() {
    return (
      <div className="HomePanel">
        <Row>
          <Col>
          </Col>
        </Row>
        <Row>
          <Col>
          </Col>
        </Row>
        <Row>
          <Col>
          </Col>
        </Row>
      </div>
    );
  }

  render() {
      return (
        <div className="Home">
          {this.props.isAuthenticated ? this.renderProjects() : this.renderLander()}
        </div>);
  }
}