using UnityEngine;

/// <summary>
/// Destroys any other gameObject contains the same type
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Component
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
        }
        else
        {
            /// Destroys same type gameobject
            Destroy(gameObject);
        }
    }
}
