using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform joystick;

    private float radius;

    // Start is called before the first frame update
    void Start()
    {
        radius = background.rect.width * 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        Debug.Log((Vector2)background.position);
        Vector2 value = eventData.position - (Vector2)background.position;

        value = Vector2.ClampMagnitude(value, radius);

        joystick.localPosition = value;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.localPosition = Vector2.zero;
    }
}