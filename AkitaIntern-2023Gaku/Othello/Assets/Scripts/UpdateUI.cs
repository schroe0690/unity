using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] Text BlackText;
    [SerializeField] Text WhiteText;
    [SerializeField] Text WinnerText;
    [SerializeField] Canvas canvas;
    Animator animator;

    private void Start()
    {
        animator = canvas.GetComponent<Animator>();
    }

    public void UpdateScore(int BlackScore, int WhiteScore)
    {
        BlackText.text = "Black:" + BlackScore.ToString("00");
        WhiteText.text = "White:" + WhiteScore.ToString("00");
    }

    public void UpdatePass(int Pass)
    {
        if (Pass == 1) animator.Play("Pass");
    }

    public void UpdateWinner(int Winner)
    {
        if (Winner == Othello.BLACK) WinnerText.text = "WINNER:BLACK";
        if (Winner == Othello.WHITE) WinnerText.text = "WINNER:WHITE";
        if (Winner == Othello.EMPTY) WinnerText.text = "DRAW";
        if (Winner == Othello.WALL) WinnerText.text = "";
        if (Winner != Othello.WALL) animator.SetBool("Winner", true);
    }
}
