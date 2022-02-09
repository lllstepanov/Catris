using UnityEngine.UI;
using UnityEngine;

namespace CatrisLoading 
{
    /// <summary>
    /// 
    /// </summary>
    internal class LoadingUI : MonoBehaviour
    {
        /// <summary>
        /// Game logo button
        /// </summary>
        [SerializeField]
        private Button catrisLogo;

        /// <summary>
        /// Appsulove logo button 
        /// </summary>
        [SerializeField]
        private Button appsuloveLogo;

        [SerializeField]
        /// <summary>
        /// Link to the website
        /// </summary>
        private string link;    ///https://apps.apple.com/US/app/id1481982479?mt=8

        private void Start()
        {
            /// Assign OpenGameLink method to Game Logo button click
            catrisLogo.onClick.AddListener(() => OpenGameLink());

            /// Assign OpenGameLink method to Open Game Link click
            appsuloveLogo.onClick.AddListener(() => OpenGameLink());
        }

        /// <summary>
        /// Opens link
        /// </summary>
        private void OpenGameLink() 
        {
            /// Opens link
            Application.OpenURL(link);
        }
    }
}
