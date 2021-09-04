using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed;

    [SerializeField] private Vector3 _offcet;

    private void Start()
    {
        _target = FindObjectOfType<Player>().transform;
    }

    private void FixedUpdate()
    {
        if(_target != null) transform.position = Vector3.Lerp(transform.position, _target.position + _offcet, Time.fixedDeltaTime * _followSpeed);
    }
}
