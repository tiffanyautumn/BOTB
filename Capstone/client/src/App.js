import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router } from "react-router-dom";
import { Spinner } from 'reactstrap';
// import Header from "./components/Header";
import ApplicationViews from "./components/ApplicationViews";
import { onLoginStatusChange } from "./modules/authManager";
import { getCurrentUserByFirebaseId } from './modules/userProfileManager';
import "firebase/auth";
import Header from './components/Header';

function App() {
    const [isLoggedIn, setIsLoggedIn] = useState(null);
    const [isAdmin, setIsAdmin] = useState();

    useEffect(() => {
        onLoginStatusChange(setIsLoggedIn)
    }, []);

    useEffect(() => {
        getCurrentUserByFirebaseId()?.then((user) => {
            if (user.Role.name === "Admin") {
                setIsAdmin(true);
            } else {
                setIsAdmin(false);
            }
        });
    }, [isLoggedIn])
    /* <Header isLoggedIn={isLoggedIn} isAdmin={isAdmin} /> */
    if (isLoggedIn === null) {
        return <Spinner className="app-spinner dark" />;
    }

    return (
        <Router>
            <Header isLoggedIn={isLoggedIn} isAdmin={isAdmin} />
            <ApplicationViews isLoggedIn={isLoggedIn} isAdmin={isAdmin} />
        </Router>
    );
}

export default App;
