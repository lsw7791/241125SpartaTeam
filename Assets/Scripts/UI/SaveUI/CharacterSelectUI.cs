using System.IO;
using TMPro;
using UnityEngine;

public class CharacterSelectUI : UIBase
{
    public bool isNewGame;
    public TextMeshProUGUI[] slotText;
    bool[] savefile = new bool[4];

    void OnEnable()
    {
        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(GameManager.Instance.DataManager.path + $"{i}"))
            {
                savefile[i] = true; // �ش� ���� ��ȣ�� bool�迭 true�� ��ȯ
                GameManager.Instance.DataManager.nowSlot = i; // ������ ���� ��ȣ ����
                GameManager.Instance.DataManager.LoadData(); // �ش� ���� ������ �ҷ���
                slotText[i].text = GameManager.Instance.DataManager.nowPlayer.NickName;	// �������� �̸��� ��ư�� ���
            }
            else
            {
                slotText[i].text = "�������";
            }
        }
        // ����� �����Ͱ� �ƴϱ� ������ �ʱ�ȭ
        GameManager.Instance.DataManager.DataClear();
    }

    public void Slot(int number)
    {
        GameManager.Instance.DataManager.nowSlot = number; // ������ ������ ��ȣ�� ���� ��ȣ�� ���

        if (savefile[number])
        {
            GameManager.Instance.DataManager.LoadData();

            GameManager.Instance.StartGame(GameManager.Instance.DataManager.nowPlayer);
            UIManager.Instance.ToggleUI<CharacterSelectUI>();
            GameManager.Instance.Player.Inventory.Items = GameManager.Instance._currentPlayer.Items;
        }
        else
        {
            GameManager.Instance.SceneNum = 24;
            GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
            UIManager.Instance.ToggleUI<CharacterSelectUI>();
        }
    }
}
