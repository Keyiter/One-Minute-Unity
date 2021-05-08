using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{

    public AnimationCurve cameraShiftCurve;

    public delegate void DestinationReached();

    private Portal destinationPortal;
    private SimpleMovement player;

    public void EnterPortal(GameObject playerObject,Portal portal) {
        if (destinationPortal != null)
            return;
        destinationPortal = transform.GetChild(((portal.transform.GetSiblingIndex() + 1) % 2)).GetComponent<Portal>();
        player = playerObject.GetComponent<SimpleMovement>();
        player.FlyToPoint(portal.transform.position, CallPlayerInPortal);
    }

    public void CallPlayerInPortal() {
        Camera.main.GetComponent<CameraMovement>().MakeMovement(.5f, destinationPortal.transform.position, cameraShiftCurve);
        Invoke("TeleportPlayer",.4f);
    }

    private void TeleportPlayer() {
        player.transform.position = destinationPortal.transform.position;
        player.FlyToPoint(destinationPortal.portalExit.position, EnableControlls);
    }

    private void EnableControlls() {
        player.EnableControll();
        destinationPortal = null;
    }
}
