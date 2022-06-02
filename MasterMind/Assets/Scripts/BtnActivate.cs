using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnActivate : MonoBehaviour
{
    public Button nextTurnBtn;
    public Text nextTurnTxt;
    public GameObject controller;
    public Game game;

    public void SetBtn(string state, int move)
    {
        if (state == "false")
        {
            nextTurnBtn = controller.GetComponent<Game>().nextTurnBtn;
            if (nextTurnBtn.GetComponent<Image>().color == Color.blue)
            {
                nextTurnBtn.GetComponent<Image>().color = Color.black;
            }
            else
            {
                nextTurnBtn.GetComponent<Image>().color = Color.blue;
            }
            controller.GetComponent<Game>().nextTurnBtn.interactable = false;
            nextTurnTxt = controller.GetComponent<Game>().nextTurnTxt;
            nextTurnTxt.GetComponent<Text>().text = "Moves: 0";
            //nextTurnBtn.GetComponent<Text>().color = Color.green;
        }
        else if (state == "true")
        {
            if (move == 1)
            {
                controller.GetComponent<Game>().nextTurnBtn.interactable = true;
                nextTurnBtn = controller.GetComponent<Game>().nextTurnBtn;
                nextTurnTxt = controller.GetComponent<Game>().nextTurnTxt;
                if (controller.GetComponent<Game>().GetCurrentPlayer() == "blue")
                {
                    nextTurnBtn.GetComponent<Image>().color = new Color32(0,157,255,255);
                    
                    //nextTurnBtn.GetComponent<Text>().color = Color.green;
                }
                else
                {
                    nextTurnBtn.GetComponent<Image>().color = new Color32(90, 14, 14, 255);
                    //nextTurnBtn.GetComponent<Text>().color = Color.green;
                }
                nextTurnTxt.GetComponent<Text>().text = "Moves: 1";
            }
            else
            {
                controller.GetComponent<Game>().nextTurnBtn.interactable = true;
                nextTurnBtn = controller.GetComponent<Game>().nextTurnBtn;
                if (controller.GetComponent<Game>().GetCurrentPlayer() == "blue")
                {
                    nextTurnBtn.GetComponent<Image>().color = Color.blue;
                    //nextTurnBtn.GetComponent<Text>().color = Color.red;
                }
                else
                {
                    nextTurnBtn.GetComponent<Image>().color = Color.black;
                    //nextTurnBtn.GetComponent<Text>().color = Color.green;
                }
                nextTurnTxt = controller.GetComponent<Game>().nextTurnTxt;
                nextTurnTxt.GetComponent<Text>().text = "Next Turn";
            }
        }
        else
        {
            nextTurnTxt = controller.GetComponent<Game>().nextTurnTxt;
            nextTurnTxt.GetComponent<Text>().text = state;
        }
    }

    public bool IsActivatedBtn()
    {
        return controller.GetComponent<Game>().nextTurnBtn.interactable == true;
    }

    public void OnMouseUp()
    {
        //controller.GetComponent<Chessman>().DestroyReplacerPlates();
        //controller.GetComponent<Chessman>().DestroyMovePlates();
        //controller.GetComponent<Chessman>().DestroyAttackPlates();

        nextTurnBtn = controller.GetComponent<Game>().nextTurnBtn;
        if (nextTurnBtn.GetComponent<Image>().color == Color.blue)
        {
            nextTurnBtn.GetComponent<Image>().color = Color.black;
        }
        else
        {
            nextTurnBtn.GetComponent<Image>().color = Color.blue;
        }
        controller.GetComponent<Game>().nextTurnBtn.interactable = false;
    }
}
