using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void GameSelectScene()
    {
        SceneManager.LoadScene("SelectScene"); // 지정된 씬으로 변경
    }

    public void GameOptionScene()
    {
        SceneManager.LoadScene("OptionScene"); // 지정된 씬으로 변경
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서는 에디터를 종료합니다.
#else
            Application.Quit(); // 빌드된 런타임에서는 애플리케이션을 종료합니다.
#endif
    }
}
