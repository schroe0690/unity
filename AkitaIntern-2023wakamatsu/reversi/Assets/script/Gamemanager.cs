using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Gamemanager : MonoBehaviour
{
   

    public GameObject obj;
    public GameObject cpp;
    public float zahyoux;
    public float zahyouy;
    public float zahyouz;
    public float pointx;
    public float pointz;
    public static int BLACK = 1;
    public static int EMPTY = 0;
    public static int WHITE = -1;
    public static int WALL = 2;
    public static int RETURN = 3;
    public static int CAN = 4;
    public int disk_x;
    public int disk_z;
    int turns =0;
    int dir;
    int[] passturn = new int[70];
    public int[,] squares = new int[10, 10];
    public int[,,] memory = new int[70,10, 10];
    public int count_w = 2;
    public int count_b = 2;
    int current_color;
    int play;
    int end;
    int win;
    int change;
    int back;
    int pass=0;
    int cant;
    White_count script_White_count;
    Black_count script_Black_count;
    winner script_winner;
    tebann script_tebann;
    back_button script_back_button;
    endbtn script_endbtn;
    List<GameObject> disc = new List<GameObject>();
    private int currentPlayer = BLACK;

    int UPPER = 1;
    int UPPER_LEFT = 2;
    int LEFT = 4;
    int LOWER_LEFT = 8;
    int LOWER = 16;
    int LOWER_RIGHT = 32;
    int RIGHT = 64;
    int UPPER_RIGHT = 128;

    Dictionary<int, DISC> m_discDic = new Dictionary<int, DISC>();
    Dictionary<int, light> m_lightDic = new Dictionary<int, light>();

    public int BackProperty
    {
        get { return back; }
        set { back = value; }
    }
    /// <summary>
    /// ディスクを生成する
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="pos"></param>
    /// <param name="rotate"></param>
    /// <param name="index"></param>
    private void CreateDisc(GameObject obj, Vector3 pos, Quaternion rotate, int index, int color)
    {
        var go = Instantiate(obj, pos, rotate);

        var disc = go.GetComponent<DISC>();
        disc.color = color;

        m_discDic.Add(index, disc);
    }
    private void BreakeDisc(int index)
    {
        m_discDic.Remove(index);
    }

    private DISC GetDisc(int index)
    {
        DISC result;
        if (!m_discDic.TryGetValue(index, out result))
        {
            return null;
        }
        return result;
    }
    private DISC Breake(int index)
    {
        DISC result;
        if (!m_discDic.TryGetValue(index, out result))
        {
            return null;
        }
        return result;
    }
   
    
   


    private void Start()
    {


        CreateDisc(obj, new Vector3(4f, 0.5f, 4f), Quaternion.Euler(180, 0, 0),(4 + 4 * 8) , DISC.BLACK );
        CreateDisc(obj, new Vector3(4f, 0.5f, 5f), Quaternion.identity, (4 + 5 * 8), DISC.WHITE);
        CreateDisc(obj, new Vector3(5f, 0.5f, 4f), Quaternion.identity,(5 + 4 * 8), DISC.WHITE);
        CreateDisc(obj, new Vector3(5f, 0.5f, 5f), Quaternion.Euler(180, 0, 0),(5 + 5 * 8), DISC.BLACK);








        for (int j = 1; j < 9; j++)
        {
            for (int k = 1; k < 9; k++)
            {
                squares[j, k] = EMPTY;
                squares[j, 0] = WALL;
                squares[j, 9] = WALL;
                squares[0, k] = WALL;
                squares[9, k] = WALL;
            }
        }
        squares[4, 5] = WHITE;
        squares[5, 5] = BLACK;
        squares[4, 4] = BLACK;
        squares[5, 4] = WHITE;

    }




    void Update()
    {
        if (turns == 0 || cant == 1)
        {
            this.script_back_button = FindObjectOfType<back_button>();
            int bc = script_back_button.BackbProperty;
            script_back_button.BackbProperty = -1;
        }
        else
        {
            this.script_back_button = FindObjectOfType<back_button>();
            int bc = script_back_button.BackbProperty;
            script_back_button.BackbProperty = 2;
        }
        if (passturn[turns] == 1 && pass == 0)
        {
            pass = 1;
        }
        if (back == 1)
        {
            if (pass == 0)
            {
                currentPlayer = -currentPlayer;
            }
            if (passturn[turns] == 1 && pass == 0)
            {
                pass = 1;
            }
            for (int i = 1; i < 9; i++)
            {

                for (int j = 1; j < 9; j++)
                {
                    if(change ==0)
                    {
                        pass = 0;
                    }
                    if (memory[turns, i, j] == 1)
                    {
                        var disc = Breake(i + j * 8);
                        disc.Breakethis();
                        squares[i, j] = EMPTY;
                        BreakeDisc(i + j * 8);

                        if (currentPlayer == WHITE)
                        {
                            count_w = count_w - 1;
                        }
                        if (currentPlayer == BLACK)
                        {
                            count_b = count_b - 1;
                        }
                        
                        memory[turns, i, j] = 0;
                    }
                    if (memory[turns, i, j] == 2)
                    {
                        var disc = GetDisc(i + j * 8);
                        disc.ChangeColor();
                        if (currentPlayer == WHITE)
                        {
                            count_w = count_w - 1;
                            count_b = count_b + 1;
                            squares[i, j] = BLACK;
                            if (pass== 1)
                            {
                                current_color = WHITE;
                            }
                        }
                        if (currentPlayer == BLACK)
                        {
                            count_b = count_b - 1;
                            count_w = count_w + 1;
                            squares[i, j] = WHITE;
                            if (pass ==1)
                            {
                                current_color = BLACK;
                            }
                        }
                        memory[turns, i, j] = 0;
                        passturn[turns] = 0;
                    }


                    if (squares[i, j] == CAN)
                    {
                        squares[i, j] = EMPTY;
                    }
                }
            }
            this.script_back_button = FindObjectOfType<back_button>();
            int bc = script_back_button.BackbProperty;
            script_back_button.BackbProperty = 1;
            turns = turns - 1;
            this.script_White_count = FindObjectOfType<White_count>();
            int cw = script_White_count.countwProperty;
            script_White_count.countwProperty = count_w;
            this.script_Black_count = FindObjectOfType<Black_count>();
            int cb = script_Black_count.countbProperty;
            script_Black_count.countbProperty = count_b;
            back = 0;
            
        }
        for (int j = 1; j < 9; j++)
        {
            for (int k = 1; k < 9; k++)
            {
                if (squares[j,k] == CAN) { squares[j,k] = EMPTY; }
                if (currentPlayer == BLACK)
                {
                    current_color = BLACK;
                    this.script_tebann = FindObjectOfType<tebann>();
                    int p = script_tebann.TebannProperty;
                    script_tebann.TebannProperty = 1;
                }
                else if (currentPlayer == WHITE)
                {
                    current_color = WHITE;
                    this.script_tebann = FindObjectOfType<tebann>();
                    int p = script_tebann.TebannProperty;
                    script_tebann.TebannProperty = -1;
                }
                if (squares[j, k] == EMPTY)
                {
                    if (squares[j, k + 1] == -1 * current_color)
                    {
                        disk_x = j; disk_z = k + 2;
                        while (squares[disk_x, disk_z] == -1 * current_color) { disk_z++; }
                        if (squares[disk_x, disk_z] == current_color) dir |= UPPER;
                    }
                    if (squares[j, k - 1] == -1 * current_color)
                    {
                        disk_x = j; disk_z = k - 2;
                        while (squares[disk_x, disk_z] == -1 * current_color) { disk_z--; }
                        if (squares[disk_x, disk_z] == current_color) dir |= LOWER;
                    }
                    if (squares[j - 1, k] == -1 * current_color)
                    {
                        disk_x = j - 2; disk_z = k;
                        while (squares[disk_x, disk_z] == -1 * current_color) { disk_x--; }
                        if (squares[disk_x, disk_z] == current_color) dir |= LEFT;
                    }
                    if (squares[j + 1, k + 1] == -1 * current_color)
                    {
                        disk_x = j + 2; disk_z = k + 2;
                        while (squares[disk_x, disk_z] == -1 * current_color) { disk_x++; disk_z++; }
                        if (squares[disk_x, disk_z] == current_color) dir |= UPPER_RIGHT;
                    }
                    if (squares[j + 1, k] == -1 * current_color)
                    {
                        disk_x = j + 2; disk_z = k;
                        while (squares[disk_x, disk_z] == -1 * current_color) { disk_x++; }
                        if (squares[disk_x, disk_z] == current_color) dir |= RIGHT;
                    }
                    if (squares[j - 1, k + 1] == -1 * current_color)
                    {
                        disk_x = j - 2; disk_z = k + 2;
                        while (squares[disk_x, disk_z] == -1 * current_color) { disk_x--; disk_z++; }
                        if (squares[disk_x, disk_z] == current_color) dir |= UPPER_LEFT;
                    }
                    if (squares[j - 1, k - 1] == -1 * current_color)
                    {
                        disk_x = j - 2; disk_z = k - 2;
                        while (squares[disk_x, disk_z] == -1 * current_color) { disk_x--; disk_z--; }
                        if (squares[disk_x, disk_z] == current_color) dir |= LOWER_LEFT;
                    }
                    if (squares[j + 1, k - 1] == -1 * current_color)
                    {
                        disk_x = j + 2; disk_z = k - 2;
                        while (squares[disk_x, disk_z] == -1 * current_color) { disk_x++; disk_z--; }
                        if (squares[disk_x, disk_z] == current_color) dir |= LOWER_RIGHT;
                    }
                    if (dir== 0) { }
                    else
                    {
                        squares[j, k] = CAN;
                        play = 1;
                        dir = 0;
                        
                    }
                }
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                {

                    {
                        zahyoux = hit.point.x;
                        zahyouy = hit.point.y;
                        zahyouz = hit.point.z;
                        pointx = Mathf.Round(zahyoux);
                        pointz = Mathf.Round(zahyouz);
                        int x = Mathf.RoundToInt(zahyoux);
                        int z = Mathf.RoundToInt(zahyouz);
                        if (0.6f > zahyouy && zahyouy > 0)
                        {
                            if (squares[x, z] == CAN)
                            {
                                
                                int u = 0;
                                int s = 0;
                                int m = 0;
                                int h = 0;
                                int mu = 0;
                                int hu = 0;
                                int ms = 0;
                                int hs = 0;

                                if (squares[x, z + 1] == -current_color)
                                {
                                    disk_x = x; disk_z = z + 2; u = 1;
                                    while (squares[disk_x, disk_z] == -current_color) { disk_z++; u++; }
                                    if (squares[disk_x, disk_z] == current_color) dir |= UPPER;
                                }
                                if (squares[x, z - 1] == -current_color)
                                {
                                    disk_x = x; disk_z = z - 2; s = 1;
                                    while (squares[disk_x, disk_z] == -current_color) { disk_z--; s++; }
                                    if (squares[disk_x, disk_z] == current_color) dir |= LOWER;
                                }
                                if (squares[x - 1, z] == -current_color)
                                {
                                    disk_x = x - 2; disk_z = z; h = 1;
                                    while (squares[disk_x, disk_z] == -current_color) { disk_x--; h++; }
                                    if (squares[disk_x, disk_z] == current_color) dir |= LEFT;
                                }
                                if (squares[x + 1, z + 1] == -current_color)
                                {
                                    disk_x = x + 2; disk_z = z + 2; mu = 1;
                                    while (squares[disk_x, disk_z] == -current_color) { disk_x++; disk_z++; mu++; }
                                    if (squares[disk_x, disk_z] == current_color) dir |= UPPER_RIGHT;
                                }
                                if (squares[x + 1, z] == -current_color)
                                {
                                    disk_x = x + 2; disk_z = z; m = 1;
                                    while (squares[disk_x, disk_z] == -current_color) { disk_x++; m++; }
                                    if (squares[disk_x, disk_z] == current_color) dir |= RIGHT;
                                }
                                if (squares[x - 1, z + 1] == -current_color)
                                {
                                    disk_x = x - 2; disk_z = z + 2; hu = 1;
                                    while (squares[disk_x, disk_z] == -current_color) { disk_x--; disk_z++; hu++; }
                                    if (squares[disk_x, disk_z] == current_color) dir |= UPPER_LEFT;
                                }
                                if (squares[x - 1, z - 1] == -current_color)
                                {
                                    disk_x = x - 2; disk_z = z - 2; hs = 1;
                                    while (squares[disk_x, disk_z] == -current_color) { disk_x--; disk_z--; hs++; }
                                    if (squares[disk_x, disk_z] == current_color) dir |= LOWER_LEFT;
                                }
                                if (squares[x + 1, z - 1] == -current_color)
                                {
                                    disk_x = x + 2; disk_z = z - 2; ms = 1;
                                    while (squares[disk_x, disk_z] == -current_color) { disk_x++; disk_z--; ms++; }
                                    if (squares[disk_x, disk_z] == current_color) dir |= LOWER_RIGHT;
                                }
                                if (dir == 0) { }
                                else
                                {

                                    squares[x,z] = current_color;
                                    if (currentPlayer == WHITE)
                                    {
                                        CreateDisc(obj, new Vector3(pointx, zahyouy, pointz), Quaternion.identity, (x + z * 8), DISC.WHITE);
                                        count_w = count_w + 1;
                                    }
                                    if (currentPlayer == BLACK)
                                    {
                                        CreateDisc(obj, new Vector3(pointx, zahyouy, pointz), Quaternion.Euler(180, 0, 0), (x + z * 8), DISC.BLACK);
                                        count_b = count_b + 1;
                                    }
                                    pass = 0;
                                    turns++;
                                    memory[turns, x, z] = 1;
                                }
                                if (dir >= UPPER_RIGHT)
                                {
                                    while (mu > 0)
                                    {
                                        squares[x + mu, z + mu] = RETURN;
                                        mu--;

                                    }
                                    dir = dir - UPPER_RIGHT;
                                }
                                if (dir >= RIGHT)
                                {
                                    while (m > 0)
                                    {
                                        squares[x + m, z] = RETURN;
                                        m--;

                                    }
                                    dir = dir - RIGHT;
                                }
                                if (dir >= LOWER_RIGHT)
                                {
                                    while (ms > 0)
                                    {
                                        squares[x + ms, z - ms] = RETURN;
                                        ms--;

                                    }
                                    dir = dir - LOWER_RIGHT;
                                }
                                if (dir >= LOWER)
                                {
                                    while (s > 0)
                                    {
                                        squares[x, z - s] = RETURN;
                                        s--;

                                    }
                                    dir = dir - LOWER;
                                }
                                if (dir >= LOWER_LEFT)
                                {
                                    while (hs > 0)
                                    {
                                        squares[x - hs, z - hs] = RETURN;
                                        hs--;

                                    }
                                    dir = dir - LOWER_LEFT;
                                }
                                if (dir >= LEFT)
                                {
                                    while (h > 0)
                                    {
                                        squares[x - h, z] = RETURN;
                                        h--;

                                    }
                                    dir = dir - LEFT;
                                }
                                if (dir >= UPPER_LEFT)
                                {
                                    while (hu > 0)
                                    {
                                        squares[x - hu, z + hu] = RETURN;
                                        hu--;

                                    }
                                    dir = dir - UPPER_LEFT;
                                }
                                if (dir >= UPPER)
                                {
                                    while (u > 0)
                                    {
                                        squares[x, z + u] = RETURN;
                                        u--;
                                    }
                                    dir = dir - UPPER;
                                }
                                dir = 0;
                                for (int i = 1; i < 9; i++)
                                {
                                    for (int j = 1; j < 9; j++)
                                    {
                                        if (squares[i, j] == RETURN)
                                        {
                                            memory[turns, i, j] =2;
                                            var disc = GetDisc(i + j * 8);
                                            disc.ChangeColor();
                                            squares[i, j] = current_color;
                                            if (currentPlayer == WHITE)
                                            {
                                                count_w = count_w + 1;
                                                count_b = count_b - 1;
                                            }
                                            if (currentPlayer == BLACK)
                                            {
                                                count_b = count_b + 1;
                                                count_w = count_w - 1;
                                            }

                                        }
                                    }
                                }
                                currentPlayer = -current_color;
                                this.script_White_count = FindObjectOfType<White_count>();
                                int cw = script_White_count.countwProperty;
                                script_White_count.countwProperty = count_w;
                                this.script_Black_count = FindObjectOfType<Black_count>();
                                int cb = script_Black_count.countbProperty;
                                script_Black_count.countbProperty = count_b;
                            }
                        }

                    }
                }
            }
        }
        if (play == 1) { end = 0;change = 0; }
        else if(play == 0) {change = change + 1;pass = 1; end = 0; passturn[turns] = 1; }
        if (turns == 60) { end = 1; }
        if(change == 1) { currentPlayer = -currentPlayer; }
        if(change == 2) { end = 1; }

        play = 0;
        if(end == 1)
        {
            cant = 1;
            if (count_b > count_w)
            {
                win = 1;    
            }
            else if(count_b < count_w)
            {
                win = -1;   
            }
            else if(count_b == count_w)
            {
                win = 0;
            }
            this.script_winner = FindObjectOfType<winner>();
            int w = script_winner.winnerProperty;
            script_winner.winnerProperty = win;
        }
    }
}

