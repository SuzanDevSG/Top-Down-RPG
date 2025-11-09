using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHealthProfile : UIProfile
{
    [SerializeField] private Transform HeartsPanel;
    [SerializeField] private GameObject HeartsPrefab;
    [SerializeField] private Sprite FullHeartSprite;
    [SerializeField] private Sprite EmptyHeartSprite;

    private List<GameObject> heartsList;
    public void UpdateHealthUI(float currentHealth,float maxHealth)
    {
        if(heartsList != null)
        {
            foreach (GameObject heart in heartsList)
            {
                Destroy(heart);
            }
        }

        heartsList = new List<GameObject>();
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject hearts = Instantiate(HeartsPrefab, transform);
            heartsList.Add(hearts);
            if (i < currentHealth)
            {
                hearts.GetComponent<Image>().sprite = FullHeartSprite;
            }
            else
            {
                hearts.GetComponent<Image>().sprite = EmptyHeartSprite;
            }
        }
    }
}