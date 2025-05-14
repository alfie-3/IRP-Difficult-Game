using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GearStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] UI_ShifterHandler shifter;
    [SerializeField] float moveSpeed = 800;

    bool grabbed = false;

    UI_GearshiftMovementConnector targetPos;

    Vector2 offset;

    RectTransform MainCanvasRectTrasnform => (RectTransform)transform.root;
    RectTransform RectTransform => (RectTransform)transform;

    private void Update()
    {
        if (!grabbed && targetPos != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.transform.position, moveSpeed * Time.deltaTime);
            Vector3 closestPoint = shifter.GetClosestLinePoint(transform.position);
            Debug.DrawLine(transform.position, closestPoint, Color.green);

            shifter.ChangeGear(targetPos.Gear);

            if (Vector3.Distance(transform.position, targetPos.transform.position) < 5f)
            {
                transform.position = targetPos.transform.position;
                targetPos = targetPos.NextPosition;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        grabbed = true;
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

    public void OnEndDrag(PointerEventData eventData)
    {
        grabbed = false;

        if (Vector3.Distance(shifter.endConnector.position, transform.position) < Vector3.Distance(shifter.startConnector.position, transform.position))
        {
            targetPos = shifter.endConnector;
        }
        else
        {
            targetPos = shifter.startConnector;
        }
    }
}
