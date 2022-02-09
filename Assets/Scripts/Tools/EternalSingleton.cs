using UnityEngine;

/// <summary>
/// Destroys any other gameObject contains the same type. Block gameObject from destroying in scene transition
/// </summary>
public class EternalSingleton<T> : MonoBehaviour where T : Component
{
    /// <summary>
    /// Instance of the object
    /// </summary>
    public static T Instance;

    public virtual void Awake()
    {
        /// Check if Instance is already taken
        if (Instance == null)
        {
            /// Sets the Instance with wanted type
            Instance = this as T;
            
            /// GameObject will not destroy on scene transitions
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            /// Destroys same type gameobject
            Destroy(gameObject);
        }
    }
}