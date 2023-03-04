import { Link } from 'react-router-dom';
import bato from '../battleship-large.svg';
import { Form, Button } from 'react-bootstrap';
import { useRef, useState, useEffect, useContext } from 'react';
import useForceUpdate from 'use-force-update';
import { Context } from '../context/context';



export const Home = () => {

  let s;
  const usernameRef = useRef(null);
  const [gameid, changeid, player1id, changep1id, ,] = useContext(Context);
  const [username, setUsername] = useState('');

  function strRandom(o) {
    var a = 10,
      b = 'abcdefghijklmnopqrstuvwxyz',
      c = '',
      d = 0,
      e = '' + b;
    if (o) {
      if (o.startsWithLowerCase) {
        c = b[Math.floor(Math.random() * b.length)];
        d = 1;
      }
      if (o.length) {
        a = o.length;
      }
      if (o.includeUpperCase) {
        e += b.toUpperCase();
      }
      if (o.includeNumbers) {
        e += '1234567890';
      }
    }
    for (; d < a; d++) {
      c += e[Math.floor(Math.random() * e.length)];
    }
    return c;
  }

  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ title: 'React POST Request Example' })
  };

  async function sentPostGame(rq) {


    await fetch(rq, requestOptions)
      .then(response => response.json())
      .catch(error => console.log(error));
  }

  async function sentPostPlayer(rq) {


    await fetch(rq, requestOptions)
      .then(response => response.json())
      .catch(error => console.log(error));
  }


  const handleUsernameChange = (event) => {
    setUsername(event.target.value);
  };


  const createGame = () => {
    const username = usernameRef.current.value;
    console.log(`Nom d'utilisateur : ${username}`);
    if (username !== "") {

      let id = strRandom({
        includeUpperCase: true,
        includeNumbers: true,
        length: 10,
        startsWithLowerCase: true
      })

      //let id = gameid;
      console.log(id);
      let req = 'https://localhost:7080/api/Game?gameId=' + id;
      sentPostGame(req);
      req = 'https://localhost:7080/api/Player?username=' + username + '&gameId=' + id
      sentPostPlayer(req);
      changeid(id);

    }
    else {
      console.log("manque username");
    }
  }


  return (
    <div className="Background">
      <div className="splash-container">
        <h1 className="splash-title">Battleship</h1>
        <Form.Group controlId="formUsername" className='form-home'>
          <Form.Label>Username</Form.Label>
          <Form.Control type="text" ref={usernameRef} onChange={handleUsernameChange} placeholder="Entrez votre nom d'utilisateur" />
        </Form.Group>
        <div className="btn-container">
          <div>
            {username ? (
              <Link to={{ pathname: "/play1", search: `?gameid=${encodeURIComponent(gameid)}` }}>
                <button type="button" className="btn splash-btn" onClick={createGame}>Créer</button>
              </Link>
            ) : (
              <button type="button" className="btn splash-btn" disabled>Créer</button>
            )}
          </div>
          <div>
            {username ? (
              <Link to={{ pathname: "/join", search: `?username=${encodeURIComponent(username)}` }}>
                <button type="button" className="btn splash-btn">Rejoindre</button>
              </Link>
            ) : (
              <button type="button" className="btn splash-btn" disabled>Rejoindre</button>
            )}
          </div>
        </div>
      </div>


      <img src={bato} alt="bat" className="splash-battleship-image"></img>

    </div>
  );
};


