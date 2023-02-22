import "./DefineDataType.css";
import React, {Component} from "react";
import {Row, Col} from "react-bootstrap";
import { Link, Navigate } from 'react-router-dom'

export default class DataFileMetadata extends Component {
  constructor(props) {
    super(props);

    let id = this.props;

    this.state = {
      file: id,
      redirect: false
    };
  }

  componentDidMount() {

  }

  renderRedirect = () => {
    if (this.state.redirect) {
      return <Navigate to={`/datafiles`} />
    }
  };


  render() {
    return (
      <div className="User">
        {this.renderRedirect()}
        <Row>
          <Col>
            <h3>Data Metadata: {`${this.state.Id}`.trim()}</h3>
          </Col>
          <Col>
            <div className="float-md-right">
              <Link to={`/definitions`}>Return to Data Definitions</Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
          </Col>
        </Row>
      </div>
    );
  }
}