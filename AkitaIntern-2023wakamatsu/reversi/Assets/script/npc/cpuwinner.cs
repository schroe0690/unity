using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cpuwinner : MonoBehaviour
{
    int win = 2 ;
    int colorb = choice_black.You;
    int colorw = choice_white.You;
    public Text textUI;
    // Start is called before the first frame update
    void Start()
    {
        textUI.enabled = false;

    }
    public int cpuwinProperty
    {
        get { return win; }
        set { win = value; }
    }
   
    // Update is called once per frame
    void Update()
    {
        if (colorb == 1)
        {
            if (win == 0) { textUI.enabled = true; textUI.text = $"引き分け"; }
            if (win == 1) { textUI.enabled = true; textUI.text = $"あなたの勝ち"; }
            if (win == -1) { textUI.enabled = true; textUI.text = $"あなたの負け"; }
        }
        if (colorw == -1)
        {
            if (win == 0) { textUI.enabled = true; textUI.text = $"引き分け"; }
            if (win == -1) { textUI.enabled = true; textUI.text = $"あなたの勝ち"; }
            if (win == 1) { textUI.enabled = true; textUI.text = $"あなたの負け"; }
        }
    }
}
