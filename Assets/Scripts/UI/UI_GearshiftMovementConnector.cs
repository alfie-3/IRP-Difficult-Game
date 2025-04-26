using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GearshiftMovementConnector : MonoBehaviour
{
    [SerializeField] UI_GearshiftMovementConnector nextPosition;
    public UI_GearshiftMovementConnector[] connectedPoints;
    [field: Space]
    [field: SerializeField] public int Gear { get; private set; }

    public Vector3 position => transform.position;

    public UI_GearshiftMovementConnector NextPosition => nextPosition;
}
