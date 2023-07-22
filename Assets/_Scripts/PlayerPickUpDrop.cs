using System;
using UnityEngine;

namespace _Scripts
{
    public class PlayerPickUpDrop : MonoBehaviour
    {
        [SerializeField] 
        private Transform _pickUpPoint;
        
        private Camera _cam;
        private IPickable _heldObject;
        
        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_heldObject == null)
                {
                    var ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));

                    if (Physics.Raycast(ray, out var hit, 6f))
                    {
                        hit.collider.TryGetComponent(out _heldObject);

                        _heldObject?.Pick(_pickUpPoint);
                    }
                }
                else
                {
                    _heldObject.Drop();
                    _heldObject = null;
                }
            }
        }
    }
}