import { ReactDOM } from 'react';
import axios from 'axios';
import React, { Component } from 'react';
import { Input, FormGroup, Label, Table, Button, Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

class App extends Component {
  state = {
    students: [],
    grades : [],
    newGradeData: {
      name: ''
    },
    editGradeData:
    {
      id: '',
      name: ''
    },
    newGradeModal: false,
    editGradeModal: false
  }
  componentWillMount()
  {
    this._refreshGrades();
  }

  toggleNewGradeModal(){
    this.setState({
      newGradeModal: ! this.state.newGradeModal
    })
  }

  toggleEditGradeModal()
  {
    this.setState({
      editGradeModal : ! this.state.editGradeModal
    })
  }
  deleteGrade(id)
  {
    axios.delete('https://localhost:44354/api/grade/' + id).then((response) =>{
      this._refreshGrades();
    })
  }
  addGrade()
  {
    axios.post('https://localhost:44354/api/grade/' + this.state.newGradeData).then((response) => {
      let { grades } = this.state;

      grades.push(response.data);

      this.setState({grades, newGradeModal: false,     newGradeData: {
        name: ''
      }});
    })
  }
  editGrade(id, name)
  {
    this.setState({
      editGradeData: { id, name }, editGradeModal: ! this.state.editGradeModal
    });
  }
  updateGrade()
  {
    let { name } = this.state.editGradeData.name;
    axios.put('https://localhost:44354/api/grade/' + this.state.editGradeData.id, {
      name
    }).then((response) => {
      this._refreshGrades();
      
      this.setState({
        editGradeModal: false, editGradeData: {id: '', name: ''}
      })
    });
  }
  _refreshGrades(){
    axios.get('https://localhost:44354/api/grades').then((response) =>{
      this.setState({
        grades: response.data
      })
      });
  }
  render() {
    let grades = this.state.grades.map((grade) =>
    {
      return (
        <tr key = {grade.id}>
          <td>{grade.id}</td>
          <td>{grade.name}</td>
          <td>
            <Button color="success" size="sm" className ="mr" onClick={ this.editGradeData.bind(this, grade.id, grade.name)}>Edit</Button>
            <Button color="danger" size="sm" onClick={this.deleteGrade.bind(this, grade.id)}>Delete</Button>
          </td>
      </tr>
      )
    })
    return (
      <div className = "Grade container">

        <h1>Grades App</h1>
      <Button color="primary" onClick={this.toggleNewGradeModal.bind(this)}>Add a new grade</Button>
      <Modal isOpen={this.state.newGradeModal} toggle={this.toggleNewGradeModal.bind(this)}>
        <ModalHeader toggle={this.toggleNewGradeModal.bind(this)}>Add a new grade</ModalHeader>
        <ModalBody>

          <FormGroup>
            <Label for="name">Name</Label>
            <Input id="name"  value={this.state.newGradeData.name} onChange={(e) => 
            {
              let { newGradeData } = this.state;
              newGradeData.name = e.target.value;
              this.setState({ newGradeData }); // updatea state razreda aka name
            }}/>
          </FormGroup>  


        </ModalBody>
        <ModalFooter>
          <Button color="primary" onClick={this.addGrade.bind(this)}>Add Grade</Button>{' '}
          <Button color="secondary" onClick={this.toggleNewGradeModal.bind(this)}>Cancel</Button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={this.state.editGradeModal} toggle={this.toggleEditGradeModal.bind(this)}>
        <ModalHeader toggle={this.toggleEditGradeModal.bind(this)}>Edit a grade</ModalHeader>
        <ModalBody>

          <FormGroup>
            <Label for="name">Name</Label>
            <Input id="name"  value={this.state.editGradeData.name} onChange={(e) => 
            {
              let { editGradeData } = this.state;
              editGradeData.name = e.target.value;
              this.setState({ editGradeData }); // updatea state razreda aka name
            }}/>
          </FormGroup>  


        </ModalBody>
        <ModalFooter>
          <Button color="primary" onClick={this.updateGrade.bind(this)}>Update Grade</Button>{' '}
          <Button color="secondary" onClick={this.toggleEditGradeModal.bind(this)}>Cancel</Button>
        </ModalFooter>
      </Modal>
        <Table>
          <thead>
            <tr>
              <th>#</th>
              <th>Grade name</th>
            </tr>
          </thead>

          <tbody>
            {grades}
          </tbody>
        </Table>
      </div>
    );
  }
}

export default App;