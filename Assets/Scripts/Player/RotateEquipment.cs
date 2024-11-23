using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RotateEquipment : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePos;
    private Vector3 relativePos;
    [SerializeField] private float rotationSpeed = 0.03f;
    private Quaternion targetRot;
    private Quaternion currentRot;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentRot = transform.rotation;

        FollowPointPosition();
    }

    private void FollowPointPosition()
    {
        relativePos = mousePos - transform.position;
        relativePos = relativePos.normalized*5;

        float rotZ = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg -90;
        targetRot = Quaternion.Euler(0, 0, rotZ);
        transform.rotation = Quaternion.Lerp(currentRot, targetRot,rotationSpeed);
    }
}
