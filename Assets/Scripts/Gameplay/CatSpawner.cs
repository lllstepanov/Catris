using UnityEngine;
using System.Collections.Generic;

namespace Catris
{
    /// <summary>
    /// CatSpawner is responsible for spawning cats
    /// </summary>
    internal class CatSpawner : Singleton<CatSpawner>
    {
        [SerializeField]
        private bool useScriptableObjects = false;

        /// <summary>
        /// Prefab for instantiation
        /// </summary>
        [SerializeField]
        private GameObject catPrefab;

        /// <summary>
        /// Collection of possible cats
        /// </summary>
        [SerializeField]
        private List<CatSO> catsSO = new List<CatSO>();

        /// <summary>
        /// Collection of possible cats
        /// </summary>
        [SerializeField]
        private List<Cat> cats = new List<Cat>();

        /// <summary>
        /// Delegate variable
        /// </summary>
        internal delegate void OnCatSpawnDelegate();
        
        /// <summary>
        /// Spawn event
        /// </summary>
        internal event OnCatSpawnDelegate OnCatSpawn;

        private void Start()
        {
            /// Creates list of the cats 
            CreatCatList();    
        }

        /// <summary>
        /// Create list of the possible cats 
        /// </summary>
        private void CreatCatList() 
        {
            /// Cats parser manages the xml cutting
            CatParser catParser = new CatParser();
            
            /// Assign list of the possible cats created by CatParser
            cats = catParser.ParseCats();
        }

        /// <summary>
        /// Returns cat class object by index in the collection
        /// </summary>
        internal Cat GetCatByIndex(int index) 
        {
            /// return cat objects in the collection 
            return cats[index - 1];
        }

        /// <summary>
        /// Returns cat (scriptable object) class object by index in the collection
        /// </summary>
        internal CatSO GetCatSOByIndex(int index)
        {
            /// return cat objects in the collection 
            return catsSO[index - 1];
        }


        /// <summary>
        /// Creates a new CatHolder object
        /// </summary>
        internal GameObject SpawnCat() 
        {
            /// Creates a new cats object
            GameObject newCat = Instantiate(catPrefab);

            /// Check which set up needs to use
            if (useScriptableObjects)
            {
                /// Use scriptable objects
                SetUpCatSO(newCat);
            }
            else 
            {
                /// Use Cat class
                SetUpCat(newCat);
            }

            /// Removes first number in the CatQueue
            CatQueue.Instance.RemoveItemFromQueue();

            /// Check if the OnCatSpawn event has any sub and than exicutes
            OnCatSpawn?.Invoke();

            /// Returns new CatHolder object
            return newCat;
        }

        /// <summary>
        /// SetUp CatHolder with Cat class 
        /// </summary>
        private void SetUpCat(GameObject newCat) 
        {
            /// Assign the first number from the Cats Queue to new variable 
            byte number = CatQueue.Instance.GetFirstCatNumber();
            
            /// Assign Cat class from the collection of cats to the new variable by it's number
            Cat cat = GetCatByIndex(number);

            /// Set new Cat class in the CatHolder object  
            newCat.GetComponent<CatHolder>().SetUp(cat);
        }

        /// <summary>
        /// SetUp CatHolder with CatSO class (ScriptableObject)
        /// </summary>
        private void SetUpCatSO(GameObject newCat)
        {
            /// Assign the first number from the Cats Queue to new variable 
            byte number = CatQueue.Instance.GetFirstCatNumber();

            /// Assign Cat class from the collection of cats to the new variable by it's number
            CatSO cat = GetCatSOByIndex(number);

            /// Set new Cat class in the CatHolder object  
            newCat.GetComponent<CatHolder>().SetUp(cat);
        }
    }
}


