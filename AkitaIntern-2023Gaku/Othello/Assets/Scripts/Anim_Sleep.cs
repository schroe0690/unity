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

    // �A�j���[�V�������X�^�[�g�������̏���
    public void anim_start()
    {
        Ruler.anim_sleep = true;
        // Debug.Log("animation start");
    }

    // �A�j���[�V�������X�g�b�v�������̏���
    public void anim_end()
    {
        Ruler.anim_sleep = false;
        // Debug.Log("animation end");
    }
}
