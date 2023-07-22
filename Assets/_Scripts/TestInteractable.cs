using System;
using UnityEngine;

namespace _Scripts
{
    public class TestInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Material _highlightMaterial;
        
        [SerializeField]
        private Renderer _renderer;

        private Material[] _defaultMaterials;
        private bool _isHighlighted;

        private void Awake()
        {
            _defaultMaterials = _renderer?.sharedMaterials;
        }

        public void Interact()
        {
            Debug.Log("Interacting with " + gameObject.name);
            if (!_isHighlighted)
            {
                var materialCount = _renderer.sharedMaterials.Length;
                var materials = new Material[materialCount + 1];

                for (var i = 0; i < materials.Length; i++)
                {
                    materials[i] = _highlightMaterial;
                }
                
                _renderer.sharedMaterials = materials;

                _isHighlighted = true;
            }
            else
            {
                _renderer.sharedMaterials = _defaultMaterials;
                _isHighlighted = false;
            }
        }
    }
}