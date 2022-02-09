using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Catris 
{
    /// <summary>
    /// Class that handles operations in the column
    /// </summary>
    internal class CatColumn : MonoBehaviour
    {
        /// <summary>
        /// Point where cat appears in the column
        /// </summary>
        [SerializeField]
        private GameObject spawningPoint;

        /// <summary>
        /// Point where cat object should go after appearing in the spawning point
        /// </summary>
        [SerializeField]
        private GameObject dropPoint;

        /// <summary>
        /// Variable that contains start position of the drop point before it change position
        /// </summary>
        private Vector3 dropPointStartPosition;

        /// <summary>
        /// Particles that appearing after two cats objects merges.
        /// </summary>
        [SerializeField]
        private ParticleSystem particles;

        /// <summary>
        /// Combo counter show number of merges
        /// </summary>
        [SerializeField]
        private ComboCounter comboCounter;

        /// <summary>
        /// Button of the column
        /// </summary>
        [SerializeField]
        private CatColumnButton button;

        /// <summary>
        /// Flag that show that merging is in progress
        /// </summary>
        private bool merging = false;

        /// <summary>
        /// Collection of cats objects in the column
        /// </summary>
        private List<CatHolder> cats = new List<CatHolder>();

        /// <summary>
        /// number of merges
        /// </summary>
        private byte merges = 0;

        /// <summary>
        /// Flag that shows that column is full
        /// </summary>
        internal bool isFull = false;

        /// <summary>
        /// Delegate variable
        /// </summary>
        internal delegate void OnColumnFullDelegate();

        /// <summary>
        /// Event that invokes when column is full of cats objects
        /// </summary>
        internal event OnColumnFullDelegate OnColumnFull;

        /// <summary>
        /// Cat object that will be added in the cats collection
        /// </summary>
        GameObject cat;

        private void Start()
        {
            /// Assign mouse click / touch with PlaceCat method
            button.OnMouseDownEvent += PlaceCat;

            /// Save start drop point position to variable
            dropPointStartPosition = dropPoint.transform.position;
        }

        /// <summary>
        /// Method that spawns and add cat object in the column
        /// </summary>
        private void PlaceCat() 
        {
            /// If column is not full
            if (isFull) return;
            /// If column is not in the process of merging 
            if (merging) return;

            /// Enables merging (even if merging is not posible right now)
            merging = true;

            /// Updates drop point position
            UpdateDropPoint();

            /// Cat object assing with new spawned cat
            cat = CatSpawner.Instance.SpawnCat();

            /// Assign cat with this gameObject
            cat.transform.parent = transform;

            /// Set cat's object start position to spawning point position
            cat.transform.position = spawningPoint.transform.position;

            /// Assing CatHolder class of new cat object to local variable
            CatHolder catHolder = cat.GetComponent<CatHolder>();
            
            /// Set cat's object target where it should go 
            catHolder.SetTarget(dropPoint.transform.position);

            /// Set sorting layer of the cat's object according to collection 
            catHolder.SetSortingLayer(cats.Count*(-1));

            /// Assign this cat column to catHolder object
            catHolder.SetColumn(this);

            /// Adds cat's object to collection of cats
            cats.Add(cat.GetComponent<CatHolder>());          
        }

        /// <summary>
        /// Check if the merge is posible and then merge to cat in one. 
        /// </summary>
        internal void CheckMerge() 
        {
            /// Checks if collection of cats contains more the one cat
            if (cats.Count > 1)
            {
                /// Checks if two last cats in collection have same number 
                if (cats[cats.Count-2].number == cats[cats.Count-1].number)
                {
                    /// Updates the score of the current match
                    UpdateScore(cats[cats.Count - 2].number);

                    /// Starts merge two cats into one
                    MergeCats();
                }
                else
                {
                    /// If two last cats don't share the same number merging set flag to false
                    merging = false;

                    /// Reset number of merges
                    merges = 0;

                    /// Check if column of cats reachs it's limit
                    CheckIfColumnIsFull();
                }
            }
            else 
            {
                /// If collection of cats do not contains more the one cat set merging flag to false
                merging = false;

                /// Reset number of merges
                merges = 0;
            }
        }

        /// <summary>
        /// Check if column of cats reachs it's limit
        /// </summary>
        private void CheckIfColumnIsFull() {
            
            ///Check if collection of cats is equel 10
            
            if (cats.Count == 10)
            {
                /// If collection of cats is equel 10 than set isFull flag to true
                isFull = true;
            
                /// Checks of there are sub of the event and than exicutes it
                OnColumnFull?.Invoke();
            }
        }

        /// <summary>
        /// Updates merges and score of the current merge
        /// </summary>
        private void UpdateScore(byte value) {
            merges++;
            ScoreCounter.Instance.CountScore(value,merges);
        }

        /// <summary>
        /// Merging method / Visual merge of two cats / Updates second cat / Deletes first cat / Shows particles and combo counter
        /// </summary>
        private void MergeCats() 
        {
            ///Starts merging process
            StartCoroutine(Merging());
        }

        /// <summary>
        /// Merging proccess
        /// </summary>
        IEnumerator Merging()
        {
            /// Command cat 2 move to the cat 1 to merge
            VisualMerge();

            /// Wait a bit then proseed
            yield return new WaitForSeconds(0.1f);
            
            /// Shows Combo Counter
            ShowComboCounter();

            /// Show particles 
            ShowParticles();

            /// Update second cat
            UpdateNewCat();

            /// Delete first cat
            DeleteCat(cats.Count - 1);
        }

        /// <summary>
        /// Sets position of the upper cat to target of the lower cat
        /// </summary>
        private void VisualMerge() 
        {
            /// Command cat 2 move to the cat 1 to merge
            cats[cats.Count - 2].MoveToMerge(cats[cats.Count - 1].transform.position);
        }

        /// <summary>
        /// Removes cat's object from the collection by index and destroys it 
        /// </summary>
        private void DeleteCat(int index) 
        {
            /// Destroys cat object 
            Destroy(cats[index].gameObject);

            /// Removes empty space in the collection
            cats.RemoveAt(index);
        }

        /// <summary>
        /// Update second cat with a new number 
        /// </summary>
        private void UpdateNewCat() 
        {
            /// Increse number by 1
            cats[cats.Count - 2].number++;

            /// Checks if current number is higher than 10
            if (cats[cats.Count - 2].number>10)
            {
                /// Delete this cat
                DeleteCat(cats.Count - 2);

                /// Check if the is a new merge
                CheckMerge();
            } 
            else 
            {
                /// Set Up cat's object by new number
                cats[cats.Count - 2].SetUp(CatSpawner.Instance.GetCatByIndex(cats[cats.Count - 2].number));
            }
        }

        /// <summary>
        /// Shows combo counter
        /// </summary>
        private void ShowComboCounter() 
        {
            /// If merges if more that 1 that combo counter appears
            if (merges>1) {
                /// Enables combo counter gameObject
                comboCounter.gameObject.SetActive(true);

                /// Show number of merges in the combo counter object
                comboCounter.Show(merges.ToString(), cats[cats.Count - 1].transform.position);
            }
        }

        /// <summary>
        /// Shows particles
        /// </summary>
        private void ShowParticles()
        {
            /// Enables particles gameObject
            particles.gameObject.SetActive(true);

            /// Starts particles 
            particles.Play();

            /// Sets position of particles object 
            particles.transform.position = cats[cats.Count - 1].transform.position;
        }

        /// <summary>
        /// Update position of drop point
        /// </summary>
        private void UpdateDropPoint() 
        {
            /// Calculates new y position of drop point object
            float y = dropPointStartPosition.y + cats.Count * 0.365f;

            /// Sets new drop point position
            dropPoint.transform.position = new Vector3(dropPoint.transform.position.x, y, dropPoint.transform.position.z);
        }

        /// <summary>
        /// Remove all cat's obejcts from the collection
        /// </summary>
        internal void Clear() 
        {
            /// Start of the cycle 
            for (int i = 0; i < cats.Count; i++) 
            {
                /// Destroy cat's object by current cycle index
                Destroy(cats[i].gameObject);
            }

            /// Sets isFull flag ot false
            isFull = false;

            /// Clears collection from all ghost objects
            cats.Clear();
        }
    }
}

