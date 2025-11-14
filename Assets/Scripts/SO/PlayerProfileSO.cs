using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Profile")]
public class PlayerProfileSO : ScriptableObject
{
    public PlayerProfile profile;
}
[Serializable]
public class PlayerProfile
{
    public int id;
    public string Name;
    public int level = 1;
    public int maxHealth = 5;
    public float maxSpeed = 3;
    public float interactionRadius = 3f;
    [Range(0, 1)]
    public float maxLookSpeed = .5f;
}
[Serializable]
public class PlayerSession
{
    public PlayerProfile profile;

    public float experiencePoints = 0;
    public float maxExperiencePoints = 5;
    public int LevelProgress = 1;
    public PlayerSession(PlayerProfile profile)
    {
        // Make a copy so we can modify stats safely
        this.profile = new PlayerProfile
        {
            id = profile.id,
            Name = profile.Name,
            level = profile.level,
            maxHealth = profile.maxHealth,
            maxSpeed = profile.maxSpeed,
            interactionRadius = profile.interactionRadius,
            maxLookSpeed = profile.maxLookSpeed
        };
    }
}
