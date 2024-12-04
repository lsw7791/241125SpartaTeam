using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    public void DropPosition(Vector2 inDropPosition)
    {
        float randomXPosition = Random.Range(0, 2);
        float randomYPosition = Random.Range(0, 2);

        transform.position = new Vector2 (inDropPosition.x + randomXPosition, inDropPosition.y + randomYPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var outPlayer))
        {
            Destroy(gameObject);
        }
    }
}
