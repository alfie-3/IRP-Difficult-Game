using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GearStick : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] UI_ShifterHandler shifter;

    Vector2 offset;

    RectTransform MainCanvasRectTrasnform => (RectTransform)transform.root;
    CanvasScaler MainCanvasScaler => transform.root.GetComponent<CanvasScaler>();
    RectTransform RectTransform => (RectTransform)transform;

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(MainCanvasRectTrasnform, eventData.position, eventData.pressEventCamera, out Vector3 worldPos);

        Vector3 closestPoint = shifter.GetClosestLinePoint(worldPos);

        Debug.DrawLine(worldPos, worldPos + (Vector3.back * 1000), Color.green);

        if (closestPoint != Vector3.zero)
            RectTransform.transform.position = closestPoint;
    }
}
