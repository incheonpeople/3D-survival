using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;// ĳ������ �̵� �ӵ��� �����ϴ� �����Դϴ�.
    private Vector2 curMovementInput; // Ű���忡�� ���� ���� �̵� �Է��� �����ϴ� 2D �����Դϴ�. x���� �¿� �̵�, y���� ���� �̵��� ��Ÿ���ϴ�.
    public float jumptForce; // ĳ���Ͱ� ������ �� ����� ���� ũ���Դϴ�.
    public LayerMask groundLayerMask; // ĳ���Ͱ� �� ���� �� �ִ� �ٴ�(ground)�� �����ϴ� �� ���� ���̾� ����ũ�Դϴ�.

    [Header("Look")]
    public Transform cameraContainer; // ī�޶� �����ϴ� ������Ʈ�� Transform ������Ʈ��, ī�޶��� ���� ȸ���� ó���ϱ� ���� ���˴ϴ�.
    public float minXLook; // ī�޶��� ���� ȸ�� ���� ���Դϴ�. �ּҿ� �ִ� ������ ������ ī�޶� ���Ʒ��� �ʹ� ���� ȸ������ �ʵ��� �����մϴ�.
    public float maxXLook;
    private float camCurXRot; // ī�޶��� ���� X�� ȸ������ �����ϴ� �����Դϴ�.
    public float lookSensitivity; // ���콺 ����. ���콺 �̵��� ���� ī�޶� �󸶳� ������ ȸ�������� �����մϴ�.

    private Vector2 mouseDelta; // ���콺 �Է��� �����ϴ� 2D �����Դϴ�. ���콺�� ������ �� �߻��ϴ� x, y ���� �����մϴ�.

    [HideInInspector]
    public bool canLook = true; // �÷��̾ ī�޶� ȸ���� �� �� �ִ��� ���θ� �����ϴ� �Ҹ��� �����Դϴ�.

    private Rigidbody rigidbody; // ������ ����(�̵�, ���� ��)�� ó���ϴ� Rigidbody ������Ʈ�� �����ϴ� �����Դϴ�.

    private void Awake() // Unity�� ���� �ֱ� �Լ� �� �ϳ���, ������Ʈ�� ó�� Ȱ��ȭ�� �� ȣ��˴ϴ�. �ַ� �ʱ�ȭ �۾��� �մϴ�.
    {
        rigidbody = GetComponent<Rigidbody>(); // �� ���� ���� ������Ʈ�� ����� Rigidbody ������Ʈ�� �����ͼ�, ���߿� ĳ���� �̵��̳� ������ ó���ϴ� �� ����մϴ�.
    }

    void Start() // ���� ���� �� �� �� ȣ��Ǵ� ���� �ֱ� �Լ��Դϴ�. ���⼭�� Ŀ�� ��� ���¸� �����մϴ�.
    {
        Cursor.lockState = CursorLockMode.Locked;
        // ���콺 Ŀ���� ȭ�� �߾ӿ� ������Ű�� Ŀ���� ������ �ʵ��� �����մϴ�. �̴� 1��Ī �Ǵ� 3��Ī ���ӿ��� ī�޶� ������ ���콺 ���������� ������ �� ���˴ϴ�.
    }

    private void FixedUpdate()
    // ���� ������ ó���� �� ȣ��Ǵ� �޼����, ������ �ð� �������� ȣ��˴ϴ�. �������� �̵�(Rigidbody�� �̿��� �̵�)�� ó���� �� �ַ� ���˴ϴ�.
    {
        Move();
        // �� �޼��忡���� Move() �Լ��� ȣ���Ͽ� ĳ������ �̵��� ó���մϴ�.
    }

    private void LateUpdate() // ��� Update�� �Ϸ�� �� ȣ��Ǵ� �޼����, ī�޶� ���� ó���� �� �� ���˴ϴ�.
    {
        if (canLook)
        // canLook�� true�� ���� ī�޶� ȸ���ϵ��� �����մϴ�. �̷ν� ���콺 �̵��� ���� ī�޶� ȸ���� �����մϴ�.
        {
            CameraLook();
            // ���콺 �������� ������� ī�޶� ������ �����ϴ� �Լ��Դϴ�.
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