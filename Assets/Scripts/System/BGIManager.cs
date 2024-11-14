using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGIManager : MonoBehaviour
{
    [SerializeField] private GameObject bigStars;
    [SerializeField] private GameObject smallStars;
    private Material bigMaterial;
    private Material smallMaterial;
    private float bRot;
    private float sRot;
    private float bOffX;
    private float bOffY;
    private float sOffX;
    private float sOffY;

    [SerializeField] private float bigRotationSpeed;
    [SerializeField] private float smallRotationSpeed;
    [SerializeField] private float bigOffsetSpeedX;
    [SerializeField] private float bigOffsetSpeedY;
    [SerializeField] private float smallOffsetSpeedX;
    [SerializeField] private float smallOffsetSpeedY;

    private void Start()
    {
        bigMaterial = bigStars.GetComponent<SpriteRenderer>().material;
        smallMaterial = smallStars.GetComponent<SpriteRenderer>().material;
        bRot = 0;
        sRot = 0;
        bOffX = 0;
        bOffY = 0;
        sOffX = 0;
        sOffY = 0;
    }

    private void Update()
    {
        bOffX += Time.deltaTime * bigOffsetSpeedX;
        bOffY += Time.deltaTime * bigOffsetSpeedY;
        sOffX += Time.deltaTime * smallOffsetSpeedX;
        sOffY += Time.deltaTime * smallOffsetSpeedY;
        bRot += Time.deltaTime * bigRotationSpeed;
        sRot += Time.deltaTime * smallRotationSpeed;
        Vector2 bOV = new(bOffX, bOffY);
        Vector2 sOV = new(sOffX, sOffY);
        bigMaterial.SetFloat("_Rotation",bRot);
        bigMaterial.SetVector("_Offset",bOV);
        smallMaterial.SetFloat("_Rotation", bRot);
        smallMaterial.SetVector("_Offset", sOV);
    }
}
