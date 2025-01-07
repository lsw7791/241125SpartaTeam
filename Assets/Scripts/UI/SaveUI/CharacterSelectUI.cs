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
                _savefile[i] = true; // �ش� ���� ��ȣ�� bool�迭 true�� ��ȯ
                dataManager.nowSlot = i; // ������ ���� ��ȣ ����
                GameManager.Instance.nowPlayer = dataManager.GetLoadData<PlayerData>(); // �ش� ���� ������ �ҷ���
                slotText[i].text = GameManager.Instance.nowPlayer.NickName;	// �������� �̸��� ��ư�� ���
            }
            else
            {
                slotText[i].text = "�������";
            }
        }
        // ����� �����Ͱ� �ƴϱ� ������ �ʱ�ȭ
        dataManager.DataClear();
    }

    public void Slot(int inSlotNumber)
    {
        DataManager dataManager = GameManager.Instance.DataManager;

        dataManager.nowSlot = inSlotNumber; // ������ ������ ��ȣ�� ���� ��ȣ�� ���

        if (dataManager.IsData<PlayerData>())
        {
            slotSelectButtonText.text = "���� ����";
        }
        else
        {
            slotSelectButtonText.text = "ĳ���� ����";
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
