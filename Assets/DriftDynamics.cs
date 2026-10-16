using UnityEngine;

public class DriftDynamics : MonoBehaviour
{
    [Header("Yol Tutuş (Grip) Katsayıları")]
    public float normalStiffness = 2.5f;
    public float driftStiffness = 0.6f;
    
    private WheelFrictionCurve forwardFriction;
    private WheelFrictionCurve sidewaysFriction;
    private CarSuspensionAndWheels wheels;
    private MobileInputController inputs;

    void Start()
    {
        wheels = GetComponent<CarSuspensionAndWheels>();
        inputs = GetComponent<MobileInputController>();
    }

    void FixedUpdate()
    {
        ApplyDriftPhysics(wheels.rearLeftWheel);
        ApplyDriftPhysics(wheels.rearRightWheel);
    }

    void ApplyDriftPhysics(WheelCollider wheel)
    {
        if (!wheel) return;
        WheelFrictionCurve sideways = wheel.sidewaysFriction;

        // El freni çekildiğinde arka tekerleklerin yanal sürtünmesini düşürerek kusursuz drift açısı ver
        if (inputs.GetHandbrake())
        {
            sideways.stiffness = Mathf.Lerp(sideways.stiffness, driftStiffness, Time.fixedDeltaTime * 5f);
        }
        else
        {
            sideways.stiffness = Mathf.Lerp(sideways.stiffness, normalStiffness, Time.fixedDeltaTime * 5f);
        }

        wheel.sidewaysFriction = sideways;
    }
}
