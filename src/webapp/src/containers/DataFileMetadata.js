import "./DefineDataType.css";
import React, {Component} from "react";
import {Row, Col, Table, Button} from "react-bootstrap";
import { Link, Navigate } from 'react-router-dom'
import NaiadService from "../services/NaiadService";
import {toast} from "react-toastify";
import {BsFileEarmarkPlusFill} from "react-icons/bs";

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
        this.setState({
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
      return <Navigate to={`/datafiles`}/>
    }
  };

  renderTableHeaderApplied() {
    return (
      <thead>
      <tr>
        <th>Action</th>
        <th>Name</th>
        <th>Description</th>
        <th>MIME Type</th>
      </tr>
      </thead>);
  }

  renderTableBodyApplied(list) {
    if (list != null
      && list.length > 0) {
      let rows = list.map((item, i) => {
        let name = item.Name;
        let description = item.Description;
        let mimeType = item.MimeType;

        return (
          <tr key={item.Id}>
            <td>
              <Button>Reapply</Button>
            </td>
            <td>{name}</td>
            <td>{description}</td>
            <td>{mimeType}</td>
          </tr>);
      });

      return (
        <tbody>
        {rows}
        </tbody>);
    }
  }

  render() {
    return (
      <div className="User">
        {this.renderRedirect()}
        <Row>
          <Col>
            <h3>Metadata for: {`${this.state.StorageLocation}`.trim()}</h3>
          </Col>
          <Col>
            <div style={{display: 'flex', justifyContent: 'flex-end'}}>
              <Link to={`/datafiles`}>Return to Data Files</Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Row>
              <Col>
                <h4>Structured Data Types Applied</h4>
              </Col>
              <Col>
                <div style={{ display: 'flex', justifyContent: 'flex-end'}}>
                  {
                    //TODO - Link to new page to set a new data type
                  }
                  <Link to={`/`}>
                    <BsFileEarmarkPlusFill
                      size={24}
                    />
                  </Link>
                </div>
              </Col>
            </Row>
            <Row>
              <Col>
                <Table striped bordered hover>
                  {this.renderTableHeaderApplied()}
                  {this.renderTableBodyApplied(this.state.definitionUsed)}
                </Table>
              </Col>
            </Row>
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