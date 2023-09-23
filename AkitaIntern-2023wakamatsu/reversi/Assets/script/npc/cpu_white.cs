using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cpu_white : MonoBehaviour
{
    int countw;
    int colorb = choice_black.You;
    int colorw = choice_white.You;
    public Text textUI;
    // Start is called before the first frame update
    void Start()
    {
        countw = 2;
    }
    public int countwProperty
    {
        get { return countw; }
        set { countw = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (colorw == -1)
        {
            textUI.text = $"あなた{countw}";
        }
        if (colorb == 1)
        {
            textUI.text = $"敵{countw}";
        }
    }
}
