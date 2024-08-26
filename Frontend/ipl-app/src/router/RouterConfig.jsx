import React from 'react';
import {BrowserRouter as Router, Routes, Route} from 'react-router-dom';
import Home from '../components/Home';
import AddPlayer from '../components/AddPlayer';
import NavBar from '../components/NavBar';
import TopPlayers from '../components/TopPlayers';
import MatchStats from '../components/MatchStats';
import MatchesByDateRange from '../components/MatchesByDateRange';

const RouterConfig = () => {

    return (
        <Router>
            <NavBar />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/Top-Players" element={<TopPlayers />} />
                <Route path="/Match-Statistics" element={<MatchStats />} />
                <Route path="/Matches-By-Date-Range" element={<MatchesByDateRange />} />
                <Route path="/Add-Player" element={<AddPlayer />} />
            </Routes>
        </Router>
    )
}

export default RouterConfig;
