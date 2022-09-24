import React,{useEffect,useState} from 'react';
import logo from './logo.svg';
import './App.css';

function App() {
  const [data,setData] = useState();
  const getData = async () => {
    let response = await fetch('http://localhost:3000/weatherforecast');
    if (response.ok) { // if HTTP-status is 200-299
    // get the response body (the method explained below)
    let json = await response.json();
    console.log("Request succeeded");
    console.log(json);
    setData(json);
    } else {
    alert("HTTP-Error: " + response.status);
    }
  };
  useEffect(() => {
     // Fetching Data on Initial Load
     getData()
  },[])
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <h1>Hello world</h1>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
