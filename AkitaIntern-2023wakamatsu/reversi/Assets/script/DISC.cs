using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DISC : MonoBehaviour
{

    // 色
    public static int BLACK = 1;
    public static int EMPTY = 0;
    public static int WHITE = -1;
    // 座標
    int reverce;
    int m_color = EMPTY;
   



    public int color
    {
        get {
            return m_color;
        }
        set {
            m_color = value;
        }
    }


    [SerializeField]
    Gamemanager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// 色が変わるとき実行させる
    /// </summary>
    public void ChangeColor()
    {
        m_color *= -1;
        this.gameObject.transform.eulerAngles = new Vector3(90 * (1 + m_color), 0, 0);
    }
    public void Breakethis()
    {
        m_color = 0;
        Destroy(gameObject);
    }
}
