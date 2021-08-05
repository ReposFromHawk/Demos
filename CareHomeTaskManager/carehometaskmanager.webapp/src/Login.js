import axios from 'axios';
import React, { useState } from 'react';

const emptyUser = { userName: "", email: "", password: "", jwtToken: "" };
function Login() {
    const [user, setUser] = useState(emptyUser);
    const loginUser = () => {
        try {
            axios.post("https://localhost:44393/api/CareHomeTask/login",user)//{ "email": user.email, "JwtToken": user.jwtToken, "password": user.password }
                .then(
                    (response) => {
                        var dt = response.data;
                        dt.userName = dt.email.substring(0, dt.email.indexOf("@"));
                        setUser(dt);
                    }).catch((error) => { alert(error + " | " + error.message); });
        } catch (e) {
            console.log(e);
        }
            }
    function Logout() {
        setUser(emptyUser);
    };
    function handleSubmit(e) {
        e.preventDefault();
        loginUser();
        console.log(user.userName);
    };
    function handleChange(e) {
        e.persist();//persist the event
        setUser((currentUser) => {
            
            return {
                ...currentUser,
                [e.target.id]: e.target.value                
            };
        })
    }
    const mystyle = {
        color: "white",
        backgroundColor: "DodgerBlue",
        padding: "10px",
        margin: "5px",
        fontFamily: "Arial",
        flexDirection: "row",
        justifyContent: "flex-end",
        textAlign: 'right',
        width:"100%"
    };
    return (
        user.jwtToken ? <table style={mystyle}><th style={{ width: "80%", textAlign: "center" }}><strong>Care House Task Manager</strong></th><th> User : {user.userName} </th><th><button onClick={Logout}>Logout</button></th></table> :
                 <form onSubmit={handleSubmit}>
                <table style={mystyle}>
                    <th style={{ width: "50%", textAlign: "center" }}><strong>Care House Task Manager</strong></th>
                    <th style={{ width: "15%", textAlign: "center" }}>    Email : <input id="email" type="email" width="200px" required value={user.email} onChange={handleChange} /></th>
                    <th style={{ width: "15%", textAlign: "center" }}>   Password : <input id="password" type="password" required width="100px" value={user.password} onChange={handleChange} /></th>
                    <th style={{ width: "10%", textAlign: "center" }}><input type="Submit" value="Submit" /></th></table>
                </form>
            );
       
    

}
export default Login;