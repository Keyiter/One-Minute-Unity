using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public float maxLength;
    public float fallSpeed;
    public LineRenderer lineRenderer;
    public LayerMask obstacleLayerMask;
    public BoxCollider2D boxCollider2D;
    public GameObject splashEffectObject;

    private float lastLength;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = 2;
        for (int i = 0; i < 2; i++) {
            lineRenderer.SetPosition(i, transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentLength(out float currentMaxLength);
        CalculateActualLength(currentMaxLength, out float currentLength);
        ResizeLine(currentLength);
        RescaleCollider(currentLength);
        CheckForSplashEffect(currentLength, currentMaxLength);
    }

    private void GetCurrentLength(out float currentMaxLength) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, maxLength, obstacleLayerMask);
        if (hit)
            currentMaxLength = Vector3.Distance(transform.position, hit.point);
        else
            currentMaxLength = maxLength;
    }

    private void CalculateActualLength(float currentMaxLength,out float currentLength) {
        currentLength = lastLength + Time.deltaTime * fallSpeed;
        currentLength = Mathf.Clamp(currentLength, 0, currentMaxLength);
        lastLength = currentLength;
    }

    private void ResizeLine(float currentLength) {
        lineRenderer.SetPosition(1, transform.position - Vector3.up * currentLength);
        lineRenderer.material.SetFloat("_Length", currentLength);
    }

    private void RescaleCollider(float currentLength) {
        boxCollider2D.size = new Vector2(boxCollider2D.size.x, currentLength);
        boxCollider2D.offset = new Vector2(0, -currentLength / 2);
    }

    private void CheckForSplashEffect(float currentLength, float currentMaxLength) {
        splashEffectObject.SetActive(currentLength >= currentMaxLength);
        splashEffectObject.transform.position = transform.position - Vector3.up * currentLength;
    }
}
