using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionUI : UIBase
{
    public Player player; // 저장할 플레이어 참조

    public void OnClickedExitBtn()
    {
        player = GameManager.Instance.player;
        // 데이터 저장 (SaveLoadManager는 static이므로 클래스 이름으로 호출)
        SaveLoadManager.SavePlayerData(player, "DefaultSave");

        // 타이틀 씬으로 이동
        SceneManager.LoadScene("TitleScene");
    }
}
