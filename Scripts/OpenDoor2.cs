using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor2 : MonoBehaviour
{
    public string playerTag = "Segment";
    public GameObject[] interactableObjects;
    public Transform[] targetRotations;  // 목표 회전을 위한 배열 사용

    private Quaternion[] originalRotations;  // 원래 회전값을 저장하기 위한 배열
    private bool[] isPlayerInside;

    int i = 0;

    private void Start()
    {
        originalRotations = new Quaternion[interactableObjects.Length];
        isPlayerInside = new bool[interactableObjects.Length];

        for (int i = 0; i < interactableObjects.Length; i++)
        {
            originalRotations[i] = interactableObjects[i].transform.rotation;
        }
    }

    private void Update()
    {
        for (int i = 0; i < interactableObjects.Length; i++)
        {
            if (isPlayerInside[i])
            {
                RotateObjectToTarget(i);
            }
            else
            {
                //ReturnObjectToOriginalRotation(i);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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

    private void RotateObjectToTarget(int index)
    {
        interactableObjects[index].transform.rotation = targetRotations[index].rotation;
    }

    //private void ReturnObjectToOriginalRotation(int index)
    //{
    //    interactableObjects[index].transform.rotation = originalRotations[index];
    //}
}
