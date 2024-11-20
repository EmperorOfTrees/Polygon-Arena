using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        target = FindAnyObjectByType<CinemachineVirtualCamera>().transform;
        transform.position = target.position + offset;
    }
    private void Update()
    {
        transform.position = target.position+ offset;
    }
}
