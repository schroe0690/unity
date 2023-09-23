using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int NPCColor;
    public int x, z;                // �Ֆʂ̈ʒu���
    public Vector3 position;        // �Ֆʂ̈ʒu���(3D�I�u�W�F�N�g�p)

    // Level�ɉ�����NPC���Ăяo���֐�
    public void npc(int[][] Board, int Color, int Level)
    {
        if (Level == 1) RandomPosition(Board);
        if (Level == 2) Negamax(Board, Color, 0);
        if (Level == 3) Negamax(Board, Color, 1);
        // Debug.Log("Position:" + x + "," + z);
    }

    // �����_���Ɏ��ł�NPC�̊֐�
    public void RandomPosition(int[][] Board)
    {
        Othello othello = new();
        System.Random random = new System.Random();
        List<int[]> indices = othello.FindAllElementIndices(Board, Othello.MOVABLE);

        int i = random.Next(indices.Count);
        UpdatePosition(indices[i][0], indices[i][1]);
    }

    // Negamax�̏����ƈʒu���̍X�V���܂Ƃ߂��֐�
    public void Negamax(int[][] Board, int Color, int Turns)
    {
        NPCColor = Color;
        int[] ScoreAndPosition = NegamaxAlgorithm(Board, Color, Turns);
        UpdatePosition(ScoreAndPosition[1], ScoreAndPosition[2]);
    }

    
    // Negamax�̏����{��
    public int[] NegamaxAlgorithm(int[][] Board, int Color, int Turns)
    {
        Othello othello = new();
        int best_score = -1000;
        int[] ScoreAndPositon = new int[] { best_score, 0, 0 };
        int[] tempScoreAndPositon;
        List<int[]> indices = othello.FindAllElementIndices(Board, Othello.MOVABLE);

        for (int i = 0; i < indices.Count; i++)
        {
            othello = new();
            othello.RawBoard = othello.DeepCopy(Board);
            othello.CurrentColor = Color;
            UpdatePosition(indices[i][0], indices[i][1]);
            Othello.direction dir = othello.CheckPosition(indices[i][0], indices[i][1], othello.CurrentColor);
            othello.TurnProcess(position, dir, false);

            if ((Turns == 0) || (indices.Count == 0))
            {
                int score = Evaluation(othello, Color);
                if (score > best_score)
                {
                    best_score = score;
                    ScoreAndPositon = new int[] { best_score, indices[i][0], indices[i][1] };
                }
            }
            else if (Turns >= 1)
            {
                tempScoreAndPositon = NegamaxAlgorithm(othello.RawBoard, othello.CurrentColor, Turns - 1);
                if (-tempScoreAndPositon[0] > best_score)
                {
                    best_score = -tempScoreAndPositon[0];
                    ScoreAndPositon = new int[] { best_score, indices[i][0], indices[i][1] };
                }
            }
        }

        return ScoreAndPositon;

    }

    // �]���֐����܂Ƃ߂�����?(�؂�ւ��₷���悤�ɂ��Ă���)
    public int Evaluation(Othello othello, int Color)
    {
        int patern = 2;
        int score = 0;
        if (patern == 1) score = EvaluationDiscNum(othello, Color);
        if (patern == 2) score = EvaluationScoreBoard(othello, Color);
        if (patern == 3) score = EvaluationMovable(othello, Color);
        if (patern == 4)
        {
            List<int[]> indices = othello.FindAllElementIndices(othello.RawBoard, Othello.EMPTY);
            if (indices.Count > 5) score = EvaluationScoreBoard(othello, Color);
            if (indices.Count <= 5) score = EvaluationDiscNum(othello, Color);
        }

        return score;
    }

    // �����̖�������Ƃ����]���֐�
    public int EvaluationDiscNum(Othello othello, int Color)
    {
        return Color * (othello.BlackScore - othello.WhiteScore);
    }

    // ���_�e�[�u������Ƃ����]���֐�
    public int EvaluationScoreBoard(Othello othello, int Color)
    {
        int BlackScore = 0;
        int WhiteScore = 0;
        int[][] ScoreBoard = new int[][]{
            new int[] { 0,   0,   0,  0,  0,  0,  0,   0,   0, 0 },
            new int[] { 0, 120, -20, 20,  5,  5, 20, -20, 120, 0 },
            new int[] { 0, -20, -40, -5, -5, -5, -5, -40, -20, 0 },
            new int[] { 0,  20,  -5, 15,  3,  3, 15,  -5,  20, 0 },
            new int[] { 0,   5,  -5,  3,  3,  3,  3,  -5,   5, 0 },
            new int[] { 0,   5,  -5,  3,  3,  3,  3,  -5,   5, 0 },
            new int[] { 0,  20,  -5, 15,  3,  3, 15,  -5,  20, 0 },
            new int[] { 0, -20, -40, -5, -5, -5, -5, -40, -20, 0 },
            new int[] { 0, 120, -20, 20,  5,  5, 20, -20, 120, 0 },
            new int[] { 0,   0,   0,  0,  0,  0,  0,   0,   0, 0 },
        };

        int[][] ScoreBoard2 = new int[][]{
            new int[] { 0,   0,   0,  0,  0,  0,  0,   0,   0, 0 },
            new int[] { 0,  30, -12,  0, -1, -1,  0, -12,  30, 0 },
            new int[] { 0, -12, -15, -3, -3, -3, -3, -15, -12, 0 },
            new int[] { 0,   0,  -3,  0, -1, -1,  0,  -3,   0, 0 },
            new int[] { 0,  -1,  -3, -1, -1, -1, -1,  -3,  -1, 0 },
            new int[] { 0,  -1,  -3, -1, -1, -1, -1,  -3,  -1, 0 },
            new int[] { 0,   0,  -3,  0, -1, -1,  0,  -3,   0, 0 },
            new int[] { 0, -12, -15, -3, -3, -3, -3, -15, -12, 0 },
            new int[] { 0,  30, -12,  0, -1, -1,  0, -12,  30, 0 },
            new int[] { 0,   0,   0,  0,  0,  0,  0,   0,   0, 0 },
        };

        for (int i = 0; i < ScoreBoard.Length; i++)
        {
            for(int j = 0; j < ScoreBoard[i].Length; j++)
            {
                if (othello.RawBoard[i][j] == Othello.BLACK) BlackScore += ScoreBoard2[i][j];
                if (othello.RawBoard[i][j] == Othello.WHITE) WhiteScore += ScoreBoard2[i][j];
            }
        }

        return Color * (BlackScore - WhiteScore);

    }

    // �u�����Ƃ̂ł���ꏊ�̐�����Ƃ����]���֐�
    public int EvaluationMovable(Othello othello, int Color)
    {
        List<int[]> indices = othello.FindAllElementIndices(othello.RawBoard, Othello.MOVABLE);
        return Color * indices.Count;
    }

    // ����NPC�N���X���񋟂���Ֆʂ̈ʒu�����X�V����
    public void UpdatePosition(int i, int j)
    {
        x = i;
        z = j;
        position = new Vector3(x, Othello.y, z);
    }
}
