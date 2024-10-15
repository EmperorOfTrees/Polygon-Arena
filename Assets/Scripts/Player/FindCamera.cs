using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindCamera : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private void Start()
    {
        vcam = FindAnyObjectByType<CinemachineVirtualCamera>();
        vcam.LookAt = gameObject.transform;
        vcam.Follow = gameObject.transform;
    }
}
