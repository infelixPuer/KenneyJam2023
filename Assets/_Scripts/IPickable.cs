using UnityEngine;

namespace _Scripts
{
    public interface IPickable
    {
        public void Pick(Transform transform);
        public void Drop();
    }
}