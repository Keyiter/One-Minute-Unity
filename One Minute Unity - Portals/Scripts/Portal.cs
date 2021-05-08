using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PortalController portalController;
    public Transform portalExit;

    void Start()
    {
        portalController = GetComponentInParent<PortalController>();
        portalExit = transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        portalController.EnterPortal(collision.gameObject, this);
    }
}
