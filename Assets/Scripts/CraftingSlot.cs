using UnityEngine;
using UnityEngine.UI;
using MainData;

public class CraftingSlot : MonoBehaviour
{ // ������ ���� ������ UI�� ����
    private Button craftButton; // ũ����Ʈ ��ư
    private CraftingData craftingData;

    private void Start()
    {
        craftButton = GetComponent<Button>();

        craftButton.onClick.AddListener(() =>
        {
            CraftingUI craftingUI = GameManager.Instance.uIManager.OpenUI<CraftingUI>();
            craftingUI.Init(craftingData);
        });
    }

    public void Init(CraftingData inCraftingData)
    {
        craftingData = inCraftingData;
    }

    //// ũ����Ʈ ��ư Ŭ�� �� ó���ϴ� �޼���
    //public void OnCraftButtonClick()
    //{ // ��ư Ŭ�� ��, GameManager�� CraftingManager�� ���� ������ ������ ó��
    //    // CraftingManager���� ������ ���� �� ũ����Ʈ �õ�
    //    if (craftingData != null)
    //    {
    //        GameManager.Instance.craftingManager.SelectItem(craftingData.id); // ������ ����
    //    }
    //}
    //// ���� ������ ������ ���� �ܰ�
}
