import "./DefineDataType.css";
import React, {Component} from "react";
import {Row, Col} from "react-bootstrap";
import { Link, Navigate } from 'react-router-dom'
import NaiadService from "../services/NaiadService";
import {toast} from "react-toastify";

export default class DataFileMetadata extends Component {
  constructor(props) {
    super(props);

    let id = this.props.params.DataPointerId;

    this.state = {
      DataPointerId: id,
      DataPointer: null,
      StorageLocation: "",
      redirect: false,
      definitionUsed: [],
      definitions: []
    };

    this.loadDataPointer = this.loadDataPointer.bind(this);
    this.loadDataTypes = this.loadDataTypes.bind(this);
    this.loadDataTypesUsed = this.loadDataTypesUsed.bind(this);
  }

  componentDidMount() {
    this.loadDataPointer();
    this.loadDataTypes();
    this.loadDataTypesUsed();
  }

  loadDataPointer() {
    NaiadService.getDataPointer(this.state.DataPointerId)
      .then(r => {
        this.setState( {
          DataPointer: r,
          StorageLocation: r.StorageLocation
        });
      })
      .catch(e => {
        toast.error(e.message);
      })
  }

  loadDataTypes() {
    NaiadService.getDefinitions()
      .then(r => {
        this.setState({
          definitions: r
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  loadDataTypesUsed() {
    NaiadService.getDefinitionsByDataPointer(this.state.DataPointerId)
      .then(r => {
        this.setState({
          definitionUsed: r
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
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
            <h3>Data Metadata: {`${this.state.StorageLocation}`.trim()}</h3>
          </Col>
          <Col>
            <div style={{ display: 'flex', justifyContent: 'flex-end'}}>
              <Link to={`/datafiles`}>Return to Data Files</Link>
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