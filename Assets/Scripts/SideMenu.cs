using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SideMenu : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform sideMenuRectTransform;
    private float width;
    private float startPositionX;
    private float startingAnchoredPositionX;

    public enum Side { left, right}
    public Side side;

    // Start is called before the first frame update
    void Start() {
        width = Screen.width;
    }

    public void OnDrag(PointerEventData eventData) {
        sideMenuRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(startingAnchoredPositionX - (startPositionX - eventData.position.x), GetMinPosition(), GetMaxPosition()), 0);
    }

    public void OnPointerDown(PointerEventData eventData) {
        StopAllCoroutines();
        startPositionX = eventData.position.x;
        startingAnchoredPositionX = sideMenuRectTransform.anchoredPosition.x;
    }

    public void OnPointerUp(PointerEventData eventData) {
        StartCoroutine(HandleMenuSlide(.25f, sideMenuRectTransform.anchoredPosition.x, isAfterHalfPoint() ? GetMinPosition() : GetMaxPosition()));
    }

    private bool isAfterHalfPoint() {
        if (side == Side.right)
            return sideMenuRectTransform.anchoredPosition.x < width;
        else
            return sideMenuRectTransform.anchoredPosition.x < 0;
    }

    private float GetMinPosition() {
        if(side == Side.right)
            return width / 2;
        return -width * .4f;
    }

    private float GetMaxPosition() {
        if(side == Side.right)
            return width * 1.4f;
        return width / 2;
    }

    private IEnumerator HandleMenuSlide(float slideTime, float startingX, float targetX) {
        for (float i = 0; i <= slideTime; i+= .025f) {
            sideMenuRectTransform.anchoredPosition = new Vector2(Mathf.Lerp(startingX, targetX, i / slideTime), 0);
            yield return new WaitForSecondsRealtime(.025f);
        }

    }

}
