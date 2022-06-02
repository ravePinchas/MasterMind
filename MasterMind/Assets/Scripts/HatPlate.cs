using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatPlate : MonoBehaviour
{

    //Some functions will need reference to the controller
    public GameObject controller;

    //The Chesspiece that was tapped to create this MovePlate
    GameObject hat = null;
    //GameObject enemyReference = null;

    //Location on the board
    int matrixX;
    int matrixY;

    int objreplaceX;
    int objreplaceY;
    
    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        objreplaceX = hat.GetComponent<Chessman>().GetXBoard();
        objreplaceY = hat.GetComponent<Chessman>().GetYBoard();

        GameObject objReplace;
        //dead object
        if (hat.name.Contains("Blue"))
        {
            objReplace = controller.GetComponent<Game>().GetPositionDeads(matrixX, matrixY, "blue" );
        }
        else
        {
            objReplace = controller.GetComponent<Game>().GetPositionDeads(matrixX, matrixY, "brown");
        }

        //GameObject objReplace = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

        controller.GetComponent<Game>().ChangeHatPosition(hat, objReplace);

        //hat now is destroy
        
        hat.GetComponent<Chessman>().DestroyHatPlates();


        objReplace.GetComponent<Chessman>().SetXBoard(objreplaceX);
        objReplace.GetComponent<Chessman>().SetYBoard(objreplaceY);
        objReplace.GetComponent<Chessman>().SetCoords();

       

        if(objReplace.name.Contains("Blue"))
        {
            controller.GetComponent<Game>().RemoveHat(hat.name, "blue");
            Destroy(hat);
            controller.GetComponent<Game>().AddPiece(objReplace, "blue", objreplaceX, objreplaceY);
        }
        else
        {
            controller.GetComponent<Game>().RemoveHat(hat.name, "brown");
            Destroy(hat);
            controller.GetComponent<Game>().AddPiece(objReplace, "brown", objreplaceX, objreplaceY);
        }
        
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        hat = obj;
    }

    public GameObject GetReference()
    {
        return hat;
    }
}
