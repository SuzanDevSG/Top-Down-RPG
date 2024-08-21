using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIProfile : MonoBehaviour
{
    public int id;
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    public void ShowProfile()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

    }

    public void HideProfile()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

    }

}
