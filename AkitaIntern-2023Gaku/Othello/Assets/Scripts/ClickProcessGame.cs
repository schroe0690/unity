using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickProcessGame : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    Animator animator;
    ruler Ruler;

    // Start is called before the first frame update
    void Start()
    {
        Ruler = GameObject.Find("Main").GetComponent<ruler>();
        
    }

    // undoボタンのクリック時の処理
    public void ClickUndo()
    {
        Ruler.undo = true;
    }

    public void ClickToTitle()
    {
        animator = canvas.GetComponent<Animator>();
        animator.SetBool("ClickToTitle", true);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
