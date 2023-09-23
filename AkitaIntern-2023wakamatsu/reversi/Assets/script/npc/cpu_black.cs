using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cpu_black : MonoBehaviour
{
    int countb;
    int colorb = choice_black.You;
    int colorw = choice_white.You;
    public Text textUI;
    // Start is called before the first frame update
    void Start()
    {
        countb = 2;
    }
    public int countbProperty
    {
        get { return countb; }
        set { countb = value; }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (colorb == 1)
        {
            textUI.text = $"あなた{countb}";
        }
        if (colorw == -1)
        {
            textUI.text = $"敵{countb}";
        }
    }
}
