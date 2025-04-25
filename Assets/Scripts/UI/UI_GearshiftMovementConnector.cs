using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GearshiftMovementConnector : MonoBehaviour
{
    [SerializeField] UI_GearshiftMovementConnector nextPosition;

    public UI_GearshiftMovementConnector NextPosition => nextPosition;
}
