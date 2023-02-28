import "./DefineDataType.css";
import React, {Component} from "react";
import {Row, Col, Table, Button, Modal, Spinner} from "react-bootstrap";
import { Link, Navigate } from 'react-router-dom'
import NaiadService from "../services/NaiadService";
import {toast} from "react-toastify";
import {BsFileEarmarkPlusFill} from "react-icons/bs";
import Form from 'react-bootstrap/Form';

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
      definitions: [],
      showNewMappingPopup: false,
      selectedMetadataId: null,
      processingData: false
    };

    this.loadDataPointer = this.loadDataPointer.bind(this);
    this.loadDataTypes = this.loadDataTypes.bind(this);
    this.loadDataTypesUsed = this.loadDataTypesUsed.bind(this);
    this.handleReapply = this.handleReapply.bind(this);
    this.handleNewMapping = this.handleNewMapping.bind(this);
    this.handleClose = this.handleClose.bind(this);
    this.handleSelection = this.handleSelection.bind(this);
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

  handleReapply(metadataId) {
    let dataPointerId = this.state.DataPointerId;

    this.setState({
        processingData: true
      }, () =>
        NaiadService.applyConvertor(dataPointerId, metadataId)
          .then(r => {
            toast.info("Conversion applied");

            this.setState({
              processingData: false
            });

            this.loadDataTypesUsed();
          })
          .catch(e => {
            toast.error(e.message);

            this.setState({
              processingData: false
            });
          })
    );
  }

  handleNewMapping() {
    this.setState({ showNewMappingPopup: true});
  }

  handleClose() {
    if (this.state.selectedMetadataId != null) {
      this.handleReapply(this.state.selectedMetadataId);
    }

    this.setState({
      showNewMappingPopup: false,
      selectedMetadataId: null
    });
  }

  handleSelection(event) {
    let value = event.target.value;
    if (value != null) {
      this.setState({
        selectedMetadataId: value
      });
    }
  }

  renderRedirect = () => {
    if (this.state.redirect) {
      return <Navigate to={`/datafiles`}/>
    }
  };

  renderSelect(list) {
    if (list != null
      && list.length > 0) {
      let rows = list.map((item, i) => {
        return (
          <option
            value={item.MetadataId}
          >
            {item.Name}
          </option>);
      });

      return (
        <Form.Select
          onChange={this.handleSelection}
        >
          <option value={null}>Select a type</option>
        {rows}
        </Form.Select>);
    }
  }

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
          <tr key={item.MetadataId}>
            <td>
              <Button onClick={ (evt) => { this.handleReapply(item.MetadataId); }}
                      disabled={this.state.processingData}
              >Reapply</Button>
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
        <Modal
          show={this.state.showNewMappingPopup}
          onHide={this.handleClose}>
          <Modal.Header closeButton>
            <Modal.Title>Chose a data type</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            {this.renderSelect(this.state.definitions)}
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={this.handleClose}>Cancel</Button>
            <Button variant="primary" onClick={this.handleClose}>Save</Button>
          </Modal.Footer>
        </Modal>
        <Modal
          show={this.state.processingData}
          onHide={this.handleClose}>
          <Modal.Header>
            <Modal.Title>Processing Data</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Spinner animation="border" role="status" visible={this.state.processingData}>
              <span className="visually-hidden">Loading...</span>
            </Spinner>
          </Modal.Body>
        </Modal>
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
                  <BsFileEarmarkPlusFill
                    size={24}
                    onClick={ (evt) => { this.handleNewMapping(); }}
                  />
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