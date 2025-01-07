using System.IO;
using TMPro;
using Unity.VisualScripting;

public class CharacterSelectUI : UIBase
{
    public bool isNewGame;
    public TextMeshProUGUI[] slotText;
    public TextMeshProUGUI slotSelectButtonText;
    bool[] _savefile = new bool[4];

    public void GameStart()
    {
        DataManager dataManager = GameManager.Instance.DataManager;

        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(dataManager.savePath + $"/PlayerData{i.ToString()}.txt"))
            {
                _savefile[i] = true; // 해당 슬롯 번호의 bool배열 true로 변환
                dataManager.nowSlot = i; // 선택한 슬롯 번호 저장
                GameManager.Instance.nowPlayer = dataManager.GetLoadData<PlayerData>(); // 해당 슬롯 데이터 불러옴
                slotText[i].text = GameManager.Instance.nowPlayer.NickName;	// 데이터의 이름을 버튼에 출력
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        // 사용할 데이터가 아니기 때문에 초기화
        dataManager.DataClear();
    }

    public void Slot(int inSlotNumber)
    {
        DataManager dataManager = GameManager.Instance.DataManager;

        dataManager.nowSlot = inSlotNumber; // 선택한 슬롯의 번호를 현재 번호로 사용

        if (dataManager.IsData<PlayerData>())
        {
            slotSelectButtonText.text = "게임 시작";
        }
        else
        {
            slotSelectButtonText.text = "캐릭터 생성";
        }
    }

    public void SlotSelect()
    {
        SoundManager.Instance.PlayButton2SFX();
        DataManager dataManager = GameManager.Instance.DataManager;

        if(dataManager.nowSlot == -1)
        {
            return;
        }

        if (_savefile[dataManager.nowSlot])
        {
            isNewGame = false;
            GameManager.Instance.nowPlayer = dataManager.GetLoadData<PlayerData>();
            GameManager.Instance.nowInventory = dataManager.GetLoadData<Inventory>();
            //GameManager.Instance.nowEquipment = dataManager.GetLoadData<Equipment>();

            GameManager.Instance.StartGame();
            UIManager.Instance.ToggleUI<CharacterSelectUI>();
        }
        else
        {
            isNewGame = true;
            GameManager.Instance.SceneNum = 24;
            GameManager.Instance.LoadScene(dataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
            UIManager.Instance.ToggleUI<CharacterSelectUI>();
        }
    }

    public void SlotDelete()
    {
        SoundManager.Instance.PlayButton2SFX();
        DataManager dataManager = GameManager.Instance.DataManager;

        if (dataManager.nowSlot == -1)
        {
            return;
        }

        if (_savefile[dataManager.nowSlot])
        {
            dataManager.DeleteData<PlayerData>();
            dataManager.DeleteData<Inventory>();
            //dataManager.DeleteData<Equipment>();
            _savefile[dataManager.nowSlot] = false;
            dataManager.DataClear();

            GameStart();
        }
    }
}
