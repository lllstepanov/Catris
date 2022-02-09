using UnityEngine;
using CatrisUI;

namespace Catris
{
    /// <summary>
    /// Class that handles detection of touch on the column
    /// </summary>
    internal class CatColumnButton : MonoBehaviour
    {
        /// <summary>
        /// Delegate variable
        /// </summary>
        internal delegate void OnMouseDownDelegate();

        /// <summary>
        /// On touch event variable
        /// </summary>
        internal event OnMouseDownDelegate OnMouseDownEvent;

        /// <summary>
        /// Active status of the button
        /// </summary>
        private bool active = true;

        private void Start()
        {
            /// Assign SetActive method to OnPauseEvent of the PausePanel class event
            PausePanel.Instance.OnPauseEvent += SetActive;            
        }

        /// <summary>
        /// Switch active flag
        /// </summary>
        private void SetActive() 
        {
            /// Switch active flag
            active = !active;
        }

        /// <summary>
        /// Detects mouse click or touch 
        /// </summary>
        private void OnMouseDown()
        {
            /// Checks if button is not active
            if (!active) return;

            /// Checks if there are subs of the event and the exicutes it
            OnMouseDownEvent?.Invoke();
        }
    }
}
