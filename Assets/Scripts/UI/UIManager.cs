using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private UIProfile[] uIProfiles;
    private Dictionary<int, UIProfile> _profiles;
    [SerializeField] private UIHealthProfile healthProfile;

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
    private void Start()
    {

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

    public void UpdateHealthUI(float currentHealth, float maxHealth = 5)
    {

        healthProfile.UpdateHealthUI(currentHealth, maxHealth);

    }



}
