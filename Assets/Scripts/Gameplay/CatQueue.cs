using UnityEngine;
using System.Collections.Generic;

namespace Catris 
{
    /// <summary>
    /// Operates collection of numbers for cats objects 
    /// </summary>
    internal class CatQueue : EternalSingleton<CatQueue>
    {
        /// <summary>
        /// Collection of random numbers
        /// </summary>
        private List<byte> catQueue = new List<byte>();

        /// <summary>
        /// Size of the collection
        /// </summary>
        private byte listSize = 100;

        private void Start()
        {
            ///Fills the collection
            GenerateQueue();
        }

        /// <summary>
        /// Fills the collection with random numbers 
        /// </summary>
        internal void GenerateQueue() 
        {
            byte randNum;

            ///Starts cycle 
            for (int i = 0; i <= listSize; i++)
            {
                ///Sets random number from 1 to 5
                randNum = (byte)Random.Range(1, 6);
                
                ///Adds number to queue
                catQueue.Add(randNum);
            }
        }

        /// <summary>
        /// Remove number from the collection after use
        /// </summary>
        internal void RemoveItemFromQueue()
        {
            ///Remove number in the head of collection 
            catQueue.RemoveAt(0);

            ///Checks if the collection need to fill again
            CheckQueue();
        }

        /// <summary>
        /// Checks if the collection need to fill again
        /// </summary>
        private void CheckQueue()
        {
            ///if queue size is became smaller that 10 - generate new numbers
            if (catQueue.Count < 10) GenerateQueue();  
        }

        /// <summary>
        /// Return a number in the head of collection 
        /// </summary>
        internal byte GetFirstCatNumber() {
            return catQueue[0];
        }

        /// <summary>
        /// Return a number from collection by it's index
        /// </summary>
        internal byte GetCurrentCatNumberByIndex(int index)
        {
            return catQueue[index];
        }

        /// <summary>
        /// Clears whole collection
        /// </summary>
        internal void ClearQueue() 
        {
            catQueue.Clear();
        }
    }
}

