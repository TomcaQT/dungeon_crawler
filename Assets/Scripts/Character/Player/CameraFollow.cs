using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private float _size = 8f;
    [SerializeField] private Vector3 _offset;

    private Camera _camera;
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();
            OnValidate();
    }

    private void OnValidate()
    {
        if (_camera != null) 
            _camera.orthographicSize = _size;
    }
    
    private void FixedUpdate()
    {
        Vector3 desiredPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }
}
