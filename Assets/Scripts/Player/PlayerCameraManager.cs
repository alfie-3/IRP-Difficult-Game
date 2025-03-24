using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField] Transform followTranform;
    [SerializeField] GameObject cameraPrefab;
    public Transform CameraTransform { get; private set; }


    public void Awake()
    {
        CinemachineFreeLook freeLook = Instantiate(cameraPrefab).GetComponent<CinemachineFreeLook>();
        freeLook.Follow = followTranform;
        freeLook.LookAt = followTranform;

        CameraTransform = Camera.main.transform;
    }
}
