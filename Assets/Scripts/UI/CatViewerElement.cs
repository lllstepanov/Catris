using UnityEngine;
using UnityEngine.UI;
using Catris;

namespace CatrisUI 
{
    /// <summary>
    /// Represents cat from the cat queue 
    /// </summary>
    internal class CatViewerElement : MonoBehaviour
    {
        /// <summary>
        /// Number of the cat
        /// </summary>
        internal byte number;

        /// <summary>
        /// Sprite of the cat
        /// </summary>
        [SerializeField]
        private Image sprite;

        /// <summary>
        /// UIText of the cat 
        /// </summary>
        [SerializeField]
        private Text text;

        /// <summary>
        /// Assign new cat information to this object
        /// </summary>
        internal void SetUp(Cat cat)
        {
            /// Assign cats number 
            number = cat.number;
            
            /// Assing number to UIText 
            text.text = number.ToString();
            
            /// Set Color of the object
            sprite.color = cat.color;
        }
    }
}

