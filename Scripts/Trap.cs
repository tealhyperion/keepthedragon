using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject prefabToRemove; // �ν����� â���� �������� ������ ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� "Segment" �±׸� ������ �ִ��� Ȯ��
        if (collision.gameObject.CompareTag("Segment"))
        {
            // �浹�� ������Ʈ�� "Segment" �±׸� ������ �ִ��� Ȯ��
            if (collision.gameObject.CompareTag("Segment"))
            {
                // �������� �����Ǿ� ������ �ش� �������� �����մϴ�.
                if (prefabToRemove != null)
                {
                    // �������� �ν��Ͻ��� �ı�
                    DestroyImmediate(gameObject);
                }
                else
                {
                    Debug.LogWarning("Prefab not assigned in the inspector!");
                }
            }
        }
    }
}
