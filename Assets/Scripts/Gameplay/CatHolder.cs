using UnityEngine;
using System.Collections.Generic;

namespace Catris
{
    /// <summary>
    /// CatHolder contains movement of the object in the column and representing visual information of the cat 
    /// </summary>
    internal class CatHolder : MonoBehaviour
    {
        /// <summary>
        /// Number value of the cat
        /// </summary>
        internal byte number;

        /// <summary>
        /// Cat's image 
        /// </summary>
        [SerializeField]
        private SpriteRenderer sprite;

        /// <summary>
        /// Visual representation of the number
        /// </summary>
        [SerializeField]
        private TextMesh textMesh;

        /// <summary>
        /// Moving speed 
        /// </summary>
        [SerializeField]
        private float speed;

        /// <summary>
        /// Current speed value
        /// </summary>
        private float speedValue;

        /// <summary>
        /// Current target to move to
        /// </summary>
        private Vector3 target;

        /// <summary>
        /// Collection of targets
        /// </summary>
        [SerializeField]
        private List<Vector3> targets = new List<Vector3>();

        /// <summary>
        /// Step of the movement
        /// </summary>
        private float step;

        /// <summary>
        /// Column of the object
        /// </summary>
        private CatColumn catColumn;

        /// <summary>
        /// Sets the information of the cat
        /// </summary>
        internal void SetUp(Cat cat) 
        {
            /// Assign cats number
            number = cat.number;
            
            /// Assign cats number into TextMesh
            textMesh.text = number.ToString();

            /// Set's the color of image
            sprite.color = cat.color;

        }

        /// <summary>
        /// Sets the information of the cat
        /// </summary>
        internal void SetUp(CatSO cat)
        {
            /// Assign cats number
            number = cat.number;

            /// Assign cats number into TextMesh
            textMesh.text = number.ToString();

            /// Set's the color of image
            sprite.color = cat.color;

        }

        private void Update()
        {
            ///Checks if there are no targets
            if (targets.Count<=0) return;
            
            ///Movement of the cat object
            Movement();   
        }

        /// <summary>
        /// Method that moves this object to current target
        /// </summary>
        private void Movement() 
        {
            /// Assign the first target in the collections to target 
            target = targets[0];

            /// Calc the step
            step = speedValue * Time.deltaTime;

            /// Makes a move to position by step
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            
            /// Check if current position of the object equels targets position
            if (transform.position == target)
            {
                /// Removes first target of the collection
                targets.RemoveAt(0);

                /// Check is collection is equels 0
                if (targets.Count == 0) 
                {
                    /// Check if the new merge is available
                    catColumn.CheckMerge();
                }
            }
        }

        /// <summary>
        /// Adds a new target to the collection of the targets.
        /// </summary>
        internal void SetTarget(Vector3 target) 
        {
            /// Sets the current speed value
            speedValue = speed;

            /// Adds to collection
            targets.Add(target);
        }

        /// <summary>
        /// Set the sorting layer of the cat object 
        /// </summary>
        internal void SetSortingLayer(int sortingLayerNumber) 
        {
            /// Sets the sorting layer number of sprite
            sprite.sortingOrder = sortingLayerNumber;

            /// Sets the text mesh number of the object +1 so it will be upper than sprite
            textMesh.GetComponent<TextSortingLayer>().orderInLayer = sortingLayerNumber + 1;
        }

        /// <summary>
        /// Sets column
        /// </summary>
        internal void SetColumn(CatColumn column)
        {
            /// Assign CatColumn object to local object
            catColumn = column;
        }

        /// <summary>
        /// Adds a targets for merge 
        /// </summary>
        internal void MoveToMerge(Vector3 target) {
            /// Changes the current speed value
            speedValue = speed / 3;

            /// Adds target of the object for merge
            targets.Add(target);

            /// Adds objects start position so it return to it after merge
            targets.Add(transform.position);
        }
    }
}
