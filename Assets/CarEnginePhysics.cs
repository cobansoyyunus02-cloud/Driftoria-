using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarEnginePhysics : MonoBehaviour
{
    [Header("Motor Gücü ve Performans")]
    public float enginePower = 3500f;
    public float maxRpm = 7500f;
    public float minRpm = 1000f;
    public float currentRpm;
    
    [Header("Şanzıman ve Vitesler")]
    public float[] gearRatios = { 3.82f, 2.2f, 1.5f, 1.15f, 0.9f, 0.75f };
    public float finalDrive = 3.9f;
    public int currentGear = 1;
    public bool isAutomatic = true;

    [Header("Ses ve Turbo")]
    public AudioSource engineAudio;
    public AudioClip engineIdleClip;
    public AudioClip engineHighClip;
    public AudioClip turboBlowOff;
    
    private Rigidbody rb;
    private MobileInputController inputs;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputs = GetComponent<MobileInputController>();
        if (!engineAudio) engineAudio = GetComponent<AudioSource>();
    }

    public float CalculateEngineTorque(float wheelRPM)
    {
        // RPM Hesabı ve Devir Dalgalanması
        float speed = rb.velocity.magnitude * 3.6f;
        currentRpm = Mathf.Lerp(minRpm, maxRpm, (speed % 60f) / 60f);

        // Motor Ses Perdesi (Pitch) Ayarı
        if (engineAudio)
        {
            engineAudio.pitch = Mathf.Lerp(0.8f, 2.4f, currentRpm / maxRpm);
        }

        if (inputs.GetThrottle() <= 0f) return 0f;

        // Tork Eğrisi Hesaplama (Sinüs tabanlı gerçekçi güç dağılımı)
        float rpmPercent = currentRpm / maxRpm;
        float torqueCurve = Mathf.Sin(rpmPercent * Mathf.PI);
        
        return inputs.GetThrottle() * enginePower * torqueCurve * gearRatios[currentGear - 1] * finalDrive;
    }
}
