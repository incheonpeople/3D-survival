using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform CameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private Rigidbody _rigidbody;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; // 셋팅된 값을

        _rigidbody.velocity = dir; // velocity에 넣어준다.
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       if(context.phase == InputActionPhase.Performed) // 키가 계속 눌린상태에도 변화할 수 있도록 Started말고 Performed를 넣어줌
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
       else if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        CameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); //값을 계속 받아오니깐 값만 불러오면됨
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Onjump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            { 
                return true;
            }
        }
        return false;
    }
}
