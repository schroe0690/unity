using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class cputurns : MonoBehaviour
{
    public Text textUI;
    int now;
    int colorb = choice_black.You;
    int colorw = choice_white.You;
    public int NowProperty
    {
        get { return now; }
        set { now = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (colorb == 1)
        {
            if (now == 1)
            {
                textUI.enabled = true; textUI.text = $"あなたのターン";
            }
            if (now == -1)
            {
                textUI.enabled = true; textUI.text = $"敵のターン";
            }
        }
        if (colorw == -1)
        {
            if (now == -1)
            {
                textUI.enabled = true; textUI.text = $"あなたのターン";
            }
            if (now == 1)
            {
                textUI.enabled = true; textUI.text = $"敵のターン";
            }
        }
    }
}
