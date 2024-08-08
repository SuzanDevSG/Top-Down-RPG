using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private UIProfile[] uIProfiles;
    private Dictionary<int, UIProfile> _profiles;

    private void Start()
    {
        for (int i = 0; i < uIProfiles.Length; i++)
        {

            _profiles.Add(uIProfiles[i].id, uIProfiles[i]);
        }
    }
}
