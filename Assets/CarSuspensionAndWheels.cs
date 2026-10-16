using UnityEngine;

public class CarSuspensionAndWheels : MonoBehaviour
{
    [Header("Tekerlek Collider Referansları")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [Header("Süspansiyon Ayarları")]
    public float springTravel = 0.3f;
    public float damperTravel = 0.15f;
    public float suspensionDistance = 0.4f;

    void Start()
    {
        ConfigureWheel(frontLeftWheel);
        ConfigureWheel(frontRightWheel);
        ConfigureWheel(rearLeftWheel);
        ConfigureWheel(rearRightWheel);
    }

    void ConfigureWheel(WheelCollider wheel)
    {
        if (!wheel) return;
        JointSpring suspensionSpring = wheel.suspensionSpring;
        suspensionSpring.spring = 35000f;
        suspensionSpring.damper = 4500f;
        suspensionSpring.targetPosition = 0.5f;
        wheel.suspensionSpring = suspensionSpring;
        wheel.suspensionDistance = suspensionDistance;
    }

    public void UpdateWheelVisuals()
    {
        UpdateSingleWheel(frontLeftWheel);
        UpdateSingleWheel(frontRightWheel);
        UpdateSingleWheel(rearLeftWheel);
        UpdateSingleWheel(rearRightWheel);
    }

    void UpdateSingleWheel(WheelCollider wheel)
    {
        if (!wheel) return;
        Transform visualWheel = wheel.transform.GetChild(0);
        if (!visualWheel) return;

        wheel.GetWorldPose(out Vector3 pos, out Quaternion rot);
        visualWheel.position = pos;
        visualWheel.rotation = rot;
    }
}
