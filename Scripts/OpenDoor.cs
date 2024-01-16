using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public string playerTag = "Segment";  // Ư�� �±׸� ���� ���� ������Ʈ�� �÷��̾�� ����մϴ�.
    public GameObject[] interactableObjects;
    public Transform[] targetPositions;
    public float moveSpeed = 5f;

    private Vector3[] originalPositions;
    private bool[] isPlayerInside;

    int i = 0;

    private void Start()
    {
        originalPositions = new Vector3[interactableObjects.Length];
        isPlayerInside = new bool[interactableObjects.Length];

        for (int i = 0; i < interactableObjects.Length; i++)
        {
            originalPositions[i] = interactableObjects[i].transform.position;
        }
    }

    private void Update()
    {
        for (int i = 0; i < interactableObjects.Length; i++)
        {
            if (isPlayerInside[i])
            {
                MoveObjectToTarget(i);
            }
            else
            {
                ReturnObjectToOriginalPosition(i);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �±׸� ����Ͽ� Ư�� �±׸� ���� ���� ������Ʈ���� �浹�� Ȯ���մϴ�.
        if (other.CompareTag(playerTag))
        {
            i++;
            Debug.Log("a");
            for (int j = 0; j < interactableObjects.Length; j++)
            {
                isPlayerInside[j] = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �±׸� ����Ͽ� Ư�� �±׸� ���� ���� ������Ʈ���� �浹�� Ȯ���մϴ�.
        if (other.CompareTag(playerTag))
        {
            i--;
            if (i != 0) return;
            for (int j = 0; j < interactableObjects.Length; j++)
            {
                isPlayerInside[j] = false;
            }
        }
    }

    private void MoveObjectToTarget(int index)
    {
        interactableObjects[index].transform.position = targetPositions[index].position;
    }

    private void ReturnObjectToOriginalPosition(int index)
    {
        interactableObjects[index].transform.position = originalPositions[index];
    }
}
