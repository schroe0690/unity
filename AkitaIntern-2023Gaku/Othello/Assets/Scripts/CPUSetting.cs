using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CPUSetting : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    Animator animator;

    public static int CPULevel;
    public static int PlayerColor;

    void Start()
    {
        CPULevel = 1;
        PlayerColor = Othello.BLACK;
    }

    public void OnLevel1()
    {
        CPULevel = 1;
    }

    public void OnLevel2()
    {
        CPULevel = 2;
    }

    public void OnLevel3()
    {
        CPULevel = 3;
    }

    public void OnBlack()
    {
        PlayerColor = Othello.BLACK;
    }

    public void OnWhite()
    {
        PlayerColor = Othello.WHITE;
    }

    public void ClickPlayStart()
    {
        animator = canvas.GetComponent<Animator>();
        animator.Play("ClickPlay");
    }

    public void ToOthello()
    {
        SceneManager.LoadScene("othello");
    }
}
