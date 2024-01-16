using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D mapCollider2D;

    private void Awake()
    {
        SetupRandomPosition();
    }

    private void SetupRandomPosition()
    {
        Bounds bounds = mapCollider2D.bounds;

        int x = Random.Range((int)bounds.min.x, (int)bounds.max.x + 1);
        int y = Random.Range((int)bounds.min.y, (int)bounds.max.y + 1);

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetupRandomPosition();
    }
}
