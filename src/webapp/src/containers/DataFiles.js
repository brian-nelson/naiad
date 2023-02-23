import "./DataFiles.css";
import React, {Component} from "react";
import NaiadService from "../services/NaiadService";
import {Row, Col, Table} from "react-bootstrap";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";

export default class DataFiles extends Component {
  constructor(props) {
    super(props);

    this.state = {
      DataFiles: [],
      downloadFileUrl: null
    };

    this.loadDataFiles = this.loadDataFiles.bind(this);
    this.renderTableBody = this.renderTableBody.bind(this);
    this.handleDownload = this.handleDownload.bind(this);
  }

  componentDidMount() {
    this.loadDataFiles();
  }

  handleDownload(filePath){
    this.setState ({fileDownloadUrl: filePath}, // Step 5
      () => {
        NaiadService.downloadDataFile(filePath)
          .then((r) => {
            let pathParts = filePath.split('/');
            let localFilename = pathParts[pathParts.length - 1];

            const url = window.URL.createObjectURL(
              new Blob([r.data], { type : 'application/octet-stream' })
            );

            const link = document.createElement('a');
            link.href = url;
            link.download = localFilename;
            document.body.appendChild(link);
            link.click();
          })
          .catch(e => {
            toast.error(e.message);
          });
      });
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
          <th>Metadata</th>
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
        let metaDataUrl = `/datafile/metadata/${item.DataPointerId}`;

        return (
          <tr key={item.Id}>
            <td>
              <button onClick={ (evt) => { this.handleDownload(item.Id); }}>Download</button>
            </td>
            <td>
              <Link to={metaDataUrl}>View</Link>
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
            <div style={{ display: 'flex', justifyContent: 'flex-end'}}>
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