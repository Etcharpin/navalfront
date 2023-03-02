import { useRef, useEffect, useState, useContext } from 'react'
import rondu from '../rond.png'
import croix from '../croix.png'
import rondcroi from '../rondcroi.png'
import { Context } from '../context/context';



export const Play2 = () => {

    let nbbatopose = 0;
    const width = 10;
    let shipcurr;
    const [gameid,,player1id,,,] = useContext(Context);
    let [shipOnBoard,setshipOnBoard] = useState([]);
    let [shipEnemyBoard,setEnemyOnBoard] = useState([]);
    const [myId,setPlayerId] = useState(undefined);
    const [enemyid,setenemyid] = useState(undefined);
    const [p1name,setp1name] = useState('');
    const [p2name,setp2name] = useState('');
    const [gameState,setGamestate] = useState(0);
    const [myboatnb,setmyboatnb] = useState(0);
    const [enboatnb,setenboatnb] = useState(0);
    const [mystate,setmystate] = useState(false);
    const [enstate,setenstate] = useState(false);

    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ title: 'React POST Request Example' })
  };

  const requestPut = {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ title: 'React POST Request Example' })
};



  useEffect(() => {
    // si pas de donnée de jeu on fait une requete dessus
    const interval = setInterval(() => {
      if(mystate === true){
        fetchShipsOnce();
        GetgameStatus();
      }
      if(gameid !== ""){
        if (myId === undefined) {
          getMyID();
        }
        else {
          fetchShipNumberOnce();
          GetPlayerStatus(myId);
        }
        if (enemyid === undefined) {
          getEnID();
        }
        else {
          fetchEnemyShipNumberOnce();
          GetPlayerStatus(enemyid);
        }
      }
    }, 1000);
    return () => clearInterval(interval);
  
  }, [myId,mystate,shipOnBoard,enstate,myboatnb,enboatnb,gameid,enemyid]);
  
    let [shipArray,setShipArray] = useState([
      {
        name: 'destroyer',
        clas:'ship destroyer-container',
        id: 102,
        st : '<div id="cruiser-0"></div><div id="cruiser-1"></div><div id="cruiser-2"></div>',
        directions: [
          [0, 1],
          [0, width]
        ]
      },
      {
        name: 'cruiser',
        clas:'ship cruiser-container',
        id: 103,
        st : '<div id="cruiser-0"></div><div id="cruiser-1"></div><div id="cruiser-2"></div>',
        directions: [
          [0, 1, 2],
          [0, width, width*2]
        ]
      },
      {
        name: 'battleship',
        clas:'ship battleship-container',
        id: 104,
        st : '<div id="cruiser-0"></div><div id="cruiser-1"></div><div id="cruiser-2"></div>',
        directions: [
          [0, 1, 2, 3],
          [0, width, width*2, width*3]
        ]
      },
      {
        name: 'carrier',
        clas:'ship carrier-container',
        id: 105,
        st : '<div id="carrier-0"></div><div id="carrier-1"></div><div id="carrier-2"></div><div id="carrier-3"></div><div id="carrier-4"></div>',
        directions: [
          [0, 1, 2, 3, 4],
          [0, width, width*2, width*3, width*4]
        ]
      },
    ]);


    const userSquares = [];
    for (let i = 0; i < width*width; i++) {
        const square = document.createElement('div');
        square.id = i;
        userSquares.push(square);
      }

    const shotSquares = [];
      for (let i = 0; i < width*width; i++) {
          const square = document.createElement('div');
          square.id = i;
          shotSquares.push(square);
        }


    const shipvis = ((nb) =>  {
        return(
            <div dangerouslySetInnerHTML={{__html:nb}}></div>
        )
    })

    const changeShip = (ship) => {
        shipcurr = ship;
        getMyID();
        //console.log(shipcurr);
    }

    const turnboat = ((curr) => {
      const shipIndex = shipArray.findIndex(ship => ship === curr);
      if(shipIndex !== -1){
        //console.log(shipArray[shipIndex]);
        const newshiparray = [...shipArray];
        if(shipIndex === 0){
          if(newshiparray[shipIndex].clas !== "ship destroyer-container-vertical"){
            newshiparray[shipIndex].clas = "ship destroyer-container-vertical";
          }
          else{
            newshiparray[shipIndex].clas = "ship destroyer-container";
          }
        }
        if(shipIndex === 1){
          if(newshiparray[shipIndex].clas !== "ship cruiser-container-vertical"){
            
            newshiparray[shipIndex].clas = "ship cruiser-container-vertical";
          }
          else{
            newshiparray[shipIndex].clas = "ship cruiser-container";
          }
        }
        if(shipIndex === 2){
          if(newshiparray[shipIndex].clas !== "ship battleship-container-vertical"){
            newshiparray[shipIndex].clas = "ship battleship-container-vertical";
          }
          else{
           
            newshiparray[shipIndex].clas = "ship battleship-container";
          }
        }
        if(shipIndex === 3){
          if(newshiparray[shipIndex].clas !== "ship carrier-container-vertical"){
            newshiparray[shipIndex].clas = "ship carrier-container-vertical";
          }
          else{
            
            newshiparray[shipIndex].clas = "ship carrier-container";
          }
        }
        
        //console.log(newshiparray[shipIndex]);
        setShipArray(newshiparray);
      }
    })

    async function sentPostShip(rq){


    await fetch(rq,requestOptions)
    .then(response => response.json())
    .catch(error => console.log(error));
    if(myId !== undefined){
      await fetchShipsOnce(myId);
    }
    
    }

    async function sentPostShot(rq){


      await fetch(rq,requestOptions)
      .then(response => response.json())
      .catch(error => console.log(error));
      await fetchEnemyOnce(enemyid);
    }

    async function sentPostGameStatus(id){

      let req = 'https://localhost:7080/api/Status?value='+id+'&id='+gameid;
      await fetch(req,requestOptions)
      .then(response => response.json())
      .catch(error => console.log(error));
    }
    async function sentPostPlayerStatusRdy(){

      let req = 'https://localhost:7080/api/Player?ready=true&playerId='+myId;
      await fetch(req,requestPut)
      .then(response => response.json())
      .catch(error => console.log(error));
    }

    async function GetPlayerStatus(id){

      let req = ' https://localhost:7080/api/PlayerStatus?playerId='+id;
      await fetch(req)
      .then(response => response.json())
      .then(data => {
        if(id === myId){
          if(data === true){
            setmystate(data);
          }
          
        }
        if(id === enemyid){
          setenstate(data);
        }
      })
      .catch(error => {
        console.error('Une erreur s\'est produite :', error);
      });
    }

    const postplaceShip = (id) => {


    let x;
    let y;
    let type;
    let orientation;
    let playerId = myId;


        if(shipcurr !== undefined){
            x = (id%10).toString();
            y = (Math.floor(id /10)).toString();
            if(shipcurr.name === 'destroyer'){
              type = "0";
                if(shipcurr.clas === 'ship destroyer-container'){
                  orientation = "false";
                }
                else{
                  orientation = "true";
                }
            }
            if(shipcurr.name === 'cruiser'){
              type = "1";
                if(shipcurr.clas === 'ship cruiser-container'){
                  orientation = "false";
                }
                else{
                  orientation = "true";
                }

            }
            if(shipcurr.name === 'battleship'){
              type = "2";
                if(shipcurr.clas === 'ship battleship-container'){
                  orientation = "false";
                }
                else{
                  orientation = "true";
                }

            }
            if(shipcurr.name === 'carrier'){
              type = "3";
                if(shipcurr.clas === 'ship carrier-container'){
                  orientation = "false";
                }
                else{
                  orientation = "true";
                }

            }
            let requete = 'https://localhost:7080/api/Ship?x='+x+'&y='+y+'&type='+type+'&orientation='+orientation+'&playerId='+playerId;
            sentPostShip(requete);

            if(/*insertok*/true){
                const newshiparray = [...shipArray];
                const newww = newshiparray.filter((ship) => ship.id !== shipcurr.id);
                setShipArray(newww);
            }
            fetchShipNumberOnce();
            
        }
    }

  

    async function getMyID() {
      let req = 'https://localhost:7080/api/Game?gameId='+gameid;
      await fetch(req)
      .then(response => response.json())
      .then(data => {
        //console.log(data);
        if(data[1] !== undefined){
          setPlayerId(data[1].id);
          setp1name(data[1].name);
        }

        
      })
      .catch(error => {
        console.error('Une erreur s\'est produite :', error);
      });
    }

    async function getEnID() {
      let req = 'https://localhost:7080/api/Game?gameId='+gameid;
      await fetch(req)
      .then(response => response.json())
      .then(data => {
        if(data[0] !== undefined){
          setenemyid(data[0].id);
          setp2name(data[0].name);
        }
        
      })
      .catch(error => {
        console.error('Une erreur s\'est produite :', error);
      });
    }

    




    async function fetchEnemyOnce(id){

      let req = 'https://localhost:7080/api/Player?playerId='+id;
      await fetch(req)
      .then(response => response.json())
      .then(data => {
        //console.log(data);
        setEnemyCase(data);
      })
      .catch(error => {
        console.error('Une erreur s\'est produite :', error);
      });

    }



    async function fetchShipsOnce(){
      let id = myId;
      let req = 'https://localhost:7080/api/Player?playerId='+id;
      await fetch(req)
      .then(response => response.json())
      .then(data => {
        //console.log(data);
        setMyshipPlace(data);
      })
      .catch(error => {
        console.error('Une erreur s\'est produite :', error);
      });

    }

    async function fetchShipNumberOnce(){
      let id = myId;
      let req = 'https://localhost:7080/api/Ship?playerId='+id;
      await fetch(req)
      .then(response => response.json())
      .then(data => {
        //console.log(data.length);
        setmyboatnb(data.length);
      })
      .catch(error => {
        console.error('Une erreur s\'est produite :', error);
      });
    }

    function fetchEnemyShipNumberOnce(){
      let id = enemyid;
      let req = 'https://localhost:7080/api/Ship?playerId='+id;
      fetch(req)
      .then(response => response.json())
      .then(data => {
        //console.log(data);
        setenboatnb(data.length);
      })
      .catch(error => {
        console.error('Une erreur s\'est produite :', error);
      });
    }

    function GetgameStatus(){
      let id = gameid;
      let req = 'https://localhost:7080/api/Status?gameid='+id;
      fetch(req)
      .then(response => response.json())
      .then(data => {
        //console.log(data);
        setGamestate(data);
      })
      .catch(error => {
        console.error('Une erreur s\'est produite :', error);
      });
    }

    const setMyshipPlace = (data) => {
      let i = 0;
      let tab = [];
      data.forEach(element => {
        if(element.wasHit){
          if(element.isOccupied){
            tab.push(i+200);
          }
          else{
            tab.push(i+100);
          }
        }
        else{
          if(element.isOccupied){
            tab.push(i);
          }
        }
          
          i++;
      });
      setshipOnBoard(tab);
    }

    const setEnemyCase = (data) => {
      getEnID();
      let i = 0;
      let tab = [];
      data.forEach(element => {
        if(element.wasHit){
          if(element.isOccupied){
            tab.push(i);
          }
          else{
            tab.push(i+100);
          }
        }
          
          i++;
      });
      setEnemyOnBoard(tab);
    }


    const getPlaceship = (id) => {
        if(shipOnBoard !== undefined){
          if(shipOnBoard.includes(parseInt(id))){
              return <img src={rondu} key={id} height="100%" alt="rd" />;
            }
          if(shipOnBoard.includes(parseInt(id)+100)){
              return <img src={croix} key={id} height="100%" alt="rd" />;
            }
          if(shipOnBoard.includes(parseInt(id)+200)){
              return <img src={rondcroi} key={id} height="100%" alt="rd" />;
            }
          }
        else{
          return <></>
        }
    }


    const gethitedgrid = (id) => {
      //getEnID();
      if(shipEnemyBoard !== undefined){
        if(shipEnemyBoard.includes(parseInt(id))){
            return <img src={rondu} key={id} height="100%" alt="rd" />;
        }
        if(shipEnemyBoard.includes(parseInt(id)+100)){
          return <img src={croix} key={id} height="100%" alt="rd" />;
        }
      }else{
        return <></>
      }
                     
    }     


    function postshot(id){

      let x;
      let y;
      if(gameState === myId){
        console.log("bizz")
        let playerId = enemyid;
        x = (id%10).toString();
        y = (Math.floor(id /10)).toString();
        let rq = 'https://localhost:7080/api/Shot?x='+x+'&y='+y+'&playerId='+playerId;
        sentPostShot(rq);
        sentPostGameStatus(enemyid);
      }
      

      
    }


    const allship = shipArray.map((ship) => {
      //console.log(shipArray);
        return(
            <div key={ship.id} onClick={()=>(changeShip(ship))} className={ship.clas}>{shipvis(ship.st)}</div>
        )
    })
    const griduser = userSquares.map((square) => {
        return(
            <div onClick={()=>(postplaceShip(square.id))} key={square.id}>{getPlaceship(square.id)}</div>
        )
    });

    const gridshot = shotSquares.map((square) => {
        return(
            <div  onClick={()=>(postshot(square.id))} key={square.id}>{gethitedgrid(square.id)}</div>
        )
    });

    const startgame = () => {

        if(myboatnb === 4){
          sentPostPlayerStatusRdy();
        }
    }

    const connectedSty = () => {
      if(enemyid !== undefined){
        return(
          <div className="connected active">Connected</div>
        )
      }
      else{
        return(
          <div className="connected ">Connected</div>
        )
      }
    }

    const readyhim = () => {
      if(enstate === true){
        return(
          <div className="ready active">Ready</div>
        )
      }
      else{
        return(
          <div className="ready">Ready</div>
        )
      }
    }

    const readyme = () => {
      if(mystate === true){
        return(
          <div className="ready active">Ready</div>
        )
      }
      else{
        return(
          <div className="ready">Ready</div>
        )
      }
    }

    const displayMyname = () => {
      if(p1name === ''){
        return(
          <div>Player 1</div>
        )
      }
      else{
        return(
        <div>{p1name}</div>)
      }
    }

    const displayHisname = () => {
      if(p2name === ''){
        return(
          <div>Player 2</div>
        )
      }
      else{
        return(
        <div>{p2name}</div>)
      }
    }

    const Instruction = () => {
      if(mystate === false){
        return( <h3 id="whose-go" className="info-text">Place ship and start the game</h3>)
      }
      else{
        if(enstate === false){
          return(<h3 id="whose-go" className="info-text">Waiting for Player 2 to be ready</h3>)
        }
        else{
          if(myboatnb === 0){
            sentPostGameStatus(-1);
            return( <h3 id="whose-go" className="info-text">Defeat</h3>)
        }
        if(enboatnb === 0){
          return( <h3 id="whose-go" className="info-text">Victory</h3>)
      }
          if(gameState === myId){
            return( <h3 id="whose-go" className="info-text">Your turn</h3>)
          }
          if(gameState === enemyid){
           return( <h3 id="whose-go" className="info-text">His turn</h3>)
          }
        }
      }
    }


    const shipButtons = () => {

      if(mystate === false){
        return(
          <div className="setup-buttons" id="setup-buttons">
              <button  id="start" className="btn" onClick={()=>(startgame())}>Start Game</button>
              <button  id="rotate" className="btn" onClick={()=>(turnboat(shipcurr))}>Rotate Your Ships</button>
          </div>
        )
      }
      else{
        return <></>
      }
    }


    return (
        <div className="Background-image">
          <div>{gameid}</div>
        <div  className="container">
          <div className="player p1">
            {displayMyname()}
            <div className="connected active">Connected</div>
            {readyme()}
          </div>
          <div className="player p2">
            {displayHisname()}
            {connectedSty()}
            {readyhim()}
          </div>
        </div>
        <div className="container">
          <div className="battleship-grid grid-user">{griduser}</div>
          <div className="battleship-grid grid-computer">{gridshot}</div>
        </div>
        <div className="container hidden-info">
          {shipButtons()}
          {Instruction()}
          <h3 id="info" className="info-text"></h3>
        </div>
        
        <div className="container">
          <div className="grid-display">
          {allship}
          </div>
        </div>
        </div>
    );
};