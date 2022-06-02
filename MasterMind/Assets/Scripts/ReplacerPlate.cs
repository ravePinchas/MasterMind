using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacerPlate : MonoBehaviour
{
    //Some functions will need reference to the controller
    public GameObject controller;

    //The Chesspiece that was tapped to create this MovePlate
    GameObject reference = null;
    //GameObject enemyReference = null;

    //Location on the board
    int matrixX;
    int matrixY;

    int enemyX;
    int enemyY;

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        enemyX = reference.GetComponent<Chessman>().GetXBoard();
        enemyY = reference.GetComponent<Chessman>().GetYBoard();

        //enemy object
        GameObject enemyObj = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

        controller.GetComponent<Game>().ChangeReplacerPosition(reference, enemyObj);

        //reference is now in the place of the enemy
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        enemyObj.GetComponent<Chessman>().SetXBoard(enemyX);
        enemyObj.GetComponent<Chessman>().SetYBoard(enemyY);
        enemyObj.GetComponent<Chessman>().SetCoords();

        //Destroy the move plates including self
        reference.GetComponent<Chessman>().DestroyMovePlates();
        reference.GetComponent<Chessman>().DestroyAttackPlates();
        reference.GetComponent<Chessman>().DestroyReplacerPlates();
        reference.GetComponent<Chessman>().DestroyHatPlates();
        reference.GetComponent<Chessman>().DestroyWallPlates();

        reference.GetComponent<Chessman>().isAbility = true;
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