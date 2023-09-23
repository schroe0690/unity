using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Vector3 position = new();
    public int x, z;
    static float y = 0.3f;

    public bool GetPosition()
    {
        bool pos_judge = false;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                position = hit.transform.position;
                position.y = y;
                x = (int)position.x;
                z = (int)position.z;
                pos_judge=true;
            }
        }

        return pos_judge;
    }
}
