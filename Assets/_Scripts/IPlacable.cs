using UnityEngine;

namespace _Scripts
{
    public interface IPlacable
    {
        public void Place(GameObject gameObject);
        public void Take();
    }
}