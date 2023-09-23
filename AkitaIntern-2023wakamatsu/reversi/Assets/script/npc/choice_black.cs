using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class choice_black : MonoBehaviour
{
   
    public static int You;
    int a = npc_manager.player;
    public void ClickOnButton()
    {
        SceneManager.LoadScene("VSnpc");
        You = 1;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(a == 1) { You = 0; }
    }
}
