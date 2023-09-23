using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Black_count : MonoBehaviour
{
    int countb;
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
        textUI.text = $"黒{countb}";
    }
    
}
