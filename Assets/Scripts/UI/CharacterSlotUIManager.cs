//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class CharacterSlotUIManager : MonoBehaviour
//{
//    public GameObject slotPrefab;  // ���� UI ������
//    public Transform slotParent;  // ���� �θ�
//    private List<GameObject> slotObjects = new List<GameObject>();  // ���� ��ü ����Ʈ

//    void Awake()
//    {
//        // slotParent�� �����Ϳ��� �Ҵ���� �ʾҴٸ�, ���� ó��
//        if (slotParent == null)
//        {
//            Debug.LogError("SlotParent�� �Ҵ���� �ʾҽ��ϴ�! ���� ������Ʈ���� Ȯ���ϼ���.");
//            return;
//        }

//        LoadSlots();  // ���� �ε�
//    }

//    public void LoadSlots()
//    {
//        if (GameManager.Instance == null)
//        {
//            Debug.LogError("GameManager�� �ʱ�ȭ���� �ʾҽ��ϴ�!");
//            return;
//        }

//        List<PlayerData> loadedCharacters = GameManager.Instance.GetAllPlayerData();  // ������ �ε�

//        // ���� ���� UI ����
//        foreach (Transform child in slotParent)
//        {
//            Destroy(child.gameObject);
//        }

//        slotObjects.Clear();  // ���� ��ü ����Ʈ �ʱ�ȭ

//        // ���ο� ���� UI ����
//        foreach (var character in loadedCharacters)
//        {
//            GameObject newSlot = Instantiate(slotPrefab, slotParent);
//            var slotComponent = newSlot.GetComponent<CharacterSlot>();
//            slotComponent.InitializeSlot(character, OnSlotSelected);

//            slotObjects.Add(newSlot);  // ���� ��ü ����Ʈ�� �߰�
//        }
//    }

//    // ���� ���� �� ó��
//    private void OnSlotSelected(PlayerData selectedCharacter)
//    {
//        Debug.Log($"ĳ���� {selectedCharacter.NickName} ���õ�!");
//        GameManager.Instance.StartGame(selectedCharacter);
//    }

//    // UI ������Ʈ �޼���
//    public void UpdateSlotUI(List<PlayerData> playerDataList)
//    {
//        // ���� ���� UI ����
//        foreach (var slotObject in slotObjects)
//        {
//            Destroy(slotObject);
//        }
//        slotObjects.Clear();

//        // ���ο� ���� UI ����
//        foreach (var playerData in playerDataList)
//        {
//            GameObject newSlot = Instantiate(slotPrefab, slotParent);
//            // TextMeshProUGUI�� ����
//            TextMeshProUGUI slotText = newSlot.GetComponentInChildren<TextMeshProUGUI>();

//            if (slotText != null)
//            {
//                slotText.text = playerData.NickName; // ĳ���� �̸� ����
//            }

//            slotObjects.Add(newSlot); // ���� ������Ʈ ����Ʈ�� �߰�
//        }
//    }
//}
