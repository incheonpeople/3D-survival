using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;// 캐릭터의 이동 속도를 조정하는 변수입니다.
    private Vector2 curMovementInput; // 키보드에서 받은 현재 이동 입력을 저장하는 2D 벡터입니다. x축은 좌우 이동, y축은 전후 이동을 나타냅니다.
    public float jumptForce; // 캐릭터가 점프할 때 사용할 힘의 크기입니다.
    public LayerMask groundLayerMask; // 캐릭터가 서 있을 수 있는 바닥(ground)을 감지하는 데 사용될 레이어 마스크입니다.

    [Header("Look")]
    public Transform cameraContainer; // 카메라를 포함하는 오브젝트의 Transform 컴포넌트로, 카메라의 상하 회전을 처리하기 위해 사용됩니다.
    public float minXLook; // 카메라의 상하 회전 제한 값입니다. 최소와 최대 각도를 지정해 카메라가 위아래로 너무 많이 회전하지 않도록 제한합니다.
    public float maxXLook;
    private float camCurXRot; // 카메라의 현재 X축 회전값을 저장하는 변수입니다.
    public float lookSensitivity; // 마우스 감도. 마우스 이동에 따라 카메라가 얼마나 빠르게 회전할지를 결정합니다.

    private Vector2 mouseDelta; // 마우스 입력을 저장하는 2D 벡터입니다. 마우스가 움직일 때 발생하는 x, y 값을 저장합니다.

    [HideInInspector]
    public bool canLook = true; // 플레이어가 카메라 회전을 할 수 있는지 여부를 제어하는 불리언 변수입니다.

    private Rigidbody rigidbody; // 물리적 동작(이동, 점프 등)을 처리하는 Rigidbody 컴포넌트를 참조하는 변수입니다.

    private void Awake() // Unity의 생명 주기 함수 중 하나로, 오브젝트가 처음 활성화될 때 호출됩니다. 주로 초기화 작업을 합니다.
    {
        rigidbody = GetComponent<Rigidbody>(); // 이 줄은 현재 오브젝트에 연결된 Rigidbody 컴포넌트를 가져와서, 나중에 캐릭터 이동이나 점프를 처리하는 데 사용합니다.
    }

    void Start() // 게임 시작 시 한 번 호출되는 생명 주기 함수입니다. 여기서는 커서 잠금 상태를 설정합니다.
    {
        Cursor.lockState = CursorLockMode.Locked;
        // 마우스 커서를 화면 중앙에 고정시키고 커서가 보이지 않도록 설정합니다. 이는 1인칭 또는 3인칭 게임에서 카메라 시점을 마우스 움직임으로 제어할 때 사용됩니다.
    }

    private void FixedUpdate()
    // 물리 연산을 처리할 때 호출되는 메서드로, 고정된 시간 간격으로 호출됩니다. 물리적인 이동(Rigidbody를 이용한 이동)을 처리할 때 주로 사용됩니다.
    {
        Move();
        // 이 메서드에서는 Move() 함수를 호출하여 캐릭터의 이동을 처리합니다.
    }

    private void LateUpdate() // 모든 Update가 완료된 후 호출되는 메서드로, 카메라 시점 처리를 할 때 사용됩니다.
    {
        if (canLook)
        // canLook이 true일 때만 카메라가 회전하도록 설정합니다. 이로써 마우스 이동을 통해 카메라 회전을 제어합니다.
        {
            CameraLook();
            // 마우스 움직임을 기반으로 카메라 시점을 변경하는 함수입니다.
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rigidbody.AddForce(Vector2.up * jumptForce, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}