using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuProfile : UIProfile
{
    [SerializeField] private TMP_Text StageLevel;
    [SerializeField] private Button playButton;

    public void UpdateStageLevel(int level)
    {
        StageLevel.text = $"Stage {level}";
    }

    public void LoadGame()
    {
        SceneManagement.instance.LoadSceneMgr(Cscene.Level_1);
    }
    // Additional functionality for main menu UI can be added here


}
