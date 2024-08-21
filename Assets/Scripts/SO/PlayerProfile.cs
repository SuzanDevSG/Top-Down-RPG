using UnityEngine;

[CreateAssetMenu(menuName = "Character/Profile")]
public class PlayerProfile : ScriptableObject
{
    public int id;
    public string playerName;
    public float maxHealth;
    public float maxSpeed;
    public float maxLookSpeed;
}

