using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private float bobSpeed = 3f;
    [SerializeField] private float bobAmount = 0.02f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float aimSmoothing = 5f;

    private Vector3 initialLocalPosition;

    void Start()
    {
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (cameraTransform == null) return;
        
        float bobOffset = Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        transform.localPosition = initialLocalPosition + new Vector3(0, bobOffset, 0);
        
        Quaternion targetRotation = Quaternion.Euler(cameraTransform.eulerAngles.x, transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, aimSmoothing * Time.deltaTime);
    }
}

