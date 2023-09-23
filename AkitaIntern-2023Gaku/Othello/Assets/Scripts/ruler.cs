using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ruler : MonoBehaviour
{
    public GameObject Disc;     // ��̃v���n�u
    public GameObject Movable;  // �K�C�h�̃v���n�u

    Othello othello = new();
    player Player = new();
    NPC npc = new();

    public bool anim_sleep = false;     // �A�j���[�V�����ҋ@�̔���
    public bool undo = false;           // undo�����̔���
    public bool undo_continue = false;  // undo�𑱂��邩�̔���(��cpu��̎�)
    
    // Start is called before the first frame update
    void Start()
    {
        othello.Disc = Disc;
        othello.Movable = Movable;

        int[][] TempRawBoard = othello.DeepCopy(othello.RawBoard);
        othello.PutDisc(new Vector3(4f, Othello.y, 5f), Othello.BLACK);
        othello.PutDisc(new Vector3(5f, Othello.y, 4f), Othello.BLACK);
        othello.PutDisc(new Vector3(4f, Othello.y, 4f), Othello.WHITE);
        othello.PutDisc(new Vector3(5f, Othello.y, 5f), Othello.WHITE);

        othello.CurrentColor = Othello.BLACK;
        othello.Turns = 0;

        othello.PointingMovable(othello.CurrentColor);
        othello.BoardHistory[othello.Turns] = othello.DeepCopy(othello.RawBoard);
        othello.ObjectUpdate(TempRawBoard, othello.BoardHistory[othello.Turns]);
        othello.UpdateScore();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        // �A�j���[�V�����ҋ@
        if (anim_sleep) { }
        // �Q�[���I�����̏���
        else if (othello.Winner != Othello.WALL) UpdateUI();
        // �v���C���[�ΐ�̏���
        else if (ClickProcessTitle.mode == "player")
        {
            if (undo && (othello.Turns != 0)) Undo();
            else if (Player.GetPosition())
            {
                Othello.direction dir = othello.CheckPosition(Player.x, Player.z, othello.CurrentColor);
                if ((dir != Othello.direction.NONE)) TurnProcess(Player.position, dir);
            }
        }
        // CPU��̏���
        else if (ClickProcessTitle.mode == "cpu")
        {
            if (undo && (othello.Turns >= 2)) undo_continue = true;
            else if (undo_continue)
            {
                Undo();
                if (othello.CurrentColor == CPUSetting.PlayerColor) undo_continue = false;
            }
            else if (othello.CurrentColor == -CPUSetting.PlayerColor)
            {
                npc.npc(othello.RawBoard, othello.CurrentColor, CPUSetting.CPULevel);
                Othello.direction dir = othello.CheckPosition(npc.x, npc.z, othello.CurrentColor);
                TurnProcess(npc.position, dir);
            }
            else if ((Player.GetPosition()) && (othello.CurrentColor == CPUSetting.PlayerColor))
            {
                Othello.direction dir = othello.CheckPosition(Player.x, Player.z, othello.CurrentColor);
                if ((dir != Othello.direction.NONE)) TurnProcess(Player.position, dir);
            }
        }
        
        undo = false;
    }

    // 1�^�[���ōs���������܂Ƃ߂��֐�
    public void TurnProcess(Vector3 position, Othello.direction dir)
    {
        othello.TurnProcess(position, dir, true);
        UpdateUI();
    }

    // Undo�̏���
    public void Undo()
    {
        othello.Undo();
        UpdateUI();
    }

    // �\������e�L�X�g�̍X�V
    public void UpdateUI()
    {
        UpdateUI ui = GameObject.Find("EventSystem").GetComponent<UpdateUI>();
        ui.UpdateScore(othello.BlackScore, othello.WhiteScore);
        if(othello.Winner != Othello.WALL) ui.UpdateWinner(othello.Winner);
        else if(othello.Turns != 0) ui.UpdatePass(othello.PassHistory[othello.Turns - 1]);
    }
}
