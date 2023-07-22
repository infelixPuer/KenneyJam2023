using UnityEngine;

namespace _Scripts
{
    public class CompatibilityReader : MonoBehaviour
    {
        public TextAsset compatibilityCSV;

        private void Start()
        {
            string[] lines = compatibilityCSV.text.Split('\n');
            
            int rowCount = lines.Length - 3;
            int colCount = lines[0].Split(';').Length - 2;

            int[,] compatibilityArray = new int[rowCount, colCount];

            for (int i = 2; i < lines.Length - 1; i++)
            {
                string line = lines[i];
                string[] values = line.Split(';');

                for (int j = 2; j <= colCount + 1; j++)
                {
                    compatibilityArray[i - 2, j - 2] = int.Parse(values[j]);
                }
            }
            
            CompatibilityManager.Instance.CompatibilityArray = compatibilityArray;
            
            // Just for testing 
            
            // for (int i = 0; i < rowCount; i++)
            // {
            //     var str = "";
            //     
            //     for (int j = 0; j < colCount; j++)
            //     {
            //         str += compatibilityArray[i, j] + " ";
            //     }
            //     
            //     Debug.Log(str);
            // }
        }
    }
}