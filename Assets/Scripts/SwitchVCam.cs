using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField] 
    private int prioritytiBoostAmount = 10;
    
    [SerializeField] 
    private Canvas normalCanvas;
    
    [SerializeField] 
    private Canvas aimCanvas;

    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;

    private void Awake() {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
    }

    private void OnEnable() {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable() {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        virtualCamera.Priority += prioritytiBoostAmount;
        aimCanvas.enabled = true;
        normalCanvas.enabled = false;
    }
    
    private void CancelAim()
    {
        virtualCamera.Priority -= prioritytiBoostAmount;
        aimCanvas.enabled = false;
        normalCanvas.enabled = true;
    }
    
}