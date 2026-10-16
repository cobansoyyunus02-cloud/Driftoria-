using UnityEngine;
using UnityEngine.EventSystems;

public class TouchUIController : MonoBehaviour
{
    private MobileInputController inputController;

    private float throttleInput;
    private float brakeInput;
    private float steeringInput;
    private bool handbrakeInput;

    void Start()
    {
        // Sahnedeki aracı ve onun input sistemini bulur
        GameObject car = GameObject.FindWithTag("Player");
        if (car != null)
        {
            inputController = car.GetComponent<MobileInputController>();
        }
    }

    void Update()
    {
        if (inputController != null)
        {
            // Girdileri ana kontrolcüye gönderir
            inputController.SetInputs(throttleInput, steeringInput, brakeInput, handbrakeInput, false);
        }
    }

    // UI Butonları için tetiklenecek fonksiyonlar
    public void OnThrottleDown() => throttleInput = 1f;
    public void OnThrottleUp() => throttleInput = 0f;

    public void OnBrakeDown() => brakeInput = 1f;
    public void OnBrakeUp() => brakeInput = 0f;

    public void OnHandbrakeDown(bool isPressed) => handbrakeInput = isPressed;

    public void OnSteeringChange(float value)
    {
        steeringInput = value; // Slider veya Joystick'ten gelen değer (-1 ile 1 arası)
    }
}
