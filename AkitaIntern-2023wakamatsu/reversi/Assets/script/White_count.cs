using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class White_count : MonoBehaviour
{
    int countw;
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
        textUI.text = $"白{countw}";
    }
    
}
