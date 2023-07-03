using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    [SerializeField] private LayerMask pickupMask;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Transform pickupTarget;
    [SerializeField] private float pickupRange;
    [SerializeField] private AudioSource pickupSfx;

    private Rigidbody currentObject;
    private Vector3 currentVelocity;
    private float smoothingFactor = 0.2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObject)
            {
                currentObject.useGravity = true;
                currentObject = null;
            }
            else
            {
                Ray cameraRay = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, pickupRange, pickupMask))
                {
                    currentObject = hitInfo.rigidbody;
                    currentObject.useGravity = false;

                    if (pickupSfx != null)
                    {
                        pickupSfx.Play();
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentObject)
        {
            Vector3 directionToPoint = pickupTarget.position - currentObject.position;
            float distanceToPoint = directionToPoint.magnitude;

            Vector3 targetVelocity = directionToPoint.normalized * 12f * distanceToPoint;
            currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, smoothingFactor);

            currentObject.velocity = currentVelocity;
        }
    }
}
