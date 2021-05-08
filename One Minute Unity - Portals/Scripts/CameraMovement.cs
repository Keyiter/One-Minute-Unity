using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player;
    private bool isFollowing;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        isFollowing = true;
    }

    void LateUpdate()
    {
        if(isFollowing)
            transform.position = new Vector3(player.position.x, player.position.y, -10);
    }

    public void MakeMovement(float time, Vector3 position, AnimationCurve cameraShiftCurve) {
        isFollowing = false;
        StartCoroutine(MoveCamera(time, new Vector3(position.x, position.y, transform.position.z), cameraShiftCurve));
    }

    private IEnumerator MoveCamera(float time,Vector3 position,AnimationCurve cameraShiftCurve) {
        Vector3 startingPosition = transform.position;
        for (float i = 0;  i < time; i+=.1f) {
            transform.position = Vector3.Lerp(startingPosition, position, cameraShiftCurve.Evaluate(i / time));
            yield return new WaitForSeconds(.1f);
        }
        isFollowing = true;
    }
}
