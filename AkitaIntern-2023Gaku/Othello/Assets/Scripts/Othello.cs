using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Othello : MonoBehaviour
{
    public GameObject Disc;     // ��̃v���n�u
    public GameObject Movable;  // �K�C�h�̃v���n�u
    Animator animator;
    public static readonly float y = 0.3f;  // �I�u�W�F�N�g��z�u���鍂��
    // static float distance = 0.5f;
    public int test = 0;

    public static readonly int BOARD_SIZE   = 8;    // �{�[�h�̑傫��
    public static readonly int MAX_TURNS    = 60;   // �ő�̃^�[����
    public static readonly int EMPTY        = 0;    // �󂫃}�X�̕\��
    public static readonly int BLACK        = 1;    // ����̕\��
    public static readonly int WHITE        = -1;   // ����̕\��
    public static readonly int WALL         = 2;    // �ǂ̕\��
    public static readonly int MOVABLE      = 3;    // ���ɋ��u�����Ƃ̂ł���_�̕\��

    // ���݂̔Ֆ�
    public int[][] RawBoard = new int[][]{
        new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
        new int[] { 2, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
        new int[] { 2, 0, 0, 0, 0, 0, 0, 0, 0, 2 },    
        new int[] { 2, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
        new int[] { 2, 0, 0, 0, 3, 3, 0, 0, 0, 2 },
        new int[] { 2, 0, 0, 0, 3, 3, 0, 0, 0, 2 },
        new int[] { 2, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
        new int[] { 2, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
        new int[] { 2, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
        new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }
    };
    public int[][][] BoardHistory = new int[MAX_TURNS + 1][][]; // �Ֆʂ̋L�^
    public int[] PassHistory = new int[MAX_TURNS + 1];          // Pass�̋L�^
    public GameObject[,] ObjectBoard = new GameObject[BOARD_SIZE + 2, BOARD_SIZE + 2];  // �\�������̃I�u�W�F�N�g
    public int CurrentColor;    // ���݂̎�ԁA�F
    public int Turns;           // �o�߂����^�[��
    public int BlackScore;      // ���̋
    public int WhiteScore;      // ���̋
    public int Winner = WALL;   // ���҂̔���

    // ������\���r�b�g�}�X�N
    public enum direction
    {
        NONE         = 0,
        UPPER        = 1,
        UPPER_LEFT   = 2,
        LEFT         = 4,
        LOWER_LEFT   = 8,
        LOWER        = 16,
        LOWER_RIGHT  = 32,
        RIGHT        = 64,
        UPPER_RIGHT  = 128,
    }

    
    // �N���b�N�ɉ����Ĕz��ɋ��z�u
    public void PutDisc(Vector3 Pos, int Color)
    {
        RawBoard[(int)Pos.x][(int)Pos.z] = Color;
    }

    // ����ʒu�ɂ���F�̋��u�����Ƃ��ł��邩�𒲂ׂ�
    public direction CheckPosition(int PosX, int PosZ, int Color)
    {
        direction dir = direction.NONE;

        int x, z;

        if (RawBoard[PosX][PosZ] == MOVABLE) { }
        else if (RawBoard[PosX][PosZ] != EMPTY) return direction.NONE;

        if (RawBoard[PosX - 1][PosZ] == -Color)
        {
            x = PosX; z = PosZ;
            while (RawBoard[--x][z] == -Color) ;
            if (RawBoard[x][z] == Color) dir |= direction.UPPER;
        }

        if (RawBoard[PosX + 1][PosZ] == -Color)
        {
            x = PosX; z = PosZ;
            while (RawBoard[++x][z] == -Color) ;
            if (RawBoard[x][z] == Color) dir |= direction.LOWER;
        }

        if (RawBoard[PosX][PosZ - 1] == -Color)
        {
            x = PosX; z = PosZ;
            while (RawBoard[x][--z] == -Color) ;
            if (RawBoard[x][z] == Color) dir |= direction.LEFT;
        }

        if (RawBoard[PosX][PosZ + 1] == -Color)
        {
            x = PosX; z = PosZ;
            while (RawBoard[x][++z] == -Color) ;
            if (RawBoard[x][z] == Color) dir |= direction.RIGHT;
        }

        if (RawBoard[PosX - 1][PosZ - 1] == -Color)
        {
            x = PosX; z = PosZ;
            while (RawBoard[--x][--z] == -Color) ;
            if (RawBoard[x][z] == Color) dir |= direction.UPPER_LEFT;
        }

        if (RawBoard[PosX - 1][PosZ + 1] == -Color)
        {
            x = PosX; z = PosZ;
            while (RawBoard[--x][++z] == -Color) ;
            if (RawBoard[x][z] == Color) dir |= direction.UPPER_RIGHT;
        }

        if (RawBoard[PosX + 1][PosZ - 1] == -Color)
        {
            x = PosX; z = PosZ;
            while (RawBoard[++x][--z] == -Color) ;
            if (RawBoard[x][z] == Color) dir |= direction.LOWER_LEFT;
        }

        if (RawBoard[PosX + 1][PosZ + 1] == -Color)
        {
            x = PosX; z = PosZ;
            while (RawBoard[++x][++z] == -Color) ;
            if (RawBoard[x][z] == Color) dir |= direction.LOWER_RIGHT;
        }

        return dir;
    }

    // �z�u������ɉ����Ĕz����̋���Ђ�����Ԃ�
    public void FlipDisc(Vector3 Pos, direction dir)
    {
        int x, z;
        int PosX = (int)Pos.x;
        int PosZ = (int)Pos.z;

        if((dir & direction.UPPER) != 0)
        {
            x = PosX; z = PosZ;
            while (RawBoard[--x][z] != CurrentColor) RawBoard[x][z] = CurrentColor;
        }

        if ((dir & direction.LOWER) != 0)
        {
            x = PosX; z = PosZ;
            while (RawBoard[++x][z] != CurrentColor) RawBoard[x][z] = CurrentColor;
        }

        if ((dir & direction.LEFT) != 0)
        {
            x = PosX; z = PosZ;
            while (RawBoard[x][--z] != CurrentColor) RawBoard[x][z] = CurrentColor;
        }

        if ((dir & direction.RIGHT) != 0)
        {
            x = PosX; z = PosZ;
            while (RawBoard[x][++z] != CurrentColor) RawBoard[x][z] = CurrentColor;
        }

        if ((dir & direction.UPPER_LEFT) != 0)
        {
            x = PosX; z = PosZ;
            while (RawBoard[--x][--z] != CurrentColor) RawBoard[x][z] = CurrentColor;
        }

        if ((dir & direction.UPPER_RIGHT) != 0)
        {
            x = PosX; z = PosZ;
            while (RawBoard[--x][++z] != CurrentColor) RawBoard[x][z] = CurrentColor;
        }

        if ((dir & direction.LOWER_LEFT) != 0)
        {
            x = PosX; z = PosZ;
            while (RawBoard[++x][--z] != CurrentColor) RawBoard[x][z] = CurrentColor;
        }

        if ((dir & direction.LOWER_RIGHT) != 0)
        {
            x = PosX; z = PosZ;
            while (RawBoard[++x][++z] != CurrentColor) RawBoard[x][z] = CurrentColor;
        }
    }

    // ���z�u�ł��邩�̊m�F
    public direction CheckMovable(int Color, int[][]Board)
    {
        direction dir = direction.NONE;

        for (int x = 0; x < BOARD_SIZE + 2; x++)
        {
            for (int z = 0; z < BOARD_SIZE + 2; z++)
            {
                dir = CheckPosition(x, z, Color);
                if (dir != direction.NONE) return dir;
            }
        }
        return dir;
    }

    // �w�肵���F���u�����Ƃ̂ł���ꏊ��z��ɒǉ�
    public void PointingMovable(int Color)
    {
        direction dir;

        for (int x = 0; x < BOARD_SIZE + 2; x++)
        {
            for (int z = 0; z < BOARD_SIZE + 2; z++)
            {
                dir = CheckPosition(x, z, Color);
                if (RawBoard[x][z] == MOVABLE) RawBoard[x][z] = EMPTY;
                if (dir != direction.NONE) RawBoard[x][z] = MOVABLE;
            }
        }
    }

    // ���_�̍X�V
    public void UpdateScore()
    {
        BlackScore = DiscCount(RawBoard, BLACK);
        WhiteScore = DiscCount(RawBoard, WHITE);
        GameResult();
    }

    // �Q�[�����ʂ̔���
    public void GameResult()
    {
        if ((BlackScore == 0) || (WhiteScore == 0))
        {
            if (WhiteScore == 0) Winner = BLACK;
            if (BlackScore == 0) Winner = WHITE;
        }

        if (Turns == MAX_TURNS)
        {
            if (BlackScore > WhiteScore) Winner = BLACK;
            if (BlackScore < WhiteScore) Winner = WHITE;
            if (BlackScore == WhiteScore) Winner = EMPTY;
        }

        if((CheckMovable(BLACK, RawBoard) == direction.NONE) && (CheckMovable(WHITE, RawBoard) == direction.NONE))
        {
            if (BlackScore > WhiteScore) Winner = BLACK;
            if (BlackScore < WhiteScore) Winner = WHITE;
            if (BlackScore == WhiteScore) Winner = EMPTY;
        }
    }

    // �\�������I�u�W�F�N�g�̍X�V
    public void ObjectUpdate(int[][] CurrentBoard, int[][] NextBoard)
    {
        for (int x = 0; x < CurrentBoard.Length; x++)
        {
            for (int z = 0; z < CurrentBoard[x].Length; z++)
            {
                Vector3 Pos = new Vector3(x, y, z);

                if (CurrentBoard[x][z] == NextBoard[x][z]) { }
                else if (CurrentBoard[x][z] == -NextBoard[x][z])
                {
                    animator = ObjectBoard[x, z].GetComponent<Animator>();
                    if (NextBoard[x][z] == BLACK) animator.Play("ToBlack");
                    if (NextBoard[x][z] == WHITE) animator.Play("ToWhite");
                    //ObjectBoard[x, z].transform.Rotate(new Vector3(180, 0, 0));
                }

                if ((CurrentBoard[x][z] == EMPTY) && (NextBoard[x][z] == MOVABLE))
                {
                    ObjectBoard[x, z] = Instantiate(Movable, Pos, Quaternion.identity);
                }

                if ((CurrentBoard[x][z] == MOVABLE) && (NextBoard[x][z] != MOVABLE))
                {
                    Quaternion rotate = new();
                    Destroy(ObjectBoard[x, z]);
                    if (NextBoard[x][z] == BLACK) rotate = Quaternion.identity;
                    if (NextBoard[x][z] == WHITE) rotate = Quaternion.Euler(180, 0, 0);
                    ObjectBoard[x, z] = Instantiate(Disc, Pos, rotate);
                }

                if ((CurrentBoard[x][z] == MOVABLE) && (NextBoard[x][z] == EMPTY))
                {
                    Destroy(ObjectBoard[x, z]);
                }

                if ((CurrentBoard[x][z] == BLACK) || (CurrentBoard[x][z] == WHITE))
                {
                    if (NextBoard[x][z] == MOVABLE)
                    {
                        Destroy(ObjectBoard[x, z]);
                        ObjectBoard[x, z] = Instantiate(Movable, Pos, Quaternion.identity);
                    }
                }
            }
        }
    }

    // ����Ֆʂ̂���F�𐔂���
    public int DiscCount(int[][] Board, int Color)
    {
        int score = 0;

        for(int i = 0; i < BOARD_SIZE + 2; i++)
        {
            for(int j = 0; j < BOARD_SIZE + 2; j++)
            {
                if (Board[i][j] == Color)
                {
                    score++;
                }
            }
        }
        return score;
    }

    // �z����f�B�[�v�R�s�[����
    public int[][] DeepCopy(int[][] source)
    {
        int[][] copy = new int[source.Length][];
        for (int i = 0; i < source.Length; i++)
        {
            copy[i] = new int[source[i].Length];
            for (int j = 0; j < source[i].Length; j++)
            {
                copy[i][j] = source[i][j];
            }
        }
        return copy;
    }

    public void ReportPass()
    {
        PassHistory[Turns] = 1;
    }

    public void UnReportPass()
    {
        PassHistory[Turns] = 0;
    }

    // Pass�̋L�^
    public void Pass()
    {
        if (CheckMovable(-CurrentColor, RawBoard) == direction.NONE)
        {
            PointingMovable(CurrentColor);
            CurrentColor = -CurrentColor;
            Debug.Log("pass");
            ReportPass();
        }
    }

    // Undo�̏���
    public void Undo()
    {
        if (PassHistory[Turns - 1] == 1) { }
        else if(PassHistory[Turns - 1] == 0) CurrentColor = -CurrentColor;
        RawBoard = DeepCopy(BoardHistory[--Turns]);
        ObjectUpdate(BoardHistory[Turns + 1], BoardHistory[Turns]);
        UnReportPass();
        UpdateScore();
    }

    // 1�^�[���ɍs���������܂Ƃ߂��֐�
    public void TurnProcess(Vector3 position, direction dir, bool object_update)
    {
        PutDisc(position, CurrentColor);
        FlipDisc(position, dir);
        PointingMovable(-CurrentColor);

        Pass();

        CurrentColor = -CurrentColor;
        BoardHistory[++Turns] = DeepCopy(RawBoard);
        if(object_update) ObjectUpdate(BoardHistory[Turns - 1], BoardHistory[Turns]);
        UpdateScore();
    }

    // �f�o�b�O�p �z����f�o�b�O��ʂɕ\������
    public void ListLog(int[][] len)
    {
        for (int i = 0; i < BOARD_SIZE + 2; i++)
        {
            string str = "";
            for (int j = 0; j < BOARD_SIZE + 2; j++)
            {
                str = str + len[i][j] + " ";
            }
            Debug.Log(str);
        }
    }

    // 2�����z��̃W���O�z�񂩂�w�肵���v�f�̃C���f�b�N�X���擾
    public List<int[]> FindAllElementIndices(int[][] jaggedArray, int targetElement)
    {
        List<int[]> indicesList = new List<int[]>();

        for (int i = 0; i < jaggedArray.Length; i++)
        {
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                if (jaggedArray[i][j] == targetElement)
                {
                    indicesList.Add(new int[] { i, j });
                }
            }
        }
        return indicesList;
    }
}
