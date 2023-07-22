using System;
using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float _cameraRotation;

        [SerializeField] 
        private float _speed;

        [SerializeField] 
        private float _interactionDistance;

        public float Speed;
        public GameObject HeldObject;
        
        private Camera _cam;
        private Rigidbody _rb;
        private Animator _anim;

        private float _horizontal;
        private float _vertical;
        private Vector3 _moveDirection;
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        
        public static Player Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _cam = Camera.main;
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            GetInput();
            Rotate();
            Animate();
            //GetInteractions();
            DropHeldObject();
            
            Speed = _rb.velocity.magnitude;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Rotate()
        {
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");
        
            transform.Rotate(Vector3.up, mouseX * _cameraRotation * Time.deltaTime, Space.World);

            if (_cam.transform.localRotation.x <= -0.5f && mouseY > 0f)
                return;
            
            if (_cam.transform.localRotation.x >= 0.63f && mouseY < 0f)
                return;
                
            _cam.transform.Rotate(new Vector3(-1f, 0, 0), mouseY * _cameraRotation * Time.deltaTime, Space.Self);
        }

        private void GetInput()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
        }

        private void GetInteractions()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;

            var ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));

            if (!Physics.Raycast(ray, out var hit, _interactionDistance)) return;

            var interactable = hit.collider.GetComponent<IInteractable>();
            interactable?.Interact(gameObject);
            Debug.Log($"Held object: {HeldObject.gameObject.name}");
            Debug.Log($"Player name: {gameObject.name}");
        }
        
        private void DropHeldObject()
        {
            if (!Input.GetKeyDown(KeyCode.Q)) return;
            
            if (HeldObject == null) return;
            
            Instantiate(HeldObject, transform.position + transform.forward, Quaternion.identity);
            HeldObject = null;
        }
        
        public void PickUp(GameObject gameObject)
        {
            HeldObject = gameObject;
        }

        private void Move()
        {
            var tran = transform;
            
            _moveDirection = tran.forward * _vertical + tran.right * _horizontal;

            _rb.AddForce(_moveDirection.normalized * (_speed * 10f), ForceMode.Force);
        }
        
        private void Animate()
        {
            _anim.SetBool(IsRunning, _rb.velocity.magnitude > 0.1f);
        }
    }
}
