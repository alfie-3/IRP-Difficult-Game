using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Parameters", order = 0)]
public class PlayerParameters : ScriptableObject
{
    [field: SerializeField] public float StickStrength = 100f;
    [field: SerializeField] public float RollTorque = 500;
}
