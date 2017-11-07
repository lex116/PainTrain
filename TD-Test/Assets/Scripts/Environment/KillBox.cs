using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //foreach (Transform x in collision.collider.transform)
        //{
        GameObject x = collision.collider.gameObject;

            Health tempH = x.GetComponent<Health>();
            if (tempH != null)
            {
                tempH.isDestroyed = true;
            }

            AttachmentHandler tempAH = x.gameObject.GetComponentInChildren<AttachmentHandler>();
            if (tempAH != null)
            {
                //tempAH.DropAllArmor();
                tempAH.KillAttachments();
            }

            Destroy(x.gameObject);
        //}
    }
}
