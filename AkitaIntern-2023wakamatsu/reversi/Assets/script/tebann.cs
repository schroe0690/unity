using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tebann : MonoBehaviour
{
    public Text textUI;
    int clr;
    // Start is called before the first frame update
    void Start()
    {
    }
    public int TebannProperty
    {
        get { return clr; }
        set { clr = value; }
    }
    // Update is called once per frame
    void Update()
    {
        if (clr == 1) 
        {
            textUI.enabled = true; textUI.text = $"黒のターン";
        }
        if (clr == -1)
        {
            textUI.enabled = true; textUI.text = $"白のターン";
        }
    }
}
