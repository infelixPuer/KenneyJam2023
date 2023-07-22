using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float Speed;
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rb.AddForce(Vector3.forward * 10f);
        
        Speed = _rb.velocity.magnitude;
    }
}
