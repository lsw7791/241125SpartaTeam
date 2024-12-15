using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionUI : UIBase
{
    public Player player; // ������ �÷��̾� ����

    public void OnClickedExitBtn()
    {
        player = GameManager.Instance.player;
        // ������ ���� (SaveLoadManager�� static�̹Ƿ� Ŭ���� �̸����� ȣ��)
        SaveLoadManager.SavePlayerData(player, "DefaultSave");

        // Ÿ��Ʋ ������ �̵�
        SceneManager.LoadScene("TitleScene");
    }
}
