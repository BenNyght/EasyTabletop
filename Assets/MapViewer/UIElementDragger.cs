using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIElementDragger : EventTrigger
{
    private bool dragging;

    public void Update()
    {
        if (dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            dragging = true;
        else if (eventData.button == PointerEventData.InputButton.Right)
            Destroy(gameObject);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            dragging = false;
    }
}
