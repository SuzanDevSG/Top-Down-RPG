using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Cscene
{
    MainMenu,       Level_1,    Easy_Level2,
    Normal_Level2,  Normal_Level1,
    Hard_Level1,    Hard_Level2,    Customize
}

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null && instance != this)
            Destroy(this);
    }
    public void LoadSceneMgr(Enum Cscene)
    {
        SceneManager.LoadSceneAsync(Cscene.ToString());
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevelScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitYes()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}