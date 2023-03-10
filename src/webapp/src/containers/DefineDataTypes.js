import "./DefineDataTypes.css";
import React, {Component} from "react";
import NaiadService from "../services/NaiadService";
import {Row, Col, Table} from "react-bootstrap";
import { BsFileEarmarkPlusFill } from "react-icons/bs";
import { Link } from 'react-router-dom'
import { toast } from "react-toastify";

export default class DefineDataTypes extends Component {
  constructor(props) {
    super(props);

    this.state = {
      definitions: []
    };

    this.loadDataTypes = this.loadDataTypes.bind(this);
    this.renderTableBody = this.renderTableBody.bind(this);
  }

  componentDidMount() {
    this.loadDataTypes();
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

  renderTableHeader() {
      return (
        <thead>
        <tr>
          <th>Action</th>
          <th>Name</th>
          <th>Description</th>
          <th>Mime Type</th>
          <th>Identifier Name</th>
        </tr>
        </thead>);
  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {

        let url = `/definition/${item.Name}`;
        let name = item.Name;
        let description = item.Description;
        let mimeType = item.MimeType;
        let identifierName = item.IdentifierName;

        return (
          <tr key={item.Id}>
            <td>
              <Link to={url}>
                Edit
              </Link>
            </td>
            <td>{name}</td>
            <td>{description}</td>
            <td>{mimeType}</td>
            <td>{identifierName}</td>
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
            <h3>Structured Data Types</h3>
          </Col>
          <Col>
            <div style={{ display: 'flex', justifyContent: 'flex-end'}}>
              <Link to={`/definition/_`}>
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
              { this.renderTableHeader() }
              { this.renderTableBody(this.state.definitions) }
            </Table>
          </Col>
        </Row>
      </div>);
  }
}