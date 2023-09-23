using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class winner : MonoBehaviour
{

    int win = 2;
    public Text textUI;
    // Start is called before the first frame update
    void Start()
    {
        textUI.enabled = false; 
    }
    public int winnerProperty
    {
        get { return win; }
        set { win = value; }
    }
    // Update is called once per frame
    void Update()
    {
        if (win == 0) { textUI.enabled = true; textUI.text = $"引き分け"; }
        if(win == 1) { textUI.enabled = true; textUI.text = $"黒の勝ち"; }
        if (win == -1) { textUI.enabled = true; textUI.text = $"白の勝ち"; }   
    }
}
