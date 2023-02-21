import "./DataFiles.css";
import React, {Component} from "react";
import NaiadService from "../services/NaiadService";
import {Row, Col, Table} from "react-bootstrap";
import { toast } from "react-toastify";

export default class DataFiles extends Component {
  constructor(props) {
    super(props);

    this.state = {
      DataFiles: []
    };

    this.loadDataFiles = this.loadDataFiles.bind(this);
    this.renderTableBody = this.renderTableBody.bind(this);
  }

  componentDidMount() {
    this.loadDataFiles();
  }

  loadDataFiles() {
    NaiadService.listDataFiles(null)
      .then(r => {
        this.setState({
          DataFiles: r
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
          <th>Action</th>
          <th>Id</th>
          <th>Mime Type</th>
          <th>Size</th>
        </tr>
        </thead>);
  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {
        let name = item.Id;
        let mimeType = item.MimeType;
        let size = item.Size;

        return (
          <tr key={item.Id}>
            <td>
            </td>
            <td>{name}</td>
            <td>{mimeType}</td>
            <td>{size}</td>
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
      <div className="DefineDataTypes">
        <Row>
          <Col>
            <h3>List of Data Files</h3>
          </Col>
          <Col>
            <div className="float-md-right">
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover>
              { this.renderTableHeader() }
              { this.renderTableBody(this.state.DataFiles) }
            </Table>
          </Col>
        </Row>
      </div>);
  }
}