using UnityEngine;

public class PlayerCreation : MonoBehaviour
{
    public CharacterSlotManager characterSlotManager;

    // 캐릭터 생성 함수
    public void CreateNewCharacter()
    {
        // 새로운 캐릭터를 만들고
        Player newPlayer = new Player(); // 기본값으로 새로운 Player 객체 생성

        // 캐릭터 슬롯에 자동으로 저장
        characterSlotManager.SaveCharacterData();

        Debug.Log("새로운 캐릭터 생성 및 자동 저장 완료");
    }
}
