using System.Linq;
using UnityEngine;

namespace _Scripts
{
    public class CompatibilityManager : MonoBehaviour
    {
        public GameObject[] Products;
        [HideInInspector] public int[,] CompatibilityArray;
        
        public static CompatibilityManager Instance;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public int GetProductIndex(GameObject product)
        {
            return Products
                .Where(x => x == product)
                .Select((x, i) => i)
                .First();
        }

        public int GetCompatibility(int index1, int index2) => CompatibilityArray[index1, index2];

    }
}