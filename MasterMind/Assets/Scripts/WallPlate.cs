using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlate : MonoBehaviour
{  //Some functions will need reference to the controller
    public GameObject controller;

    //The Chesspiece that was tapped to create this MovePlate
    GameObject wall = null;

    //Location on the board
    int matrixX;
    int matrixY;

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);


        if (wall.name.Contains("Blue"))
        {
            //Set the Chesspiece's original location to be empty
            controller.GetComponent<Game>().SetPositionWallNotUsedEmpty
            (wall.GetComponent<Chessman>().GetXBoard(), wall.GetComponent<Chessman>().GetYBoard(), "blue");
        }
        else
        {
            //TODO
        }

        //Move reference chess piece to this position
        wall.GetComponent<Chessman>().SetXBoardWall(matrixX);
        wall.GetComponent<Chessman>().SetYBoardWall(matrixY + 0.5f);
        wall.GetComponent<Chessman>().SetXBoard(matrixX);
        wall.GetComponent<Chessman>().SetYBoard(matrixY);
        wall.GetComponent<Chessman>().SetCoordsWalls();

        //Update the matrix
        controller.GetComponent<Game>().SetPositionWall(wall);

        //Destroy the move plates including self
        wall.GetComponent<Chessman>().DestroyMovePlates();
        wall.GetComponent<Chessman>().DestroyAttackPlates();
        wall.GetComponent<Chessman>().DestroyReplacerPlates();
        wall.GetComponent<Chessman>().DestroyHatPlates();
        wall.GetComponent<Chessman>().DestroyWallPlates();

        controller.GetComponent<Game>().countMove++;
        wall.GetComponent<Chessman>().isMove = true;

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
        wall = obj;
    }

    public GameObject GetReference()
    {
        return wall;
    }
}
