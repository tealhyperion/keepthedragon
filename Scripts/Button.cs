using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void GameSelectScene()
    {
        SceneManager.LoadScene("SelectScene"); // ������ ������ ����
    }

    public void GameOptionScene()
    {
        SceneManager.LoadScene("OptionScene"); // ������ ������ ����
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ����� �����͸� �����մϴ�.
#else
            Application.Quit(); // ����� ��Ÿ�ӿ����� ���ø����̼��� �����մϴ�.
#endif
    }
}
