using UnityEngine;

public class ExpDrop : CollectableDrops
{
    public int expAmount = 1;
    private void Start()
    {
        
    }
    public override void ApplyEffect(Transform target)
    {
        Debug.Log("Applying EXP Drop Effect");
        // Apply experience points to the player
        PlayerStatsHandler playerStats = target.GetComponent<PlayerStatsHandler>();
        if (playerStats != null)
        {
            playerStats.UpdateExpPoints(expAmount);
        }
    }

    public override void Interact(Transform target)
    {
        Collect(target);
    }

}
