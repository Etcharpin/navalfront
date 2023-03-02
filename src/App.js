import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Home } from './Pages/Home.js';
import {Join} from './Pages/Join.js';
import {Play1} from './Pages/Play1.js';
import {Play2} from './Pages/Play2.js';
import axios from 'axios';
import { Context } from './context/context';
import { useState } from 'react';

function App() {

const [gameid,setid] = useState("");
const [player1id,setp1id] = useState("");
const [player2id,setp2id] = useState("");

const changeid = (id) => {
  let modif = {...gameid};
  modif = id;
  setid(modif);
}

const changep1id = (id) => {
  let modif = {...player1id};
  modif = id;
  setp1id(modif);
}

const changep2id = (id) => {
  let modif = {...player2id};
  modif = id;
  setp2id(modif);
}

  return (
    <BrowserRouter>
    <Context.Provider value={[gameid, changeid,player1id,changep1id,player2id,changep2id]}>
          <main className="w-75 mx-auto p-5">
          <Routes>
              <Route>
                <Route path="*" element={<Home />} />
                <Route path="join" element={<Join />} />
                <Route path="play1" element={<Play1/>} />
                <Route path="play2" element={<Play2/>} />
              </Route>
            </Routes>
          </main>
          </Context.Provider>
      </BrowserRouter>

  );
}

export default App;
