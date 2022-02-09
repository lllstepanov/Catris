using UnityEngine;
using System.Collections.Generic;

namespace Catris 
{
    /// <summary>
    /// Manages columns of the game. 
    /// </summary>
    internal class CatField : Singleton<CatField>
    {
        /// <summary>
        /// Collection of columns
        /// </summary>
        [SerializeField]
        private List<CatColumn> columns = new List<CatColumn>();

        /// <summary>
        /// Delegate variable
        /// </summary>
        internal delegate void OnGameOverDelegate();

        /// <summary>
        /// Game over event
        /// </summary>
        internal event OnGameOverDelegate OnGameOver;

        private void Start()
        {
            /// Calls SetUp method 
            SetUp();
        }

        /// <summary>
        /// Assigns CheckGameOver method with OnColumnFull event of the column
        /// </summary>
        private void SetUp() 
        {
            /// Start of cycle
            for (int i = 0; i < columns.Count; i++)
            {
                ///Assing CheckGameOver method with OnColumnFull event by column index in the collection
                columns[i].OnColumnFull += CheckGameOver;
            }
        }

        /// <summary>
        /// Check if all columns are full
        /// </summary>
        internal void CheckGameOver() 
        {
            /// Number of full columns
            byte fullCounter = 0;

            /// Start of cycle
            for (int i = 0; i < columns.Count;i++) 
            {
                /// Check if column is full
                if (columns[i].isFull) {
                    /// Increase counter by 1
                    fullCounter++;
                }
            }

            /// Check if counter is equel to collection size 
            if (fullCounter == columns.Count) 
            {
                /// Enable GameOver
                GameOver();
            }
        }

        /// <summary>
        /// Removes all cats in the columns
        /// </summary>
        internal void ClearField() 
        {
            /// Start of cycle
            for (int i = 0; i < columns.Count; i++)
            {
                /// Clear the column
                columns[i].Clear();
            }
        }

        /// <summary>
        /// Start Game Over event
        /// </summary>
        internal void GameOver() 
        {
            /// Check if OnGameOver event contains subs than exicutes
            OnGameOver?.Invoke();
        }
    }
}
