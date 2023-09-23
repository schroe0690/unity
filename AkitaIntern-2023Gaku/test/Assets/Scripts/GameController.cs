using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("光線が当たったオブジェクトの情報");
                Debug.Log("名前：　" + hit.transform.name);
                Debug.Log("位置：　" + hit.transform.position);
                Debug.Log("距離：　" + hit.distance);
                hit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
    }
}
