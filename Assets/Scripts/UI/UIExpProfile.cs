using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIExpProfile : UIProfile
{
    [SerializeField] private float fillSpeed = 10f;

    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text level;
    private float expValue = 0;
    public void SetExpValue(float currentexp, float maxValue, int playerLevel)
    {
        expSlider.maxValue = maxValue;
        expSlider.value = 0;
        expValue = currentexp;
        level.text = "Level: " + playerLevel;
    }
    public void UpdateExpBar(float currentExp)
    {
        expValue = currentExp;
    }
    private void Update()
    {
        if (expValue - expSlider.value >= 3)
        {
            fillSpeed *= 3;
        }
        else
        {
            fillSpeed = 10f;
        }
        if (expSlider.value < expValue)
        {
            expSlider.value += Time.deltaTime * fillSpeed;
        }
    }
}