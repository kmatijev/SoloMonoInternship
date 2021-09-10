import { ReactDOM } from 'react';
import axios from 'axios';
import React, { Component } from 'react';
import { Input, FormGroup, Label, Table, Button, Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

class App extends Component {
  state = {
   // students: [],
    grades : [],
    newGradeData: {
      id: '',
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
    this.setState((state) => ({
      newGradeModal: !state.newGradeModal
    }))
  }

  toggleEditGradeModal()
  {
    this.setState((state) => ({
      editGradeModal : !state.editGradeModal
    }))
  }
  deleteGrade(id)
  {
    axios.delete('https://localhost:44354/api/grade/' + id).then((response) =>{
      this._refreshGrades();
    })
  }
  addGrade()
  {
    axios.post('https://localhost:44354/api/grade', this.state.newGradeData).then((response) => {
      let { grades } = this.state;

      grades.push(response.data);

      this.setState({grades, newGradeModal: false,     newGradeData: {
        name: ''
      }});

      //this._refreshGrades();
    }).then((response) => { this._refreshGrades();})
  }
  editGrade(id, name)
  {
    //console.log(id, name);
    this.setState((state) => ({
      editGradeData: { id, name }, editGradeModal: !state.editGradeModal
    }));
  }
  updateGrade()
  {
    axios.put('https://localhost:44354/api/grade', this.state.editGradeData).then((response) => {
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
            <Button key = {"button1" + grade.id} color="success" size="sm" onClick={ this.editGrade.bind(this, grade.id, grade.name)}>Edit</Button>
            <Button key = {"button2" + grade.id} color="danger" size="sm" onClick={this.deleteGrade.bind(this, grade.id)}>Delete</Button>
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
            <Label for="id">ID</Label>
            <Input id="id"  value={this.state.newGradeData.id} onChange={(e) => 
            {
              let { newGradeData } = this.state;
              newGradeData.id = e.target.value;
              this.setState({ newGradeData }); // updatea state razreda aka name
            }}/>
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
          <Button color="primary" onClick={this.addGrade.bind(this)} >Add Grade</Button>
          <Button color="secondary" onClick={this.toggleNewGradeModal.bind(this)}>Cancel</Button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={this.state.editGradeModal}>
        <ModalHeader>Edit a grade</ModalHeader>
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