using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    //References to objects in our Unity Scene
    public GameObject controller;
    public GameObject movePlate;
    public GameObject attackPlate;
    public GameObject replacerPlate;
    public GameObject cheosPlate;
    public GameObject hatPlate;
    public GameObject wallPlate;
    public bool isMove = false;
    public bool isAttack = false;
    public bool isAbility = false;
    public bool isSheild = true;
    //public bool isMoveWithAttack = false;
    //public bool actionAttackIsDone = false;

    //Position for this Chesspiece on the Board
    //The correct position will be set later
    private int xBoard = -1;
    private int yBoard = -1;
    private int xBoardDead = -1;
    private int yBoardDead = -1;
    private float xBoardWall = -1;
    private float yBoardWall = -1;

    //Variable for keeping track of the player it belongs to "black" or "white"
    public string player;

    //References to all the possible Sprites that this Chesspiece could be
    public Sprite bomb_Blue, bomb_Brown, bombShield_Blue, bombShield_Brown, cannon_Blue, cannon_Brown, changeMaker_Blue, changeMaker_Brown,
           cheos_Blue, cheos_Brown, hatOfTheDeads_Blue, hatOfTheDeads_Brown, overload_Blue, overload_Brown,
           replacer_Blue, replacer_Brown, wallBreaker_Blue, wallBreaker_Brown, wall_Blue, wall_Brown, hat_Blue, hat_Brown;

    public void Activate()
    {
        //Get the game controller
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        //Choose correct sprite based on piece's name
        switch (this.name)
        {
            case "bombShield_Blue": this.GetComponent<SpriteRenderer>().sprite = bombShield_Blue; player = "blue"; break;
            case "cannon_Blue": this.GetComponent<SpriteRenderer>().sprite = cannon_Blue; player = "blue"; break;
            case "changeMaker_Blue": this.GetComponent<SpriteRenderer>().sprite = changeMaker_Blue; player = "blue"; break;
            case "cheos_Blue": this.GetComponent<SpriteRenderer>().sprite = cheos_Blue; player = "blue"; break;
            case "hatOfTheDeads_Blue": this.GetComponent<SpriteRenderer>().sprite = hatOfTheDeads_Blue; player = "blue"; break;
            case "overload_Blue": this.GetComponent<SpriteRenderer>().sprite = overload_Blue; player = "blue"; break;
            case "replacer_Blue": this.GetComponent<SpriteRenderer>().sprite = replacer_Blue; player = "blue"; break;
            case "wallBreaker_Blue": this.GetComponent<SpriteRenderer>().sprite = wallBreaker_Blue; player = "blue"; break;
            case "wall_Blue": this.GetComponent<SpriteRenderer>().sprite = wall_Blue; player = "blue"; break;

            case "bombShield_Brown": this.GetComponent<SpriteRenderer>().sprite = bombShield_Brown; player = "brown"; break;
            case "cannon_Brown": this.GetComponent<SpriteRenderer>().sprite = cannon_Brown; player = "brown"; break;
            case "changeMaker_Brown": this.GetComponent<SpriteRenderer>().sprite = changeMaker_Brown; player = "brown"; break;
            case "cheos_Brown": this.GetComponent<SpriteRenderer>().sprite = cheos_Brown; player = "brown"; break;
            case "hatOfTheDeads_Brown": this.GetComponent<SpriteRenderer>().sprite = hatOfTheDeads_Brown; player = "brown"; break;
            case "overload_Brown": this.GetComponent<SpriteRenderer>().sprite = overload_Brown; player = "brown"; break;
            case "replacer_Brown": this.GetComponent<SpriteRenderer>().sprite = replacer_Brown; player = "brown"; break;
            case "wallBreaker_Brown": this.GetComponent<SpriteRenderer>().sprite = wallBreaker_Brown; player = "brown"; break;
            case "wall_Brown": this.GetComponent<SpriteRenderer>().sprite = wall_Brown; player = "brown"; break;
        }
    }

    public void SetCoordsWalls()
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard + 0.5f;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public void SetCoords()
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, -1.0f);
    }


    public void SetCoordsForDeads()
    {
        //Get the board value in order to convert to xy coords
        float x = xBoardDead;
        float y = yBoardDead;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, -1.0f);
    }


    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }
    public void SetXBoardDead(int x)
    {
        xBoardDead = x;
    }

    public void SetXBoardWall(float x)
    {
        xBoardWall = x;
    }
    public void SetYBoardWall(float y)
    {
        yBoardWall = y;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }
    public void SetYBoardDead(int y)
    {
        yBoardDead = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            if (this.name == "hat_Blue" || this.name == "hat_Brown")
            {
                DestroyAttackPlates();
                DestroyMovePlates();
                DestroyCheosPlates();
                DestroyReplacerPlates();
                DestroyHatPlates();
                DestroyWallPlates();
                InitiateHatPlates();
            }
            else
            {
                if (controller.GetComponent<Game>().countMove < 2) //The player still has moves
                {
                    if(this.name == "wall_Blue" || this.name == "wall_Brown")
                    {
                        DestroyAttackPlates();
                        DestroyMovePlates();
                        DestroyCheosPlates();
                        DestroyReplacerPlates();
                        DestroyHatPlates();
                        DestroyWallPlates();

                        if(xBoard < 0 || xBoard > 8 || yBoard < 0 || yBoard > 8)
                        {
                            Debug.Log("Enter to onmous up chessman");
                            InitiateWallPlates();
                            
                        }
                    }
                    else if (!isMove && (this.name != "wall_Blue" || this.name != "wall_Brown"))
                    {
                        //Remove all moveplates relating to previously selected piece
                        DestroyAttackPlates();
                        DestroyMovePlates();
                        DestroyCheosPlates();
                        DestroyReplacerPlates();
                        DestroyHatPlates();
                        DestroyWallPlates();

                        //Create new MovePlates
                        InitiateMovePlates();
                        //InitiateAttackPlates();
                        if (!isAttack && (this.name != "wall_Blue" || this.name != "wall_Brown"))
                        {
                            InitiateAttackPlates();
                        }
                    }
                }
                if (!isAbility && !this.name.Contains("wall"))
                {
                    DestroyReplacerPlates();
                    DestroyCheosPlates();
                    DestroyHatPlates();
                    DestroyWallPlates();

                    InitiateReplacerPlates();
                    InitiateCheosPlates();
                }
            }
        }
    }


    public void DestroyWallPlates()
    {
        GameObject[] wallPlates = GameObject.FindGameObjectsWithTag("wallPlate");
        for (int i = 0; i < wallPlates.Length; i++)
        {
            Destroy(wallPlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }

    public void InitiateHatPlates()
    {
        Game sc = controller.GetComponent<Game>();
        if (this.name.Contains("hat"))
        {
            if (this.name.Contains("Blue"))
            {
                foreach (GameObject obj in sc.playerBlueDeads)
                {
                    int x = obj.GetComponent<Chessman>().GetXBoard();
                    int y = obj.GetComponent<Chessman>().GetYBoard();
                    MovePlateHatSpawn(x, y);
                }
            }
            else
            {
                foreach (GameObject obj in sc.playerBrownDeads)
                {
                    int x = obj.GetComponent<Chessman>().GetXBoard();
                    int y = obj.GetComponent<Chessman>().GetYBoard();
                    MovePlateHatSpawn(x, y);
                }
            }
        }
    }

    public void MovePlateHatSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(hatPlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        HatPlate mpScript = mp.GetComponent<HatPlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void DestroyMovePlates()
    {
        //Destroy old MovePlates
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }
    public void DestroyHatPlates()
    {
        GameObject[] hatPlates = GameObject.FindGameObjectsWithTag("hatPlate");
        for (int i = 0; i < hatPlates.Length; i++)
        {
            Destroy(hatPlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }

    public void DestroyAttackPlates()
    {
        //Destroy old MovePlates
        GameObject[] attackPlates = GameObject.FindGameObjectsWithTag("AttackPlate");
        for (int i = 0; i < attackPlates.Length; i++)
        {
            Destroy(attackPlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }
        //actionAttackIsDone = true;
    }

    public void DestroyReplacerPlates()
    {
        GameObject[] replacerPlates = GameObject.FindGameObjectsWithTag("ReplacerPlate");
        for (int i = 0; i < replacerPlates.Length; i++)
        {
            Destroy(replacerPlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }
    public void DestroyCheosPlates()
    {
        GameObject[] cheosPlates = GameObject.FindGameObjectsWithTag("CheosPlate");
        for (int i = 0; i < cheosPlates.Length; i++)
        {
            Destroy(cheosPlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }


    public void InitiateReplacerPlates()
    {
        if(this.name.Contains("replacer"))
        {
            PointReplacrPlate(xBoard, yBoard + 1);
            PointReplacrPlate(xBoard, yBoard + 2);
            PointReplacrPlate(xBoard - 1, yBoard);
            PointReplacrPlate(xBoard - 2, yBoard);
            PointReplacrPlate(xBoard, yBoard - 1);
            PointReplacrPlate(xBoard, yBoard - 2);
            PointReplacrPlate(xBoard + 1, yBoard);
            PointReplacrPlate(xBoard + 2, yBoard);
        }
    }

    public void InitiateCheosPlates()
    {
        Game sc = controller.GetComponent<Game>();
        if (this.name.Contains("cheos"))
        {
            if(this.name.Contains("Blue"))
            {
                foreach (GameObject obj in sc.playerBlue)
                {
                    int x = obj.GetComponent<Chessman>().GetXBoard();
                    int y = obj.GetComponent<Chessman>().GetYBoard();
                    MovePlateCheosSpawn(x, y);
                }
            }
            else
            {
                foreach (GameObject obj in sc.playerBrown)
                {
                    int x = obj.GetComponent<Chessman>().GetXBoard();
                    int y = obj.GetComponent<Chessman>().GetYBoard();
                    MovePlateCheosSpawn(x, y);
                }
            }
        }
    }

    public void MovePlateCheosSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(cheosPlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        CheosPlate mpScript = mp.GetComponent<CheosPlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "wallBreaker_Blue":
            case "wallBreaker_Brown":
            case "bomb_Brown":
            case "bomb_Blue":
            case "bombShield_Brown":
            case "bombShield_Blue":
            case "bombMaker_Brown":
            case "bombMaker_Blue":
            case "bombShieldMaker_Brown":
            case "bombShieldMaker_Blue":
            case "wallBreakerMaker_Blue":
            case "wallBreakerMaker_Brown":
                SurroundMovePlate();
                SurroundMovePlate();
                SurroundMovePlate();
                SurroundMovePlate();
                SurroundMovePlate();
                SurroundMovePlate();
                SurroundMovePlate();
                SurroundMovePlate();
                break;

            case "cheos_Blue":
            case "cheos_Brown":
            case "hatOfTheDeads_Blue":
            case "hatOfTheDeads_Brown":
            case "cheosMaker_Blue":
            case "cheosMaker_Brown":
            case "hatOfTheDeadsMaker_Blue":
            case "hatOfTheDeadsMaker_Brown":
                OneMovePlate();
                break;
            case "cannon_Blue":
            case "cannon_Brown":
            case "cannonMaker_Blue":
            case "cannonMaker_Brown":
                CannonMovePlate();
                break;

            case "replacer_Blue":
            case "replacer_Brown":
            case "replacerMaker_Blue":
            case "replacerMaker_Brown":
                ReplacerMovePlate();
                break;

            case "overload_Blue":
            case "overload_Brown":
            case "overloadMaker_Blue":
            case "overloadMaker_Brown":
                OverloadMovePlate();
                break;
            case "changeMaker_Blue":
            case "changeMaker_Brown":
                SurroundMovePlate();
                break;
        }
}

    public void InitiateAttackPlates()
    {
        switch (this.name)
        {
            case "wallBreaker_Blue":
            case "wallBreaker_Brown":
            case "bomb_Brown":
            case "bomb_Blue":
            case "bombShield_Brown":
            case "bombShield_Blue":
            case "overload_Blue":
            case "overload_Brown":

            case "wallBreakerMaker_Blue":
            case "wallBreakerMaker_Brown":
            case "bombMaker_Brown":
            case "bombMaker_Blue":
            case "bombShieldMaker_Brown":
            case "bombShieldMaker_Blue":
            case "overloadMaker_Blue":
            case "overloadMaker_Brown":

            case "changeMaker_Blue":
            case "changeMaker_Brown":
            case "cheosMaker_Blue":
            case "cheosMaker_Brown":
            case "hatOfTheDeadsMaker_Blue":
            case "hatOfTheDeadsMaker_Brown":
                SurroundAttackPlate();
                SurroundAttackPlate();
                SurroundAttackPlate();
                SurroundAttackPlate();
                SurroundAttackPlate();
                SurroundAttackPlate();
                SurroundAttackPlate();
                SurroundAttackPlate();
                break;

            case "cheos_Blue":
            case "cheos_Brown":
            case "hatOfTheDeads_Blue":
            case "hatOfTheDeads_Brown":
                OneAttackPlate();
                break;
            case "cannon_Blue":
            case "cannon_Brown":
            case "cannonMaker_Blue":
            case "cannonMaker_Brown":
                CannonAttackPlate();
                break;

            case "replacer_Blue":
            case "replacer_Brown":
            case "replacerMaker_Blue":
            case "replacerMaker_Brown":
                break;
        }
    }

    public void CannonAttackPlate()
    {
        PointAttackPlate(xBoard, yBoard + 1);
        PointAttackPlate(xBoard, yBoard - 1);
        PointAttackPlate(xBoard - 1, yBoard + 0);
        PointAttackPlate(xBoard + 1, yBoard + 0);

        PointAttackPlate(xBoard, yBoard + 2);
        PointAttackPlate(xBoard, yBoard - 2);
        PointAttackPlate(xBoard - 2, yBoard + 0);
        PointAttackPlate(xBoard + 2, yBoard + 0);

        if(this.name.Contains("Maker"))
        {
            PointAttackPlate(xBoard - 1, yBoard + 1);
            PointAttackPlate(xBoard - 1, yBoard - 1);
            PointAttackPlate(xBoard + 1, yBoard + 1);
            PointAttackPlate(xBoard + 1, yBoard - 1);

            PointAttackPlate(xBoard, yBoard + 2);
            PointAttackPlate(xBoard, yBoard - 2);
            PointAttackPlate(xBoard - 2, yBoard + 0);
            PointAttackPlate(xBoard + 2, yBoard + 0);
        }
    }

    public void OneAttackPlate()
    {
        if(this.name.Contains("Maker"))
        {
            SurroundAttackPlate();
        }
        else
        {
            PointAttackPlate(xBoard, yBoard + 1);
            PointAttackPlate(xBoard, yBoard - 1);
            PointAttackPlate(xBoard - 1, yBoard + 0);
            PointAttackPlate(xBoard + 1, yBoard + 0);
        }
    }

    public void SurroundAttackPlate()
    {
        //isMoveWithAttack = false;
        PointAttackPlate(xBoard, yBoard + 1);
        PointAttackPlate(xBoard, yBoard - 1);
        PointAttackPlate(xBoard - 1, yBoard + 0);
        PointAttackPlate(xBoard - 1, yBoard - 1);
        PointAttackPlate(xBoard - 1, yBoard + 1);
        PointAttackPlate(xBoard + 1, yBoard + 0);
        PointAttackPlate(xBoard + 1, yBoard - 1);
        PointAttackPlate(xBoard + 1, yBoard + 1);
    }

    public void OverloadMovePlate()
    {
        bool isValidPlace;

        Game sc = controller.GetComponent<Game>();
        GameObject cp = sc.GetPositionWall(xBoard, yBoard);
        if (cp == null)
        {
            PointMovePlate(xBoard, yBoard + 1, false);
            PointMovePlate(xBoard - 1, yBoard + 1, false);
            PointMovePlate(xBoard + 1, yBoard + 1, false);
        }
        if (yBoard != 0)
        {
            cp = sc.GetPositionWall(xBoard, yBoard - 1);
            if (cp == null)
            {
                PointMovePlate(xBoard, yBoard - 1, false);
                PointMovePlate(xBoard - 1, yBoard - 1, false);
                PointMovePlate(xBoard + 1, yBoard - 1, false);
            }
        }


        //isMoveWithAttack = false;
        PointMovePlate(xBoard - 1, yBoard + 0, false);
        PointMovePlate(xBoard + 1, yBoard + 0, false);

        //When the palyer made two moves

        isValidPlace = checkValidMove(xBoard, yBoard + 1);
        if (isValidPlace)
        {
            PointMovePlate(xBoard, yBoard + 2, false);
        }
        isValidPlace = checkValidMove(xBoard, yBoard - 1);
        if (isValidPlace)
        {
            PointMovePlate(xBoard, yBoard - 2, false);
        }
        isValidPlace = checkValidMove(xBoard + 1, yBoard);
        if (isValidPlace)
        {
            PointMovePlate(xBoard + 2, yBoard, false);
        }
        isValidPlace = checkValidMove(xBoard -1, yBoard);
        if (isValidPlace)
        {
            PointMovePlate(xBoard -2, yBoard, false);
        }


        isValidPlace = checkValidMove(xBoard + 1, yBoard + 1);
        if (isValidPlace)
        {
            PointMovePlate(xBoard + 2, yBoard + 2, false);
        }
        isValidPlace = checkValidMove(xBoard - 1, yBoard - 1);
        if (isValidPlace)
        {
            PointMovePlate(xBoard -2, yBoard - 2, false);
        }
        isValidPlace = checkValidMove(xBoard - 1, yBoard + 1);
        if (isValidPlace)
        {
            PointMovePlate(xBoard -2, yBoard + 2, false);
        }
        isValidPlace = checkValidMove(xBoard + 1, yBoard - 1);
        if (isValidPlace)
        {
            PointMovePlate(xBoard + 2, yBoard - 2, false);
        }
    }

    public bool checkValidMove(int x, int y)
    {
       
        
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            if (y > 0)
            {
                GameObject cw1 = sc.GetPositionWall(x , y - 1);
                GameObject cw2 = sc.GetPositionWall(x , y);
                if (cp == null && cw1 == null && cw2 == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (cp == null)
                {
                    return true;
                }
                return false;
            }
        }
        return false;
    }

    public void ReplacerMovePlate()
    {
        //isMoveWithAttack = false;
        Game sc = controller.GetComponent<Game>();
        GameObject cp = sc.GetPositionWall(xBoard, yBoard);
        if (cp == null)
        {
            PointMovePlate(xBoard, yBoard + 1, false);
        }
        if (yBoard != 0)
        {
            cp = sc.GetPositionWall(xBoard, yBoard - 1);
            if (cp == null)
            {
                PointMovePlate(xBoard, yBoard - 1, false);
            }
        }

        PointMovePlate(xBoard - 1, yBoard + 0, false);
        PointMovePlate(xBoard + 1, yBoard + 0, false);
        if(this.name.Contains("Maker"))
        {
            cp = sc.GetPositionWall(xBoard, yBoard);
            if (cp == null)
            {
                PointMovePlate(xBoard - 1, yBoard + 1, false);
                PointMovePlate(xBoard + 1, yBoard + 1, false);
            }
            if (yBoard != 0)
            {
                cp = sc.GetPositionWall(xBoard, yBoard - 1);
                if (cp == null)
                {
                    PointMovePlate(xBoard - 1, yBoard - 1, false);
                    PointMovePlate(xBoard + 1, yBoard - 1, false);
                }
            }
        }

        bool isValidPlace;

        isValidPlace = checkValidMove(xBoard, yBoard + 1);
        if (isValidPlace)
        {
            PointMovePlate(xBoard, yBoard + 2, false);
        }
        isValidPlace = checkValidMove(xBoard, yBoard - 1);
        if (isValidPlace)
        {
            PointMovePlate(xBoard, yBoard - 2, false);
        }
        isValidPlace = checkValidMove(xBoard + 1, yBoard);
        if (isValidPlace)
        {
            PointMovePlate(xBoard + 2, yBoard, false);
        }
        isValidPlace = checkValidMove(xBoard - 1, yBoard);
        if (isValidPlace)
        {
            PointMovePlate(xBoard - 2, yBoard, false);
        }
    }

    public void OneMovePlate()
    {
        if (this.name.Contains("Maker"))
        {
            SurroundMovePlate();
        }
        else
        {
            Game sc = controller.GetComponent<Game>();
            GameObject cp = sc.GetPositionWall(xBoard, yBoard);
            if (cp == null)
            {
                PointMovePlate(xBoard, yBoard + 1, false);
            }
            if (yBoard != 0)
            {
                cp = sc.GetPositionWall(xBoard, yBoard - 1);
                if (cp == null)
                {
                    PointMovePlate(xBoard, yBoard - 1, false);
                }
            }
            PointMovePlate(xBoard - 1, yBoard + 0, false);
            PointMovePlate(xBoard + 1, yBoard + 0, false);
        }
        //isMoveWithAttack = false;
    }


    public void CannonMovePlate()
    {
        //for the moves:
        PointMovePlate(xBoard - 1, yBoard + 0, false);
        PointMovePlate(xBoard + 1, yBoard + 0, false);


        Game sc = controller.GetComponent<Game>();
        GameObject cp = sc.GetPositionWall(xBoard, yBoard);
        if (cp == null)
        {
            PointMovePlate(xBoard, yBoard + 1, false);
        }
        if (yBoard != 0)
        {
            cp = sc.GetPositionWall(xBoard, yBoard - 1);
            if (cp == null)
            {
                PointMovePlate(xBoard, yBoard - 1, false);
            }
        }

        //for the attacks:
        PointMovePlate(xBoard, yBoard + 2, true);
        PointMovePlate(xBoard, yBoard - 2, true);
        PointMovePlate(xBoard - 2, yBoard + 0, true);
        PointMovePlate(xBoard + 2, yBoard + 0, true);

        if(this.name.Contains("Maker"))
        {
            cp = sc.GetPositionWall(xBoard, yBoard);
            if (cp == null)
            {
                PointMovePlate(xBoard - 1, yBoard + 1, false);
                PointMovePlate(xBoard + 1, yBoard + 1, false);
            }
            if (yBoard != 0)
            {
                cp = sc.GetPositionWall(xBoard, yBoard - 1);
                if (cp == null)
                {
                    PointMovePlate(xBoard - 1, yBoard - 1, false);
                    PointMovePlate(xBoard + 1, yBoard - 1, false);
                }
            }
        }
    }

    public void SurroundMovePlate()
    {
        //isMoveWithAttack = false;

        Game sc = controller.GetComponent<Game>();
        GameObject cp = sc.GetPositionWall(xBoard, yBoard);
        if (cp == null)
        {
            PointMovePlate(xBoard, yBoard + 1, false);
            PointMovePlate(xBoard + 1, yBoard + 1, false);
            PointMovePlate(xBoard - 1, yBoard + 1, false);
        }
        if(yBoard != 0)
        {
            cp = sc.GetPositionWall(xBoard, yBoard - 1);
            if(cp == null)
            {
                PointMovePlate(xBoard, yBoard - 1, false);
                PointMovePlate(xBoard - 1, yBoard - 1, false);
                PointMovePlate(xBoard + 1, yBoard - 1, false);
            }
        }
        
        PointMovePlate(xBoard - 1, yBoard + 0, false);
        PointMovePlate(xBoard + 1, yBoard + 0, false);
        
    }

    public void InitiateWallPlates()
    {
        Game sc = controller.GetComponent<Game>();
        if (this.name == "wall_Blue")
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    PointWallPlate(x, y);
                }
            }
        }
    }

    public void PointWallPlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPositionWall(x, y);
            if (cp == null)
            {
                WallPlateSpawn(x, y);
            }
        }
    }
    public void WallPlateSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY + 0.5f;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(wallPlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        Debug.Log(mp);

        WallPlate mpScript = mp.GetComponent<WallPlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void PointMovePlate(int x, int y, bool cannon)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            if (cannon == false)
            {
                if (cp == null)
                {
                    MovePlateSpawn(x, y);
                }
                else if(cp.GetComponent<Chessman>().player != player)
                {
                    if (cp.GetComponent<Chessman>().name == "hat_Blue")
                    {
                        MovePlateSpawn(x, y);
                    }
                    else if (cp.GetComponent<Chessman>().name == "hat_Brown")
                    {
                        MovePlateSpawn(x, y);
                    }
                }
            }
            else
            {
                PointAttackPlate(x, y);
            }
        }
    }
    public void PointAttackPlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp != null && cp.GetComponent<Chessman>().player != player && cp.name != "hat_Blue" && cp.name != "hat_Brown")
            {
                MovePlateAttackSpawn(x, y);
            }
            else
            {
                //isMoveWithAttack = false;
            }
        }
    }
    public void PointReplacrPlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp != null && cp.GetComponent<Chessman>().player != player)
            {
                MovePlateReplacerSpawn(x, y);
            }
        }
    }

    public void MovePlateReplacerSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject rp = Instantiate(replacerPlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        ReplacerPlate rpScript = rp.GetComponent<ReplacerPlate>();
        rpScript.SetReference(gameObject);
        rpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void changeSprite(string color, GameObject a)
    {
        if (color == "blue")
        {
            a.GetComponent<SpriteRenderer>().sprite = bomb_Blue;
            a.name = "bomb_Blue";
        }
        else
        {
            a.GetComponent<SpriteRenderer>().sprite = bomb_Brown;
            a.name = "bomb_Brown";
        }
    }

    public void ChangeToHatSprite(string color, GameObject a)
    {
        if (color == "blue")
        {
            a.GetComponent<SpriteRenderer>().sprite = hat_Blue;
            a.name = "hat_Blue";
        }
        else
        {
            a.GetComponent<SpriteRenderer>().sprite = hat_Brown;
            a.name = "hat_Brown";
        }
    }


    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject ap = Instantiate(attackPlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        AttackPlate apScript = ap.GetComponent<AttackPlate>();
        apScript.SetReference(gameObject);
        apScript.SetCoords(matrixX, matrixY);
    }
}
