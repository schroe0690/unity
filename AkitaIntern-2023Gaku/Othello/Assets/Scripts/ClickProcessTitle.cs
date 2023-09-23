using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickProcessTitle : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    public static string mode;
    Animator animator;

    public void ClickPlayer()
    {
        animator = canvas.GetComponent<Animator>();
        animator.Play("ClickPlayer");
    }
    public void ClickCPU()
    {
        animator = canvas.GetComponent<Animator>();
        animator.Play("ClickCPU");
    }

    public void ToCPUSetting()
    {
        mode = "cpu";
        SceneManager.LoadScene("CPUSetting");
    }

    public void ToOthello()
    {
        mode = "player";
        SceneManager.LoadScene("othello");
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
