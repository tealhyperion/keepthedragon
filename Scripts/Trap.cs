using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject prefabToRemove; // 인스펙터 창에서 프리팹을 지정할 변수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 "Segment" 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Segment"))
        {
            // 충돌한 오브젝트가 "Segment" 태그를 가지고 있는지 확인
            if (collision.gameObject.CompareTag("Segment"))
            {
                // 프리팹이 지정되어 있으면 해당 프리팹을 제거합니다.
                if (prefabToRemove != null)
                {
                    // 프리팹의 인스턴스를 파괴
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
