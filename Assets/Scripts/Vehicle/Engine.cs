using System;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] WheelsController wheelsController;
    [field: Space]
    [field: SerializeField] public bool Running { get; private set; }
    [field: SerializeField] public float Throttle;
    [field: SerializeField] public float Brake;
    [Space]
    [SerializeField] AnimationCurve motorCurve;
    [SerializeField] float engineRPM = 0;
    [SerializeField] float motorPower = 1000f;
    [SerializeField] float brakePower = 1000f;

    [field: SerializeField] public int CurrentGearIndex { get; private set; }

    public Action<bool> OnChangeCarRunnng = delegate { };

    public float Speed;
    public float SpeedClamped;

    private void Start()
    {
        ToggleEngineRunning();
    }

    private void Update()
    {
        Speed = wheelsController.GetDriveWheelAverageRPM() * wheelsController.Wheels[0].WheelCollider.radius * 2f * Mathf.PI / 10;
        SpeedClamped = Mathf.Lerp(SpeedClamped, Speed, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        ApplyThrottle();
    }

    public float GetSpeedRatio()
    {
        var throttle = Mathf.Clamp(Throttle, 0.5f, 1);
        return (SpeedClamped * (engineRPM * Throttle) / 1) * Time.fixedDeltaTime;
    }

    public void ToggleEngineRunning()
    {
        Running = !Running;

        OnChangeCarRunnng.Invoke(Running);
    }

    public void ApplyThrottle()
    {
        if (!Running) return;

        float motorTarget = Throttle > Brake ? Throttle : -Brake;
        engineRPM = Mathf.Lerp(engineRPM, motorTarget, Time.fixedDeltaTime * 3);
        float curveValue = motorCurve.Evaluate(engineRPM / motorTarget);

        if (Throttle > 0.1f)
        {
            wheelsController.Throttle(Throttle * motorPower * curveValue);
            return;
        }
        else if (Brake > 0.1f)
        {
            wheelsController.Throttle(Brake * -motorPower * curveValue);
            return;
        }

        wheelsController.Throttle(Brake * -motorPower);
    }
}