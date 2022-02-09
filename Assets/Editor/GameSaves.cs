using UnityEngine;
using UnityEditor;
using System.IO;

public class GameSaves
{
    [MenuItem("GameSaves/Delete All Saves")]
    private static void DeleteSaves()
    {
#if UNITY_IOS || UNITY_ANDROID
        DeleteIOSSaves();
#else
        DeleteStandaloneSaves();
#endif
    }

    private static void DeleteIOSSaves()
    {
        string mobileSavePath = System.IO.Path.Combine(Application.persistentDataPath, "profile.gd");

        if (File.Exists(mobileSavePath))
        {
            File.Delete(mobileSavePath);
        }
    }

    private static void DeleteStandaloneSaves()
    {
        string standaloneSavePath = System.IO.Path.Combine(Application.dataPath, "profile.gd");

        if (File.Exists(standaloneSavePath))
        {
            File.Delete(standaloneSavePath);
        }
    }
}