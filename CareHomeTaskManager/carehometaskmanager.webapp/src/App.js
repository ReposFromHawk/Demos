import React, { Component, useEffect } from 'react';
import CareHouseTaskList from './CareHouseTaskList';
import Login from './Login';


class App extends Component {
    constructor(props) {
        super(props);
        this.state = { CareHouseTasks:[]}
    }
    render(){
        return (
            <div className="App">
                    <Login />
                    <CareHouseTaskList CareHouseTasks={this.state.CareHouseTasks} />              
            </div>
          
        );
    }
}


export default App;
