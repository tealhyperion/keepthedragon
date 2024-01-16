using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SnakeController : MonoBehaviour
{
    [Header("Snake Movement")]
    [SerializeField]
    private float moveTime = 0.1f;                  // �� ĭ �̵� �ð�
    private Vector2 moveDirection = Vector2.right;      // �̵� ����
                                                        // ���� Snake �̵������� �ȹٲ������ �Է� ���⿡ ���� ���� ������ �̵��� ������ ���� ����
    private Vector2 lastInputDirection = Vector2.right;

    [Header("Snake Segments")]
    [SerializeField]
    private Transform segmentPrefab;                        // Segment ������
    [SerializeField]
    private int spawnSegmentCountAtStart = 4;       // ���� ���� �� Snake�� ���� (�Ӹ� ����)
    private List<Transform> segments = new List<Transform>();   // Snake�� Segment�� �����ϴ� ����Ʈ

    [Header("MapCollider")]
    [SerializeField]
    private BoxCollider2D mapCollider2D;                        // ���� ������� �˻��ϱ� ���� ���� �浹 ���� ����

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
        // ���� x������ �̵����̸� y�� �������θ� ���� ��ȯ ����
        if (moveDirection.x != 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) lastInputDirection = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.DownArrow)) lastInputDirection = Vector2.down;
        }
        // ���� y������ �̵����̸� x�� �������θ� ���� ��ȯ ����
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
            Debug.Log($"ȹ�� ���� : {segments.Count}");
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
        // Snake ��ü�� segments ����Ʈ�� ����
        segments.Add(transform);

        // Snake�� �Ѿƴٴϴ� ����(segment ������Ʈ)�� �����ϰ�, segments ����Ʈ�� ����
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
        // ���� �̵��� �� ������ �Է� ����(lastInputDirection)���� �̵��ϵ��� ����
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
            Debug.Log($"ȹ�� ���� : {segments.Count}");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}