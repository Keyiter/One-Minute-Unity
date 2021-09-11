using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform circleSpace;
    public RectTransform innerCircle;

    public Vector2 inputVector;
    public void OnDrag(PointerEventData eventData) {
        CalculateInnerCirclePosition(eventData.position);
        CalculateInputVector();
        CalculateInnerCircleRotation();
    }

    public void OnPointerDown(PointerEventData eventData) {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData) {
        innerCircle.anchoredPosition = Vector2.zero;
        innerCircle.localRotation = Quaternion.identity;
        inputVector = Vector2.zero;
    }

    private void CalculateInnerCirclePosition(Vector2 position) {
        Vector2 directPosition = position - (Vector2)circleSpace.position;
        if (directPosition.magnitude > circleSpace.rect.width / 2f)
            directPosition = directPosition.normalized * circleSpace.rect.width / 2f;
        innerCircle.anchoredPosition = directPosition;
    }

    private void CalculateInputVector() {
        inputVector = innerCircle.anchoredPosition / (circleSpace.rect.size / 2f);
    }
    
    private void CalculateInnerCircleRotation() {
        innerCircle.localRotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, inputVector));
    }
}
