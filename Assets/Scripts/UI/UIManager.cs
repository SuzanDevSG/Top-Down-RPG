using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private UIProfile[] uIProfiles;
    private Dictionary<int, UIProfile> _profiles = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;

        for (int i = 0; i < uIProfiles.Length; i++)
        {
            _profiles.Add(uIProfiles[i].id, uIProfiles[i]);
        }
    }
    public void ShowProfile(int id)
    {
        if (_profiles.ContainsKey(id))
        {
            _profiles[id].ShowProfile();
        }
    }
    public void HideProfile(int id)
    {
        if (_profiles.ContainsKey(id))
        {
            _profiles[id].HideProfile();
        }
    }
    // Exp Methods
    public void SetExpUI(float currentExp, float maxExp, int playerLevel)
    {
        foreach (UIProfile profile in _profiles.Values)
        {
            if(profile is UIExpProfile expProfile)
                expProfile.SetExpValue(currentExp, maxExp, playerLevel);
        }
    }
    public void UpdateExpUI(float currentExp)
    {
        //_profiles.Where(p => p.Value is UIExpProfile).ToList().ForEach(p =>
        //{
        //    (p.Value as UIExpProfile).UpdateExpBar(currentExp, playerLevel);
        //});

        foreach (UIProfile profile in _profiles.Values)
        {
            if(profile is UIExpProfile expProfile)
                expProfile.UpdateExpBar(currentExp);
        }
    }

    // Health Methods
    public void UpdateHealthUI(float currentHealth, float maxHealth = 5)
    {
        foreach (UIProfile uIProfile in _profiles.Values)
        {
            if (uIProfile is UIHealthProfile healthUI)
            {
                healthUI.UpdateHealthUI(currentHealth, maxHealth);
            }
        }
    }

    // 
}

