using UnityEngine;

public class MobileInputController : MonoBehaviour
{
    private float throttle;
    private float steering;
    private float footBrake;
    private bool handbrake;
    private bool boost;

    // UI butonlarından veya Joystick'ten gelen verileri günceller
    public void SetInputs(float _throttle, float _steering, float _footBrake, bool _handbrake, bool _boost)
    {
        throttle = Mathf.Clamp(_throttle, -1f, 1f);
        steering = Mathf.Clamp(_steering, -1f, 1f);
        footBrake = Mathf.Clamp01(_footBrake);
        handbrake = _handbrake;
        boost = _boost;
    }

    public float GetThrottle() => throttle;
    public float GetSteering() => steering;
    public float GetFootBrake() => footBrake;
    public bool GetHandbrake() => handbrake;
    public bool GetBoost() => boost;
}
