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
                Debug.Log("���������������I�u�W�F�N�g�̏��");
                Debug.Log("���O�F�@" + hit.transform.name);
                Debug.Log("�ʒu�F�@" + hit.transform.position);
                Debug.Log("�����F�@" + hit.distance);
                hit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
    }
}
