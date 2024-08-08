using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{

    public TextAsset playerInfo;
    public TextAsset[] characterInfo;
    public TextMeshProUGUI textObject;

    void Start()
    {
        AssesText();
        textObject.text = GetText();
    }
    void AssesText()
    {

        playerInfo = Resources.Load<TextAsset>("Character/PlayerData");
        characterInfo = Resources.LoadAll<TextAsset>("Character");
    
    }

    public string GetText()
    {
        return playerInfo.text;
    }



}
