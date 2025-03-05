using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerControllerAlt : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 9f;

    [SerializeField] 
    private GameObject bulletPrefab;
    [SerializeField] 
    private Transform barrelTransform;
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private float bulletHitMissDistance = 25f;
    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;
    
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => ShootGun();
    }

    private void ShootGun()
    {
        RaycastHit hit;
        Vector3 targetPoint;
        bool hasHit = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 100f); 

        if (hasHit)
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
        }

        GameObject bullet = Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.target = targetPoint;
        bulletController.hit = hasHit;
    }


    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>(); 
        Vector3 move = input.x * cameraTransform.right + input.y * cameraTransform.forward;
        move.y = 0f;
        controller.Move(move.normalized * Time.deltaTime * playerSpeed);
        
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}