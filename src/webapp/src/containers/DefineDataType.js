import "./DefineDataType.css";
import React, {Component} from "react";
import {Row, Col, Form, Button} from "react-bootstrap";
import { Link, Navigate } from 'react-router-dom'
import {toast} from "react-toastify";
import NaiadService from "../services/NaiadService";

export default class DefineDataType extends Component {
  constructor(props) {
    super(props);

    let name = this.props.params.name;
    let isNew = false;

    if (name === "_"){
      name = "";
      isNew = true;
    }

    this.state = {
      redirect: false,
      Name: name,
      Description: "",
      MimeType: "",
      IdentifierName: "",
      IsNew: isNew
    };

    this.loadDefinition = this.loadDefinition.bind(this);
  }

  componentDidMount() {
    if (this.state.Name !== '') {
      this.loadDefinition(this.state.Name);
    }
  }

  loadDefinition(name) {
    NaiadService.getDefinition(name)
      .then(r => {
        this.setState({
          Name: r.Name,
          Description: r.Description ?? '',
          MimeType: r.MimeType ?? '',
          IdentifierName: r.IdentifierName ?? ''
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

  renderRedirect = () => {
    if (this.state.redirect) {
      return <Navigate to={`/definitions`} />
    }
  };

  handleSubmit = event => {
    event.preventDefault();

    try {
      let name = this.state.Name;
      let description = this.state.Description;
      let mimeType = this.state.MimeType;
      let identifierName = this.state.IdentifierName;

      let definition = {
        Name: name,
        Description: description,
        MimeType: mimeType,
        IdentifierName: identifierName
      };

      NaiadService.saveDefinition(definition)
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
            <h3>Data Definition: {`${this.state.Name}`.trim()}</h3>
          </Col>
          <Col>
            <div className="float-md-right">
              <Link to={`/definitions`}>Return to Data Definitions</Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Form onSubmit={this.handleSubmit}>
              <Form.Group className="mb-3" controlId="Name">
                <Form.Label>Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter data type name (only text and numbers)"
                  value={this.state.Name}
                  onChange={this.handleChange}
                  disabled={!this.state.IsNew}
                />
              </Form.Group>
              <Form.Group className="mb-3" controlId="Description">
                <Form.Label>Description</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter description"
                  value={this.state.Description}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group className="mb-3" controlId="MimeType">
                <Form.Label>Mime Type</Form.Label>
                <Form.Select
                  onChange={this.handleChange}
                  value={this.state.MimeType}
                  disabled={!this.state.IsNew}
                >
                  <option value="application/json">Json</option>
                  <option value="text/csv">CSV</option>
                </Form.Select>
              </Form.Group>
              <Form.Group className="mb-3" controlId="IdentifierName">
                <Form.Label>Identifier Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter the identifier name"
                  value={this.state.IdentifierName}
                  onChange={this.handleChange}
                  disabled={!this.state.IsNew}
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