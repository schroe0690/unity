using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mattanpc : MonoBehaviour
{
    int bcbutton;
    int push;
    Button btn;
    npc_manager script_back;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
    }
    public void OnClick()
    {
        this.script_back = FindObjectOfType<npc_manager>();
        int b = script_back.BackProperty;
        script_back.BackProperty = 1;
    }
    public int BackbProperty
    {
        get { return bcbutton; }
        set { bcbutton = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (bcbutton == -1)
        {
            btn.interactable = false;
        }
        if (bcbutton == 2)
        {
            btn.interactable = true;
        }
        if (bcbutton == 1)
        {
            push = push - 1;
            bcbutton = 0;
        }
    }
}
