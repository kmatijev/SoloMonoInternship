import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';


class FlavorForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {value: 'coconut'};
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {    this.setState({value: event.target.value});  }
  handleSubmit(event) {
    alert('Your favorite flavor is: ' + this.state.value);
    event.preventDefault();
  }

  render() {
    return (
      <form onSubmit={this.handleSubmit}>
        <label>
          Pick your favorite game:
          <select value={this.state.value} onChange={this.handleChange}>            
            <option value="Metal Gear">Metal Gear</option>
            <option value="Final Fantasy">Final Fantasy</option>
            <option value="Heat Signature">Heat Signature</option>
            <option value="Enter The Gungeon">Enter The Gungeon</option>
          </select>
        </label>
        <input type="submit" value="Submit" />
      </form>
    );
  }
}

class Button extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      random: null,
    }
  }

  min = 1;
  max = 10;

  handleClick = () => {
    this.setState({random: this.min + (Math.random() * (this.max - this.min))});
  };

  render() {
    return (
      <div>
        <button onClick={this.handleClick}>Click me to get a random number!</button>
        {this.state.random}
      </div>
    );
  }
}

export default Button;