using System;
using UnityEngine;

namespace _Scripts
{
    public class Apple : MonoBehaviour, IPickable
    {
        private Rigidbody _rb;
        private Transform _pickUpPoint;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Pick(Transform pickUpPoint)
        {
            _pickUpPoint = pickUpPoint;
            _rb.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        public void Drop()
        {
            _pickUpPoint = null;
            _rb.useGravity = true;
        }

        private void FixedUpdate()
        {
            if (_pickUpPoint == null) return;

            _rb.useGravity = false;
            var newPos = Vector3.Lerp(transform.position, _pickUpPoint.position, Time.deltaTime * 10f);
            _rb.MovePosition(newPos);
        }
    }
}