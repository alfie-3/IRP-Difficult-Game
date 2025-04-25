using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GearStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] UI_ShifterHandler shifter;

    bool grabbed = false;

    UI_GearshiftMovementConnector targetPos;

    Vector2 offset;

    RectTransform MainCanvasRectTrasnform => (RectTransform)transform.root;
    CanvasScaler MainCanvasScaler => transform.root.GetComponent<CanvasScaler>();
    RectTransform RectTransform => (RectTransform)transform;

    private void Update()
    {
        if (!grabbed && targetPos != null)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos.transform.position, 10 * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos.transform.position) < 10f)
            {
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

        Debug.Log(Vector3.Dot(transform.position, closestPoint));

        if (closestPoint != Vector3.zero)
            RectTransform.transform.position = closestPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        grabbed = false;
        GearshiftLine anchor = shifter.CurrentLine;

        if (Vector3.Distance(anchor.End.transform.position, transform.position) < Vector3.Distance(anchor.Start.transform.position, transform.position))
        {
            targetPos = anchor.End.GetComponent<UI_GearshiftMovementConnector>();
        }
        else
        {
            targetPos = anchor.Start.GetComponent<UI_GearshiftMovementConnector>();
        }
    }
}
