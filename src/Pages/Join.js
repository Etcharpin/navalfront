import { Link } from "react-router-dom";
import bato from '../battleship-large.svg';
import { Form, Button } from 'react-bootstrap';
import { useLocation } from 'react-router-dom';
import queryString from 'query-string';
import { useRef,useState,useContext } from 'react';
import { Context } from '../context/context';



export const Join = () => {

  const location = useLocation();
  const { username } = queryString.parse(location.search);
  const gameref = useRef(null);
  const [,changeid] = useContext(Context);
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ title: 'React POST Request Example' })
};

  async function sentPostPlayer(rq){


    await fetch(rq,requestOptions)
    .then(response => response.json())
    .catch(error => console.log(error));
    }
    var id;
    const JoinTheGame = () => {
      id = gameref.current.value;
      changeid(id);
      console.log(`Nom d'utilisateur : ${id}`);
    let req = 'https://localhost:7080/api/Player?username='+username+'&gameId='+id
    sentPostPlayer(req);

    }

    return (
        <>
        <div className="form-container">
        <Form className="form" >
        <Form.Group controlId="formGameNumber">
          <Form.Label>Game number</Form.Label>
          <Form.Control type="text" ref={gameref} />
        </Form.Group>
        <Link to={{ pathname:"/play2"}}>
        <Button variant="primary" type="submit" onClick={JoinTheGame}>
          Join
        </Button>
        </Link>
      </Form>
      <img src={bato} alt="bat" className="splash-battleship-image"></img>
      </div>
      </>
    );
};

