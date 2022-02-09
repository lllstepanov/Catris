using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Handles saving
/// </summary>
public class ProfileManager : EternalSingleton<ProfileManager> 
{
	/// <summary>
	/// Profile Data variable - contains data that needs to be save
	/// </summary>
	private ProfileData profile = new ProfileData();

	/// <summary>
	/// Path to folder that cointains game data
	/// </summary>
	private string profilePath;

	private void Start(){
		///Create path
		MatchPathes();

		///Load game data
		Load ();
	}

	/// <summary>
	/// Creates path according to platform
	/// </summary>
	private void MatchPathes()
    {
#if UNITY_ANDROID 
		profilePath = Path.Combine(Application.persistentDataPath, "profile.gd");
#else
        profilePath = Path.Combine(Application.dataPath, "profile.gd");
#endif
    }

	/// <summary>
	/// Loads existing profile
	/// </summary>
	private void Load(){
		/// Check if game data exists
		if (File.Exists (profilePath)) {
			Debug.Log("Profile Exists");
			
			///Loading Game Data
			LoadProfileData ();
		} 
		else
		{
			Debug.Log("New Profile");
		}
	}

	/// <summary>
	/// When Application wraps - save game data
	/// </summary>
	private void OnApplicationFocus(bool pauseStatus){
		if (!pauseStatus) {
			///Save game data
			
			SaveProfileData();
		}
	}

	/// <summary>
	/// Saves game data
	/// </summary>
	public void SaveProfileData(){
        Debug.Log("<color=magenta>Save Profile</color>");

		BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(profilePath);

		///class serialization to .gd
        bf.Serialize(file, profile);
        file.Close();

        Debug.Log("<color=magenta>-------------Profile Saved-----------------</color>");
	}

	/// <summary>
	/// Loads game data
	/// </summary>
	public void LoadProfileData(){
		Debug.Log("<color=cyan>Load Profiles</color>");

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(profilePath, FileMode.Open);

		/// Deserialization from .gd to ProfileData class
        profile = (ProfileData)bf.Deserialize(file);

        file.Close();

        Debug.Log("<color=cyan>-------------Profiles Loaded-----------------</color>");
	}

	/// <summary>
	/// Override old profile with new one and saves
	/// </summary>
	public void ResetSaves() {
		profile = new ProfileData();
		SaveProfileData();
    }

	/// <summary>
	/// Saves recest best score to profile
	/// </summary>
	public void SetBestScore(int bestScore) 
	{
		profile.BestScore = bestScore;
	}

	/// <summary>
	/// Return best score
	/// </summary>
	public int GetBestScore() 
	{
		return profile.BestScore;
	}
}


[Serializable]
/// <summary>
/// Profile data class contains information of all data thats needs to be saved
/// </summary>
public class ProfileData {
	
	[HideInInspector]
	/// <summary>
	/// Override old profile with new one and saves
	/// </summary>
	public int BestScore { get; set; }
}
