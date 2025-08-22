using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grab : MonoBehaviour
{
    private Rigidbody rb;
    private ConfigurableJoint joint;
    private Inputs currentHand;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            var input = other.GetComponentInParent<Inputs>();
            if (input)
                currentHand = input;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            var input = other.GetComponentInParent<Inputs>();
            if (input && input == currentHand)
                currentHand = null;
        }
    }

    void Update()
    {
        if (currentHand)
        {
            if (currentHand.Grip > 0.5f && joint == null)
            {
                joint = gameObject.AddComponent<ConfigurableJoint>();
                joint.connectedBody = currentHand.GetComponent<Rigidbody>();
                joint.xMotion = ConfigurableJointMotion.Locked;
                joint.yMotion = ConfigurableJointMotion.Locked;
                joint.zMotion = ConfigurableJointMotion.Locked;
                joint.angularXMotion = ConfigurableJointMotion.Locked;
                joint.angularYMotion = ConfigurableJointMotion.Locked;
                joint.angularZMotion = ConfigurableJointMotion.Locked;
            }
            if (currentHand.Grip < 0.2f && joint != null)
            {
                Destroy(joint);
                joint = null;
            }
        }
        else if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }
    }
}
