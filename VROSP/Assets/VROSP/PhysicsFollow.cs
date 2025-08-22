using UnityEngine;

public class PhysicsFollow : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public float positionStrength = 50f;
    public float rotationStrength = 50f;
    public Vector3 lockPositionAxes = Vector3.zero;
    public Vector3 lockRotationAxes = Vector3.zero;
    public bool isHead = false;
    public float headPosMultiplier = 3f;

    void FixedUpdate()
    {
        Vector3 posDelta = target.position - rb.position;
        posDelta = Vector3.Scale(posDelta, Vector3.one - lockPositionAxes);
        if (isHead)
            posDelta *= headPosMultiplier;

        Vector3 velTarget = posDelta / Time.fixedDeltaTime;
        Vector3 velDelta = velTarget - rb.linearVelocity;
        velDelta = Vector3.Scale(velDelta, Vector3.one - lockPositionAxes);
        rb.AddForce(velDelta * positionStrength, ForceMode.Acceleration);

        Quaternion rotDelta = target.rotation * Quaternion.Inverse(rb.rotation);
        rotDelta.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle > 180f) angle -= 360f;
        Vector3 angVelTarget = axis * angle * Mathf.Deg2Rad / Time.fixedDeltaTime;
        angVelTarget = Vector3.Scale(angVelTarget, Vector3.one - lockRotationAxes);
        Vector3 angVelDelta = angVelTarget - rb.angularVelocity;
        angVelDelta = Vector3.Scale(angVelDelta, Vector3.one - lockRotationAxes);
        rb.AddTorque(angVelDelta * rotationStrength, ForceMode.Acceleration);
    }
}
