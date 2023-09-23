using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class endbtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    
    public void OnClick()
    {
        SceneManager.LoadScene("title");
     
    }
    // Update is called once per frame
    void Update()
    {
    }
}
