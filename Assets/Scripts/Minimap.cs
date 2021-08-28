using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public float angle;

    public Transform minimapOverlay;

    private Transform player;

    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + Vector3.up * 5f;
        HandleEnemyVisible();
        RotateOverlay();
    }

    private void HandleEnemyVisible() {
        for (int i = 0; i < enemies.Length; i++) {
            // if(Physics.raycast...
            enemies[i].SetActive(Vector3.Angle(player.forward, enemies[i].transform.position - player.position) <= angle);
        }
    }

    private void RotateOverlay() {
        minimapOverlay.localRotation = Quaternion.Euler(0, 0, -player.eulerAngles.y - angle);
    }
}
