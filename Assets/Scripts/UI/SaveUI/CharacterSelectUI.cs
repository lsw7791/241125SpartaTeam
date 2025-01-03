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
                savefile[i] = true; // 해당 슬롯 번호의 bool배열 true로 변환
                GameManager.Instance.DataManager.nowSlot = i; // 선택한 슬롯 번호 저장
                GameManager.Instance.DataManager.LoadData(); // 해당 슬롯 데이터 불러옴
                slotText[i].text = GameManager.Instance.DataManager.nowPlayer.NickName;	// 데이터의 이름을 버튼에 출력
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        // 사용할 데이터가 아니기 때문에 초기화
        GameManager.Instance.DataManager.DataClear();
    }

    public void Slot(int number)
    {
        GameManager.Instance.DataManager.nowSlot = number; // 선택한 슬롯의 번호를 현재 번호로 사용

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
