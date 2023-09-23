using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    [SerializeField]
    Gamemanager _gameManager;
    [SerializeField]
    npc_manager npc_manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void breakcpp()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
