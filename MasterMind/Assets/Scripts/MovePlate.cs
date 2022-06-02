using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
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
        controller = GameObject.FindGameObjectWithTag("GameController");
        GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
        if(cp != null)
        {
            if (cp.GetComponent<Chessman>().name == "hat_Blue")
            {
                controller.GetComponent<Game>().RemoveHat(cp.name, "blue");
                Destroy(cp);
            }
            else if (cp.GetComponent<Chessman>().name == "hat_Brown")
            {
                controller.GetComponent<Game>().RemoveHat(cp.name, "brown");
                Destroy(cp);
            }
        }

        //Set the Chesspiece's original location to be empty
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());

        //Move reference chess piece to this position
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        if(reference.GetComponent<Chessman>().player == "blue")
        {
            if(matrixY == 7)
            {
                controller.GetComponent<Game>().SetActivatedButton("Blue Win!", 1);
                return;
            }
        }
        else
        {
            if (matrixY == 0)
            {
                controller.GetComponent<Game>().SetActivatedButton("Brown Win!", 1);
                return;
            }
        }

        //Update the matrix
        controller.GetComponent<Game>().SetPosition(reference);

        //Destroy the move plates including self
        reference.GetComponent<Chessman>().DestroyMovePlates();
        reference.GetComponent<Chessman>().DestroyAttackPlates();
        reference.GetComponent<Chessman>().DestroyReplacerPlates();
        reference.GetComponent<Chessman>().DestroyHatPlates();
        reference.GetComponent<Chessman>().DestroyWallPlates();

        if (reference.GetComponent<Chessman>().isAttack == false)
        {
            reference.GetComponent<Chessman>().InitiateAttackPlates();
            if (reference.GetComponent<Chessman>().isAbility == false)
            {
                reference.GetComponent<Chessman>().InitiateReplacerPlates();
            }
        }
        else if(reference.GetComponent<Chessman>().isAbility == false)
        {
            reference.GetComponent<Chessman>().InitiateReplacerPlates();
        }
        
        controller.GetComponent<Game>().countMove++;
        reference.GetComponent<Chessman>().isMove = true;

        //change the color of the button littl bit brighter
        controller.GetComponent<Game>().SetActivatedButton("true", 1);


        //Switch Current Player
        if (controller.GetComponent<Game>().countMove == 2)
        {
            controller.GetComponent<Game>().SetActivatedButton("true", 2);
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
}
