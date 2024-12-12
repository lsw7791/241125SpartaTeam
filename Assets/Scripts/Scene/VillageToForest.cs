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
            //����ٰ� �浹�� �÷��̾� Vector2���� ��ġ�� �̵���Ű�� �ʹ�. �÷��̾�� �ı����� �ʴ´�

            // �� ��ȯ ���� �÷��̾��� ��ġ ����
            collision.transform.position = GameManager.Instance.dataManager.scene.GetMoveTransform(1);
        }
    }
}
