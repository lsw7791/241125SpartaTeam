using UnityEngine;

public class PlayerCreation : MonoBehaviour
{
    public CharacterSlotManager characterSlotManager;

    // ĳ���� ���� �Լ�
    public void CreateNewCharacter()
    {
        // ���ο� ĳ���͸� �����
        Player newPlayer = new Player(); // �⺻������ ���ο� Player ��ü ����

        // ĳ���� ���Կ� �ڵ����� ����
        characterSlotManager.SaveCharacterData();

        Debug.Log("���ο� ĳ���� ���� �� �ڵ� ���� �Ϸ�");
    }
}
