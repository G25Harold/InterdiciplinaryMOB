import "./index.css";
import {useNavigate} from "react-router";

export function App() {
    const navigate = useNavigate();

    return (
        <div className="app">
            <h1>Welcome To My Amazing Satin Road</h1>
            <button onClick={() => navigate("/login")}>Log In</button>
            <button onClick={() => navigate("/create-user")}>Create user</button>
        </div>
    );
}

export default App;

