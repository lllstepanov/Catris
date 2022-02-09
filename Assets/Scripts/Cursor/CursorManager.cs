using UnityEngine;

namespace CursorManagment {

    /// <summary>
    /// Manages cursor icons
    /// </summary>
    public class CursorManager : MonoBehaviour
    {
        /// <summary>
        /// Idle cursor image.
        /// </summary>
        [SerializeField]
        private Texture2D idleCursor;

        /// <summary>
        /// Click cursor image.
        /// </summary>
        [SerializeField]
        private Texture2D clickCursor;

        /// <summary>
        /// Cursor Mode field.
        /// </summary>
        private CursorMode cursorMode = CursorMode.Auto;

        /// <summary>
        /// Cursor position fields.
        /// </summary>
        private Vector2 hotSpot = Vector2.zero;

        /// <summary>
        /// Loading completed delegate field. 
        /// </summary>		
        private delegate void ClickDelegate();

        /// <summary>
        /// Loading completed delegate object field. 
        /// </summary>
        private ClickDelegate clickDelegate;

        /// <summary>
        /// Time of reset when cursor backs to idle.
        /// </summary>
        private float resetTime = 0.02f;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);           
        }

        private void Start()
        {
            // Assigns methods to delegate on Start.
            AssingDelegateMethods();
        }

        /// <summary>
        /// // Assigns methods to delegate.
        /// </summary>
        private void AssingDelegateMethods() 
        {
            clickDelegate += ChangeCursorToClick;
            clickDelegate += WaitThenReset;
        }

        private void Update()
        {
            // Checks if any key pressed. 
            if (Input.anyKey)
            {
                // Enable the delegate.
                clickDelegate();
            }
        }

        /// <summary>
        /// Changes image of the cursor to click image.
        /// </summary>
        private void ChangeCursorToClick()
        {
            // Changes cursor image.
            Cursor.SetCursor(clickCursor, hotSpot, cursorMode);
        }

        /// <summary>
        /// Reset after time.
        /// </summary>
        private void WaitThenReset()
        {
            Invoke("ResetCursor", resetTime);            
        }

        /// <summary>
        /// Change image of the cursor to idle image.
        /// </summary>
        private void ResetCursor()
        {
            // Changes cursor image.
            Cursor.SetCursor(idleCursor, hotSpot, cursorMode);
        }
    }
}
