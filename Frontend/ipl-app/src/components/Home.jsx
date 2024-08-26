import React from "react";
import iplpicture from "../assets/ipl.png";

import "../index.css";

const Home = () => {

    return (
        <div className="home-container">
            <h1>IPL Home Page</h1>
            <img className="home-image" src={iplpicture} alt="IPL banner" />
        </div>
    )

}

export default Home;