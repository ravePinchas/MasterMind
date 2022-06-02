using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlate : MonoBehaviour
{
    //Some functions will need reference to the controller
    public GameObject controller;

    //The Chesspiece that was tapped to create this MovePlate
    GameObject reference = null;

    //Location on the board
    int matrixX;
    int matrixY;

    public void OnMouseUp()
    {
        //connect the controller that shows in unity to the controller that we created here
        controller = GameObject.FindGameObjectWithTag("GameController");

        //put the object that we attack into cp
        GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

        reference.GetComponent<Chessman>().DestroyAttackPlates();
        reference.GetComponent<Chessman>().DestroyMovePlates();
        reference.GetComponent<Chessman>().DestroyHatPlates();
        reference.GetComponent<Chessman>().DestroyWallPlates();

        if (reference.GetComponent<Chessman>().name.Contains("bomb"))
        {
            splash(reference);
            changeBomb(reference);
        }
        if(reference.GetComponent<Chessman>().name.Contains("change"))
        {
            ChangeThePieceMaker(cp);
        }
        else if (cp.name.Contains("bomb"))
        {
            splash(cp);
            changeBomb(cp);
        }
        else
        {
            Death(cp);
        }

        //check if we move with the object and if so the changes are already made in move plate
        if (reference != null && reference.GetComponent<Chessman>().isMove != true)
        {
            controller.GetComponent<Game>().SetActivatedButton("true", 1);
            reference.GetComponent<Chessman>().isMove = true;
            controller.GetComponent<Game>().countMove++;
            //Switch Current Player
            if (controller.GetComponent<Game>().countMove == 2)
            {
                controller.GetComponent<Game>().SetActivatedButton("true", 2);
                //controller.GetComponent<Game>().NextTurn();
                //controller.GetComponent<Game>().countMove = 0;
                //controller.GetComponent<Game>().SetAllMovesToFalse();
                //reference.GetComponent<Chessman>().actionAttackIsDone = false;
                //reference.GetComponent<Chessman>().isMoveWithAttack = false;
            }
        }
        reference.GetComponent<Chessman>().isAttack = true;
    }

    public void ChangeThePieceMaker(GameObject obj)
    {
        switch (obj.name)
        {
            case "bomb_Blue":
            case "bombShield_Blue":
                splash(obj);
                changeBomb(obj);
                break;
            case "cannon_Blue":
                reference.name = "cannonMaker_Brown";
                Death(obj);
                break;
            case "changeMaker_Blue":
                Death(obj);
                break;
            case "cheos_Blue":
                reference.name = "cheosMaker_Brown";
                Death(obj);
                break;
            case "hatOfTheDeads_Blue":
                reference.name = "hatOfTheDeadsMaker_Brown";
                Death(obj);
                break;
            case "overload_Blue":
                reference.name = "overloadMaker_Brown";
                Death(obj);
                break;
            case "replacer_Blue":
                reference.name = "replacerMaker_Brown";
                Death(obj);
                break;
            case "wallBreaker_Blue":
                reference.name = "wallBreakerMaker_Brown";
                Death(obj);
                break;



            case "bomb_Brown":
            case "bombShield_Brown":
                splash(obj);
                changeBomb(obj);
                break;
            case "cannon_Brown":
                reference.name = "cannonMaker_Blue";
                Death(obj);
                break;
            case "changeMaker_Brown":
                Death(obj);
                break;
            case "cheos_Brown":
                reference.name = "cheosMaker_Blue";
                Death(obj);
                break;
            case "hatOfTheDeads_Brown":
                reference.name = "hatOfTheDeadsMaker_Blue";
                Death(obj);
                break;
            case "overload_Brown":
                reference.name = "overloadMaker_Blue";
                Death(obj);
                break;
            case "replacer_Brown":
                reference.name = "replacerMaker_Blue";
                Death(obj);
                break;
            case "wallBreaker_Brown":
                reference.name = "wallBreakerMaker_Blue";
                Death(obj);
                break;

            default:
                reference.name = obj.name;
                Death(obj);
                break;
        }
    }

    public void splash(GameObject b)
    {
        int x;
        int y;
        if (b.name.Contains("bomb"))
        {
            if (!b.GetComponent<Chessman>().isSheild)
            {
                x = b.GetComponent<Chessman>().GetXBoard();
                y = b.GetComponent<Chessman>().GetYBoard();

                GameObject piece1, piece2, piece3, piece4, piece5, piece6, piece7, piece8, piece9;

                piece6 = controller.GetComponent<Game>().GetPosition(x, y);
                if (piece6.name.Contains("Brown"))
                {
                    controller.GetComponent<Game>().SetPositionEmpty(piece6);
                    if(controller.GetComponent<Game>().RemoveObject(piece6, "brown"))
                    {
                        SetDeads(piece6);
                    }
                }
                else
                {
                    controller.GetComponent<Game>().SetPositionEmpty(piece6);
                    if(controller.GetComponent<Game>().RemoveObject(piece6, "blue"))
                    {
                        SetDeads(piece6);
                    }
                }
                
               


                piece1 = controller.GetComponent<Game>().GetPosition(x + 1, y + 1);
                Death(piece1);
                piece2 = controller.GetComponent<Game>().GetPosition(x, y + 1);
                Death(piece2);
                piece3 = controller.GetComponent<Game>().GetPosition(x - 1, y + 1);
                Death(piece3);
                piece4 = controller.GetComponent<Game>().GetPosition(x + 1, y);
                Death(piece4);
                piece5 = controller.GetComponent<Game>().GetPosition(x - 1, y);
                Death(piece5);
                piece7 = controller.GetComponent<Game>().GetPosition(x - 1, y - 1);
                Death(piece7);
                piece8 = controller.GetComponent<Game>().GetPosition(x, y - 1);
                Death(piece8);
                piece9 = controller.GetComponent<Game>().GetPosition(x + 1, y - 1);
                Death(piece9);
            }
        }
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }

    public void changeBomb(GameObject a)
    {
        if (a.GetComponent<Chessman>().isSheild)
        {
            a.GetComponent<Chessman>().isSheild = false;
            if (a.GetComponent<Chessman>().name == "bombShield_Blue")
            {
                a.GetComponent<Chessman>().changeSprite("blue", a);
            }
            else
            {
                a.GetComponent<Chessman>().changeSprite("brown", a);
            }
        }
    }

    public void ChangeHat(GameObject a)
    {
        if (a.name.Contains("hat"))
        {
            if (a.name == "hatOfTheDeads_Blue")
            {
                a.GetComponent<Chessman>().ChangeToHatSprite("blue", a);
            }
            else
            {
                a.GetComponent<Chessman>().ChangeToHatSprite("brown", a);
            }
        }
    }

    public void Death(GameObject a)
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        //remove from the lists playerBrown or playerBlue
        if (a)
        {
            if (a.name.Contains("bomb"))
            {
                splash(a);
                changeBomb(a);
            }
            else
            {
                if (a.name.Contains("Brown"))
                {
                    if(!a.name.Contains("hatOfThe"))
                    {
                        controller.GetComponent<Game>().SetPositionEmpty(a);
                    }
                   
                    if(controller.GetComponent<Game>().RemoveObject(a, "brown"))
                    {
                        SetDeads(a);
                    }
                }
                else
                {
                    if (!a.name.Contains("hatOfThe"))
                    {
                        controller.GetComponent<Game>().SetPositionEmpty(a);
                    }
                    
                    if(controller.GetComponent<Game>().RemoveObject(a, "blue"))
                    {
                        SetDeads(a);
                    }
                }
                
                //Destroy(a);
            }
        }
    }

    public void SetDeads(GameObject obj)
    {
        switch(obj.name)
        {
            case "bomb_Blue":
                obj.GetComponent<Chessman>().SetXBoard(10);
                obj.GetComponent<Chessman>().SetYBoard(3);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "blue");
                break;
            case "cannon_Blue":
                obj.GetComponent<Chessman>().SetXBoard(11);
                obj.GetComponent<Chessman>().SetYBoard(3);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "blue");
                break;
            case "changeMaker_Blue":
                obj.GetComponent<Chessman>().SetXBoard(12);
                obj.GetComponent<Chessman>().SetYBoard(3);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "blue");
                break;
            case "cheos_Blue":
                obj.GetComponent<Chessman>().SetXBoard(13);
                obj.GetComponent<Chessman>().SetYBoard(3);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "blue");
                break;
            case "hatOfTheDeads_Blue":
                ChangeHat(obj);
                break;
            case "overload_Blue":
                obj.GetComponent<Chessman>().SetXBoard(10);
                obj.GetComponent<Chessman>().SetYBoard(2);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "blue");
                break;
            case "replacer_Blue":
                obj.GetComponent<Chessman>().SetXBoard(11);
                obj.GetComponent<Chessman>().SetYBoard(2);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "blue");
                break;
            case "wallBreaker_Blue":
                obj.GetComponent<Chessman>().SetXBoard(12);
                obj.GetComponent<Chessman>().SetYBoard(2);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "blue");
                break;



            case "bomb_Brown":
                obj.GetComponent<Chessman>().SetXBoard(-2);
                obj.GetComponent<Chessman>().SetYBoard(3);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "brown");
                break;
            case "cannon_Brown":
                obj.GetComponent<Chessman>().SetXBoard(-3);
                obj.GetComponent<Chessman>().SetYBoard(3);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "brown");
                break;
            case "changeMaker_Brown":
                obj.GetComponent<Chessman>().SetXBoard(-4);
                obj.GetComponent<Chessman>().SetYBoard(3);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "brown");
                break;
            case "cheos_Brown":
                obj.GetComponent<Chessman>().SetXBoard(-5);
                obj.GetComponent<Chessman>().SetYBoard(3);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "brown");
                break;
            case "hatOfTheDeads_Brown":
                ChangeHat(obj);
                break;
            case "overload_Brown":
                obj.GetComponent<Chessman>().SetXBoard(-2);
                obj.GetComponent<Chessman>().SetYBoard(2);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "brown");
                break;
            case "replacer_Brown":
                obj.GetComponent<Chessman>().SetXBoard(-3);
                obj.GetComponent<Chessman>().SetYBoard(2);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "brown");
                break;
            case "wallBreaker_Brown":
                obj.GetComponent<Chessman>().SetXBoard(-4);
                obj.GetComponent<Chessman>().SetYBoard(2);
                obj.GetComponent<Chessman>().SetCoords();
                controller.GetComponent<Game>().SetPositionDead(obj, "brown");
                break;
            default:
                if(obj.name.Contains("Brown"))
                {
                    obj.name = "changeMaker_Brown";
                    obj.GetComponent<Chessman>().SetXBoard(-4);
                    obj.GetComponent<Chessman>().SetYBoard(3);
                    obj.GetComponent<Chessman>().SetCoords();
                    controller.GetComponent<Game>().SetPositionDead(obj, "brown");
                }
                else
                {
                    obj.name = "changeMaker_Blue";
                    obj.GetComponent<Chessman>().SetXBoard(12);
                    obj.GetComponent<Chessman>().SetYBoard(3);
                    obj.GetComponent<Chessman>().SetCoords();
                    controller.GetComponent<Game>().SetPositionDead(obj, "blue");
                }
                break;
        }
    }
}
