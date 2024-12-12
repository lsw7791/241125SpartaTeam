using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageToForest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.sceneController.LoadScene(GameManager.Instance.dataManager.scene.GetMapTo(1));
            //여기다가 충돌안 플레이어 Vector2지정 위치로 이동시키고 싶다. 플레이어는 파괴되지 않는다

            // 씬 전환 전에 플레이어의 위치 설정
            collision.transform.position = GameManager.Instance.dataManager.scene.GetMoveTransform(1);
        }
    }
}
