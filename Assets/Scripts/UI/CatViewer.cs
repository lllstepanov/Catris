using UnityEngine;
using Catris;

namespace CatrisUI 
{
    /// <summary>
    /// Shows next CatHolder objects that will created next 
    /// </summary>
    internal class CatViewer : Singleton<CatViewer>
    {
        /// <summary>
        /// Animator variable
        /// </summary>
        private Animator anim;

        /// <summary>
        /// First cat
        /// </summary>
        [SerializeField]
        private CatViewerElement cat1;

        /// <summary>
        /// Second cat
        /// </summary>
        [SerializeField]
        private CatViewerElement cat2;

        private void Start()
        {
            /// Assign Animator of the object to local variable 
            anim = GetComponent<Animator>();

            /// Assing UpdateCats to CatSpawner event
            CatSpawner.Instance.OnCatSpawn += UpdateCats;

            /// Set up first two cats
            SetUp();
        }

        /// <summary>
        /// Set up first two cats
        /// </summary>
        internal void SetUp()
        {
            /// Set up first cat
            SetUp(cat1, 0);

            /// Set up second cat
            SetUp(cat2, 1);
        }

        /// <summary>
        /// Set up CatViewerElement 
        /// </summary>
        private void SetUp(CatViewerElement catElement, int order)
        {
            /// Get number from the queue
            byte number = CatQueue.Instance.GetCurrentCatNumberByIndex(order);
            
            /// Assign Cat class object to local variable
            Cat cat = CatSpawner.Instance.GetCatByIndex(number);

            /// Set up Cat Viewer Element using new Cat class  
            catElement.SetUp(cat);
        }

        /// <summary>
        /// Use Animator to move cats
        /// </summary>
        internal void UpdateCats()
        {
            /// Animator trigger action 
            anim.SetTrigger("Next");
        }

        /// <summary>
        /// Update first cat with new cat
        /// </summary>
        internal void UpdateFirstCat() 
        {
            /// Set up first cat
            SetUp(cat1,1);
            
            /// Set position in the hierarchy
            cat1.transform.SetAsFirstSibling();
        }

        /// <summary>
        /// Update second cat with new cat
        /// </summary>
        internal void UpdateSecondCat()
        {
            /// Set up second cat
            SetUp(cat2,1);

            /// Set position in the hierarchy
            cat2.transform.SetAsFirstSibling();
        }
    }
}
