using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundForceEffect : MonoBehaviour
{
    public bool Toggle;
    public float ThrowForce;

    void OnCollisionEnter(Collision collision)
    {
        if (Toggle)
        {
            GameObject x = collision.collider.gameObject;
            //foreach (Transform x in collision.collider.transform)
            //{
            Health tempH = x.GetComponent<Health>();
            if (tempH != null)
            {
                tempH.isDestroyed = true;
            }

            AttachmentHandler tempAH = x.GetComponentInChildren<AttachmentHandler>();
            if (tempAH != null)
            {
                //tempAH.DropAllArmor();
                tempAH.KillAttachments();
            }
            //}

            ParticleSystem tempPs = collision.collider.gameObject.GetComponent<ParticleSystem>();
            Rigidbody tempRb = collision.collider.gameObject.GetComponent<Rigidbody>();

            //if (tempRb !=  null && tempPs == null)
            if (tempRb != null && tempPs == null)
            {
                tempRb.AddForce(this.transform.forward * -1 * ThrowForce);
                tempRb.AddTorque(this.transform.right * ThrowForce);
                tempRb.AddForce(this.transform.up * (ThrowForce / 4));
                //tempRb.AddForce(this.transform.up * ThrowForce);
            }
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (Toggle)
        {
            Rigidbody tempRb = collisionInfo.collider.gameObject.GetComponent<Rigidbody>();
            ParticleSystem tempPs = collisionInfo.collider.gameObject.GetComponent<ParticleSystem>();

            if (tempRb != null && tempPs == null)
            {
                tempRb.AddForce(this.transform.up * (ThrowForce / 4));
            }
        }
    }
}
