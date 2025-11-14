using UnityEngine;
public enum DataType
{
    PlayerData,
    WeaponData,
    EnemyData,
    LevelData,
    CurrencyData,
}

public static class DataManager
{
    public static void SaveData<T>(T data, DataType dataType)
    {
        string saveData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(dataType.ToString(), saveData);
        Debug.Log(saveData);
    }
    public static T LoadData<T>(DataType dataType)
    {
        if (!PlayerPrefs.HasKey(dataType.ToString())) return default;
        string loadData = PlayerPrefs.GetString(dataType.ToString());
        return JsonUtility.FromJson<T>(loadData);
    }
    public static bool DeleteData(DataType dataType)
    {
        if (!PlayerPrefs.HasKey(dataType.ToString())) return false;
        PlayerPrefs.DeleteKey(dataType.ToString());
        return true;
    }
    public static bool ExistData(DataType dataType)
    {
        return PlayerPrefs.HasKey(dataType.ToString());
    }
}
