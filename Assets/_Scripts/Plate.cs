using System;
using UnityEngine;

namespace _Scripts
{
    public class Plate : MonoBehaviour
    {
        [SerializeField]
        private Transform _placePoint;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Apple apple))
            {
                var rb = apple.GetComponent<Rigidbody>();
                
                apple.Drop();
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                apple.transform.rotation = Quaternion.identity;
                apple.transform.position = _placePoint.position;
            }
        }
    }
}