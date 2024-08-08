using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Books : MonoBehaviour, IPointerClickHandler
{
    public int sales;
    public int price;
    public int income;
    public int Profit;


    public void OnPointerClick(PointerEventData eventData)
    {
        Calculate();
    }

    protected abstract void Calculate();
}
