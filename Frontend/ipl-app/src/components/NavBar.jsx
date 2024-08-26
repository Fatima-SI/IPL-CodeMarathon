import React from "react";
import { Link } from "react-router-dom";

import "../index.css";

const NavBar = () => {

    return (

        <nav className="navbar">
            <Link to="/" className="nav-link">Home</Link> 
            <Link to="/Top-Players" className="nav-link">Top Players</Link> 
            <Link to="/Match-Statistics" className="nav-link">Match Statistics</Link> 
            <Link to="/Matches-By-Date-Range" className="nav-link">Matches By Date Range</Link>
            <Link to="/Add-Player" className="nav-link">Add Player</Link>
        </nav>
    )
}

export default NavBar;