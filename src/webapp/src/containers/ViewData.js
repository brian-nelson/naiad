import "./DataFiles.css";
import React, {Component} from "react";
import NaiadService from "../services/NaiadService";
import {Row, Col, Table, Button} from "react-bootstrap";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";
import { GrCaretNext, GrCaretPrevious } from "react-icons/gr"

export default class ViewData extends Component {
  constructor(props) {
    super(props);

    let name = this.props.params.name;

    this.state = {
      metadataId: null,
      dataTypeName: name,
      dataTypeDescription: "",
      dataTypeRowCount: 0,
      dataTypeColumns: [],
      currentPage: 0,
      pageSize: 100,
      totalPages: 0,
      data: [],
      currentPosition: 0
    };

    this.loadDataTypeDetails = this.loadDataTypeDetails.bind(this);
    this.loadData = this.loadData.bind(this);
    this.renderTableHeader = this.renderTableHeader.bind(this);
    this.renderTableBody = this.renderTableBody.bind(this);
    this.handlePreviousPage = this.handlePreviousPage.bind(this);
    this.handleNextPage = this.handleNextPage.bind(this);
  }

  componentDidMount() {
    this.loadDataTypeDetails();
  }

  handlePreviousPage() {
    let currentPage = this.state.currentPage;

    currentPage = currentPage - 1;

    if (currentPage < 0) {
      currentPage = 0;
    }

    this.setState({
      currentPage: currentPage
    }, () => {
      this.loadData();
    })
  }

  handleNextPage() {
    let currentPage = this.state.currentPage;
    let totalPages = this.state.totalPages;

    currentPage = currentPage + 1;

    if (currentPage > totalPages) {
      currentPage = totalPages;
    }

    this.setState({
      currentPage: currentPage
    }, () => {
      this.loadData();
    })
  }

  loadData() {
    const skip = this.state.currentPage * this.state.pageSize;

    NaiadService.getStructuredData(
      this.state.metadataId,
      skip,
      this.state.pageSize)
      .then(r => {
        this.setState({
          data: r
        });
      })
      .catch(e => {
        toast(e.message);
      });
  }

  loadDataTypeDetails() {
    NaiadService.getDefinitionDetails(this.state.dataTypeName)
      .then(r => {
        const rowCount = r.RowCount;
        const totalPages = Math.ceil(rowCount / this.state.pageSize);

        this.setState({
          dataTypeDescription: r.Description,
          dataTypeRowCount: rowCount,
          dataTypeColumns: r.Columns,
          metadataId: r.MetadataId,
          currentPage: 0,
          totalPages: totalPages
        }, ()=> {
          this.loadData();
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  renderTableHeader(list) {
    if (list != null
      && list.length > 0) {
      let headers = list.map((item, i) => {
        return (
          <th>{item}</th>
        );
      })

      return (
        <thead>
          <tr>
            {headers}
          </tr>
        </thead>);
    }

    return(<thead/>);
  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {
        let columns = [];

        for(let propValue in item){

        }

        return (
          <tr key={i}>
            {columns}
          </tr>
        );
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
            <h3>Data for {this.state.dataTypeName}. </h3>
          </Col>
          <Col>
          </Col>
        </Row>
        <Row>
          <Col>
            <p>{this.state.dataTypeDescription}</p>
          </Col>
          <Col>
            <span>
              <GrCaretPrevious
                size={24}
                style={{padding:"5px"}}
                onClick={ (evt) => { this.handlePreviousPage(); }}
              />
            </span>
            <span style={{fontWeight:"bold"}}>
              Page {this.state.currentPage + 1} of {this.state.totalPages + 1}
            </span>
            <span>
              <GrCaretNext
                size={24}
                style={{padding:"5px"}}
                onClick={ (evt) => { this.handleNextPage(); }}
              />
            </span>
          </Col>
          <Col>
            <div style={{ display: 'flex', justifyContent: 'flex-end'}}>
              Row Count: {this.state.dataTypeRowCount}
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover>
              { this.renderTableHeader(this.state.dataTypeColumns) }
              { this.renderTableBody(this.state.data) }
            </Table>
          </Col>
        </Row>
      </div>);
  }
}