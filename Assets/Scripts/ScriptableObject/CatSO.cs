using UnityEngine;

namespace Catris 
{
    [CreateAssetMenu(fileName = "Cat", menuName = "Cat")]
    public class CatSO : ScriptableObject
    {
        public byte number;
        public Color color;
    }
}
