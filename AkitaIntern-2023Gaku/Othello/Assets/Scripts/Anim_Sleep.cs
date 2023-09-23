using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Sleep : MonoBehaviour
{
    ruler Ruler;
    // Start is called before the first frame update
    void Start()
    {
        Ruler= GameObject.Find("Main").GetComponent<ruler>();
    }

    // アニメーションがスタートした時の処理
    public void anim_start()
    {
        Ruler.anim_sleep = true;
        // Debug.Log("animation start");
    }

    // アニメーションがストップした時の処理
    public void anim_end()
    {
        Ruler.anim_sleep = false;
        // Debug.Log("animation end");
    }
}
