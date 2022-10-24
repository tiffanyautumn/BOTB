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
    const [isApproved, setIsApproved] = useState()

    useEffect(() => {
        onLoginStatusChange(setIsLoggedIn)
    }, []);

    useEffect(() => {
        getCurrentUserByFirebaseId()?.then((user) => {
            if (user.role.name === "Admin") {
                setIsAdmin(true);
                setIsApproved(true)
            } else if (user.role.name === "Approved") {
                setIsAdmin(false);
                setIsApproved(true);
            } else {
                setIsAdmin(false);
                setIsApproved(false)
            }
        });
    }, [isLoggedIn])
    /* <Header isLoggedIn={isLoggedIn} isAdmin={isAdmin} /> */
    if (isLoggedIn === null) {
        return <Spinner className="app-spinner dark" />;
    }

    return (
        <Router>
            <Header isLoggedIn={isLoggedIn} isAdmin={isAdmin} isApproved={isApproved} />
            <ApplicationViews isLoggedIn={isLoggedIn} isAdmin={isAdmin} isApproved={isApproved} />
        </Router>
    );
}

export default App;
