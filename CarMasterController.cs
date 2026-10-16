using UnityEngine;

[RequireComponent(typeof(MobileInputController))]
[RequireComponent(typeof(CarEnginePhysics))]
[RequireComponent(typeof(CarSuspensionAndWheels))]
[RequireComponent(typeof(DriftDynamics))]
[RequireComponent(typeof(Rigidbody))]
public class CarMasterController : MonoBehaviour
{
    private MobileInputController inputs;
    private CarEnginePhysics engine;
    private CarSuspensionAndWheels wheels;
    private Rigidbody rb;

    [Header("Direksiyon ve Ağırlık")]
    public float maxSteeringAngle = 38f;
    public Vector3 centerOfMassOffset = new Vector3(0f, -0.7f, 0.1f);
    public float brakeForce = 6000f;

    void Start()
    {
        inputs = GetComponent<MobileInputController>();
        engine = GetComponent<CarEnginePhysics>();
        wheels = GetComponent<CarSuspensionAndWheels>();
        rb = GetComponent<Rigidbody>();

        // Arabanın takla atmasını tamamen engelleyen profesyonel ağırlık merkezi
        rb.centerOfMass = centerOfMassOffset;
    }

    void FixedUpdate()
    {
        // 1. Direksiyon Kontrolü
        float steeringAngle = inputs.GetSteering() * maxSteeringAngle;
        wheels.frontLeftWheel.steerAngle = steeringAngle;
        wheels.frontRightWheel.steerAngle = steeringAngle;

        // 2. Motor Gücü İletimi (Arkadan İtiş - RWD)
        float motorTorque = engine.CalculateEngineTorque(wheels.rearLeftWheel.rpm);
        wheels.rearLeftWheel.motorTorque = motorTorque;
        wheels.rearRightWheel.motorTorque = motorTorque;

        // 3. Frenleme Sistemi
        float currentBrake = inputs.GetFootBrake() * brakeForce;
        wheels.frontLeftWheel.brakeTorque = currentBrake;
        wheels.frontRightWheel.brakeTorque = currentBrake;

        if (inputs.GetHandbrake())
        {
            wheels.rearLeftWheel.brakeTorque = brakeForce * 1.5f;
            wheels.rearRightWheel.brakeTorque = brakeForce * 1.5f;
        }
        else
        {
            wheels.rearLeftWheel.brakeTorque = 0f;
            wheels.rearRightWheel.brakeTorque = 0f;
        }

        // 4. Tekerlek Görsellerini Güncelle
        wheels.UpdateWheelVisuals();
    }
}
