using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SnakeController : MonoBehaviour
{
    [Header("Snake Movement")]
    [SerializeField]
    private float moveTime = 0.1f;                  // 한 칸 이동 시간
    private Vector2 moveDirection = Vector2.right;      // 이동 방향
                                                        // 실제 Snake 이동방향은 안바뀌었지만 입력 방향에 의해 같은 축으로 이동이 가능한 것을 방지
    private Vector2 lastInputDirection = Vector2.right;

    [Header("Snake Segments")]
    [SerializeField]
    private Transform segmentPrefab;                        // Segment 프리팹
    [SerializeField]
    private int spawnSegmentCountAtStart = 4;       // 게임 시작 시 Snake의 길이 (머리 포함)
    private List<Transform> segments = new List<Transform>();   // Snake와 Segment를 관리하는 리스트

    [Header("MapCollider")]
    [SerializeField]
    private BoxCollider2D mapCollider2D;                        // 맵을 벗어나는지 검사하기 위한 맵의 충돌 범위 정보

    private IEnumerator Start()
    {
        Setup();
        Time.timeScale = 0;

        while (true)
        {
            MoveSegments();

            yield return StartCoroutine("WaitForSeconds", moveTime);
        }
    }
     
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            Time.timeScale = 1;
        }
        // 현재 x축으로 이동중이면 y축 방향으로만 방향 전환 가능
        if (moveDirection.x != 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) lastInputDirection = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.DownArrow)) lastInputDirection = Vector2.down;
        }
        // 현재 y축으로 이동중이면 x축 방향으로만 방향 전환 가능
        else if (moveDirection.y != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) lastInputDirection = Vector2.left;
            else if (Input.GetKeyDown(KeyCode.RightArrow)) lastInputDirection = Vector2.right;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            AddSegment();
        }
        else if (collision.CompareTag("Segment") || collision.CompareTag("Wall"))
        {
            Debug.Log($"획득 점수 : {segments.Count}");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator WaitForSeconds(float time)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            yield return null;
        }
    }

    private void Setup()
    {
        // Snake 본체를 segments 리스트에 저장
        segments.Add(transform);

        // Snake를 쫓아다니는 꼬리(segment 오브젝트)를 생성하고, segments 리스트에 저장
        for (int i = 0; i < spawnSegmentCountAtStart - 1; ++i)
        {
            AddSegment();
        }
    }

    private void AddSegment()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void MoveSegments()
    {
        // 실제 이동할 때 마지막 입력 방향(lastInputDirection)으로 이동하도록 설정
        moveDirection = lastInputDirection;

        for (int i = segments.Count - 1; i > 0; --i)
        {
            segments[i].position = segments[i - 1].position;
        }

        transform.position = (Vector2)transform.position + moveDirection;

        Bounds bounds = mapCollider2D.bounds;

        if (transform.position.x < bounds.min.x || transform.position.x > bounds.max.x ||
             transform.position.y < bounds.min.y || transform.position.y > bounds.max.y)
        {
            Debug.Log($"획득 점수 : {segments.Count}");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}