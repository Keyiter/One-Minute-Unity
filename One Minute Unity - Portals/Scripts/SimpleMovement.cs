using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{

    private bool isControlled;
    private Vector3 target;

    private void Start() {
        isControlled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isControlled)
            transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime;
    }

    public void FlyToPoint(Vector3 position, PortalController.DestinationReached functionToCall) {
        isControlled = false;
        target = position;
        StartCoroutine(FlyToPoint(functionToCall));
    }


    private IEnumerator FlyToPoint(PortalController.DestinationReached function) {
        while(Vector2.Distance(transform.position,target) > .1f) {
            transform.position += (target - transform.position).normalized * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        function.Invoke();
    }

    public void EnableControll() {
        isControlled = true;
    }
}
