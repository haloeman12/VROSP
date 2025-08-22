using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody locomotionSphere;
    public Transform cameraTransform;
    public float moveForce = 10f;
    public float maxAngularVelocity = 10f;
    public InputActionReference moveAction;

    private Vector3 initialCamLocalPos;

    void Start()
    {
        locomotionSphere.maxAngularVelocity = maxAngularVelocity;
        initialCamLocalPos = cameraTransform.localPosition;
    }

    void OnEnable()
    {
        moveAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
    }

    void FixedUpdate()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) input.y += 1f;
            if (Keyboard.current.sKey.isPressed) input.y -= 1f;
            if (Keyboard.current.aKey.isPressed) input.x -= 1f;
            if (Keyboard.current.dKey.isPressed) input.x += 1f;
        }

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 direction = camForward * input.y + camRight * input.x;
        if (direction.sqrMagnitude > 1f) direction.Normalize();

        if (direction.sqrMagnitude > 0.01f)
        {
            locomotionSphere.AddTorque(transform.right * -direction.z * moveForce, ForceMode.Force);
            locomotionSphere.AddTorque(transform.forward * direction.x * moveForce, ForceMode.Force);
        }
        else
        {
            locomotionSphere.angularVelocity = Vector3.zero;
        }

        Vector3 camOffset = cameraTransform.localPosition - initialCamLocalPos;
        Vector3 horizontalOffset = new Vector3(camOffset.x, 0f, camOffset.z);

        if (horizontalOffset.sqrMagnitude > 0.0001f)
        {
            Vector3 localDelta = transform.InverseTransformDirection(horizontalOffset);
            locomotionSphere.AddTorque(transform.right * -localDelta.z * moveForce, ForceMode.Force);
            locomotionSphere.AddTorque(transform.forward * localDelta.x * moveForce, ForceMode.Force);

            cameraTransform.localPosition = new Vector3(
                initialCamLocalPos.x,
                cameraTransform.localPosition.y,
                initialCamLocalPos.z
            );
        }
    }
}
