using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Reference from Unity IDE
    public GameObject chesspiece;

    //Matrices needed, positions of each of the GameObjects
    //Also separate arrays for the players in order to easily keep track of them all
    //Keep in mind that the same objects are going to be in "positions" and "playerBlack"/"playerWhite"
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[,] positionsWall = new GameObject[8, 8];
    private GameObject[,] positionsNotUsedBlueWall = new GameObject[13, 1];
    private GameObject[,] positionsDeadBrown = new GameObject[5, 4];
    private GameObject[,] positionsDeadBlue = new GameObject[14, 4];
    public List<GameObject> playerBrown = new List<GameObject>();
    public List<GameObject> playerBlue = new List<GameObject>();

    public List<GameObject> playerBrownDeads = new List<GameObject>();
    public List<GameObject> playerBlueDeads = new List<GameObject>();

    public List<GameObject> playerBlueWalls = new List<GameObject>();
    public List<GameObject> playerBrownWalls = new List<GameObject>();


    public Button nextTurnBtn;
    public Text nextTurnTxt;

    //current turn
    private string currentPlayer = "blue";

    //Game Ending
    private bool gameOver = false;
    public int countMove = 0;

    //Unity calls this right when the game starts, there are a few built in functions
    //that Unity can call for you
    public void Start()
    {
        SetActivatedButton("false", 0);
        playerBlue.Add(Create("wallBreaker_Blue", 0, 0));
        playerBlue.Add(Create("changeMaker_Blue", 1, 0));
        playerBlue.Add(Create("overload_Blue", 2, 0));
        playerBlue.Add(Create("hatOfTheDeads_Blue", 3, 0));
        playerBlue.Add(Create("cheos_Blue", 4, 0));
        playerBlue.Add(Create("overload_Blue", 5, 0));
        playerBlue.Add(Create("changeMaker_Blue", 6, 0));
        playerBlue.Add(Create("wallBreaker_Blue", 7, 0));
        playerBlue.Add(Create("replacer_Blue", 0, 1));
        playerBlue.Add(Create("replacer_Blue", 1, 1));
        playerBlue.Add(Create("bombShield_Blue", 2, 1));
        playerBlue.Add(Create("cannon_Blue", 3, 1));
        playerBlue.Add(Create("cannon_Blue", 4, 1));
        playerBlue.Add(Create("bombShield_Blue", 5, 1));
        playerBlue.Add(Create("replacer_Blue", 6, 1));
        playerBlue.Add(Create("replacer_Blue", 7, 1));

        playerBlueWalls.Add(CreateWall("wall_Blue", 12, 0));
        playerBlueWalls.Add(CreateWall("wall_Blue", 11, 0));
        
        

        playerBrown.Add(Create("wallBreaker_Brown", 0, 7));
        playerBrown.Add(Create("changeMaker_Brown", 1, 7));
        playerBrown.Add(Create("overload_Brown", 2, 7));
        playerBrown.Add(Create("hatOfTheDeads_Brown", 3, 7));
        playerBrown.Add(Create("cheos_Brown", 4, 7));
        playerBrown.Add(Create("overload_Brown", 5, 7));
        playerBrown.Add(Create("changeMaker_Brown", 6, 7));
        playerBrown.Add(Create("wallBreaker_Brown", 7, 7));
        playerBrown.Add(Create("replacer_Brown", 0, 6));
        playerBrown.Add(Create("replacer_Brown", 1, 6));
        playerBrown.Add(Create("bombShield_Brown", 2, 6));
        playerBrown.Add(Create("cannon_Brown", 3, 6));
        playerBrown.Add(Create("cannon_Brown", 4, 6));
        playerBrown.Add(Create("bombShield_Brown", 5, 6));
        playerBrown.Add(Create("replacer_Brown", 6, 6));
        playerBrown.Add(Create("replacer_Brown", 7, 6));

        foreach (GameObject cp in playerBlue)
        {
            SetPosition(cp);
        }
        foreach (GameObject cp in playerBrown)
        {
            SetPosition(cp);
        }
        foreach (GameObject cp in playerBlueWalls)
        {
            SetPositionWallNotUsed(cp, "blue");
        }
        //foreach (GameObject cp in playerBrownWalls)
        //{
        //    SetPositionWallNotUsed(cp, "brown");
        //}

        //positionsWall = null;
    }

    public void AddPiece(GameObject obj, string player, int x, int y)
    {
        if (player == "blue")
        {
            playerBlue.Add(obj);
        }
        else
        {
            playerBrown.Add(obj);
        }
    }

    public void RemoveHat(string name, string player)
    {
        if (player == "blue")
        {
            foreach (GameObject obj in playerBlue)
            {
                if (obj.name == name)
                {
                    playerBlue.Remove(obj);
                    return;
                }
            }
        }
        else
        {
            foreach (GameObject obj in playerBrown)
            {
                if (obj.name == name)
                {
                    playerBrown.Remove(obj);
                    return;
                }
            }
        }
    }

    //internal void SetAllAttacksToFalse()
    //{
    //    throw new NotImplementedException();
    //}

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>(); //We have access to the GameObject, we need the script
        cm.name = name; //This is a built in variable that Unity has, so we did not have to declare it before
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate(); //It has everything set up so it can now Activate()
        return obj;
    }

    public GameObject CreateWall(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>(); //We have access to the GameObject, we need the script
        cm.name = name; //This is a built in variable that Unity has, so we did not have to declare it before
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.SetXBoardWall(x);
        cm.SetYBoardWall(y + 0.5f);
        cm.Activate(); //It has everything set up so it can now Activate()
        return obj;
    }



    public void SetAllMovesToFalse()
    {
        foreach (GameObject cp in playerBlue)
        {
            cp.GetComponent<Chessman>().isMove = false;
        }
        foreach (GameObject cp in playerBrown)
        {
            cp.GetComponent<Chessman>().isMove = false;
        }
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionWall(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        //Overwrites either empty space or whatever was there
        positionsWall[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void ChangeReplacerPosition(GameObject reference, GameObject enemy)
    {
        Chessman r = reference.GetComponent<Chessman>();
        Chessman en = enemy.GetComponent<Chessman>();

        positions[r.GetXBoard(), r.GetYBoard()] = enemy;
        positions[en.GetXBoard(), en.GetYBoard()] = reference;
    }

    public void ChangeHatPosition(GameObject hat, GameObject obj)
    {
        Chessman h = hat.GetComponent<Chessman>();
        Chessman o = obj.GetComponent<Chessman>();

        if(hat.name.Contains("Blue"))
        {
            positionsDeadBlue[o.GetXBoard(), o.GetYBoard()] = null;
            removeBlueObjectDead(obj.name);
            positions[h.GetXBoard(), h.GetYBoard()] = obj;
        }
        else
        {
            positionsDeadBrown[o.GetXBoard() + 5, o.GetYBoard()] = null;
            removeBrownObjectDead(obj.name);
            positions[h.GetXBoard(), h.GetYBoard()] = obj;
        }
    }

    public void removeBlueObjectDead(string name)
    {
        foreach (GameObject obj in playerBlueDeads)
        {
            if(obj.name == name)
            {
                playerBlueDeads.Remove(obj);
                return;
            }
        }
    }
    public void removeBrownObjectDead(string name)
    {
        foreach (GameObject obj in playerBrownDeads)
        {
            if (obj.name == name)
            {
                playerBrownDeads.Remove(obj);
                return;
            }
        }
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public void SetPositionWallNotUsedEmpty(int x, int y, string player)
    {
        if(player == "blue")
        {
            positionsNotUsedBlueWall[x, y] = null;
        }
        else
        {

        }
    }

    public void SetPositionEmpty(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = null;
    }
    public void SetPositionDead(GameObject obj, string player)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        if (player == "blue")
        {
            //Overwrites either empty space or whatever was there
            positionsDeadBlue[cm.GetXBoard(), cm.GetYBoard()] = obj;
        }
        else
        {
            //Overwrites either empty space or whatever was there
            positionsDeadBrown[cm.GetXBoard() + 5, cm.GetYBoard()] = obj;
        }
       
    }

    public void SetPositionWallNotUsed(GameObject wall, string player)
    {
        Chessman wl = wall.GetComponent<Chessman>();

        if (player == "blue")
        {
            //Overwrites either empty space or whatever was there
            positionsNotUsedBlueWall[wl.GetXBoard(), wl.GetYBoard()] = wall;
        }
        else
        {
            //Overwrites either empty space or whatever was there
            //positionsDeadBrown[wl.GetXBoard(), wl.GetYBoard()] = wall;
        }
    }


    public bool RemoveObject(GameObject obj, string player)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        if(obj.name.Contains("hat"))
        {
            return true;
        }


        if (player == "brown")
        {
            playerBrown.Remove(obj);

            foreach (GameObject a in playerBrownDeads)
            {
                if (obj.name == a.name)
                {
                    Destroy(obj);
                    return false;
                }
            }
            playerBrownDeads.Add(obj);
            return true;
            
            //Destroy(obj);
        }
        else
        {
            playerBlue.Remove(obj);
            foreach (GameObject a in playerBlueDeads)
            {
                if (obj.name == a.name)
                {
                    Destroy(obj);
                    return false;
                }
            }
            playerBlueDeads.Add(obj);
            return true;
            //Destroy(obj);
        }
    }
    public GameObject GetPositionDeads(int x, int y, string player)
    {
        if (player == "blue")
        {
            return positionsDeadBlue[x, y];
        }
        else
        {
            return positionsDeadBrown[x + 5, y];
        }
    }

    public void SetPositionDeadsEmpty(GameObject obj, string player)
    {
        Chessman cm = obj.GetComponent<Chessman>();
        if (player == "blue")
        {
            positionsDeadBlue[cm.GetXBoard(), cm.GetYBoard()] = null;
        }
        else
        {
            int x = cm.GetXBoard() + 5;
            positionsDeadBrown[x, cm.GetYBoard()] = null;
        }

    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public GameObject GetPositionWall(int x, int y)
    {
        
        return positionsWall[x, y];
    }

    public GameObject GetPositionWallNotUsed(int x, int y)
    {
        return positionsNotUsedBlueWall[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }


    public void NextTurn()
    {

        //obj.DestroyAttackPlates();
        //obj.DestroyMovePlates();
        //obj.DestroyReplacerPlates();


        SetActivatedButton("false", 0);
        countMove = 0;
        //this.GetComponent<Chessman>().DestroyAttackPlates();
        //this.GetComponent<Chessman>().DestroyMovePlates();
        //this.GetComponent<Chessman>().DestroyReplacerPlates();

        if (currentPlayer == "blue")
        {
            currentPlayer = "brown";
        }
        else
        {
            currentPlayer = "blue";
        }



        foreach (GameObject go in playerBlue)
        {
            if (go.GetComponent<Chessman>() !=null)
            {
                go.GetComponent<Chessman>().isMove = false;
                go.GetComponent<Chessman>().isAttack = false;
                go.GetComponent<Chessman>().isAbility = false;
                go.GetComponent<Chessman>().DestroyAttackPlates();
                go.GetComponent<Chessman>().DestroyReplacerPlates();
                go.GetComponent<Chessman>().DestroyMovePlates();
                go.GetComponent<Chessman>().DestroyCheosPlates();
                go.GetComponent<Chessman>().DestroyHatPlates();
                go.GetComponent<Chessman>().DestroyWallPlates();
                //go.GetComponent<Chessman>().isAblityReaplacer = false;
            }
        }
        foreach (GameObject g in playerBrown)
        {
            if (g.GetComponent<Chessman>())
            {
                g.GetComponent<Chessman>().isMove = false;
                g.GetComponent<Chessman>().isAttack = false;
                g.GetComponent<Chessman>().isAbility = false;
                g.GetComponent<Chessman>().DestroyAttackPlates();
                g.GetComponent<Chessman>().DestroyReplacerPlates();
                g.GetComponent<Chessman>().DestroyMovePlates();
                g.GetComponent<Chessman>().DestroyCheosPlates();
                g.GetComponent<Chessman>().DestroyHatPlates();
                g.GetComponent<Chessman>().DestroyWallPlates();
                //g.GetComponent<Chessman>().isAblityReaplacer = false;
            }
        }
    }

    public void SetActivatedButton(string state, int move)
    {
        Button b = Instantiate(nextTurnBtn, new Vector3(0, 0, -1), Quaternion.identity);
        Text t = Instantiate(nextTurnTxt, new Vector3(0, 0, -1), Quaternion.identity);
        BtnActivate btn = b.GetComponent<BtnActivate>();
        BtnActivate txt = t.GetComponent<BtnActivate>();
        btn.SetBtn(state, move);
    }
    public bool IsActivatedButton()
    {
        Button b = Instantiate(nextTurnBtn, new Vector3(0, 0, -1), Quaternion.identity);
        BtnActivate btn = b.GetComponent<BtnActivate>();
        return btn.IsActivatedBtn();
    }

    //public void NextTurn()
    //{
    //    if (currentPlayer == "blue")
    //    {
    //        currentPlayer = "brown";
    //    }
    //    else
    //    {
    //        currentPlayer = "blue";
    //    }
    //}

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            //Using UnityEngine.SceneManagement is needed here
            SceneManager.LoadScene("Game"); //Restarts the game by loading the scene over again
        }
    }
    
    public void Winner(string playerWinner)
    {
        gameOver = true;

        //Using UnityEngine.UI is needed here
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}
