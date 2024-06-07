using UnityEngine;

public class ForceFromMicrophone : MonoBehaviour
{
    public float force;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private Vector3 minScale;

    [SerializeField]
    private Vector3 maxScale;

    [SerializeField]
    private AudioLoudnessDetection detector;

    [SerializeField]
    private float loudnessSensibility = 100;

    [SerializeField]
    private float threshold = 0.1f;


    // Update is called once per frame
    void Update()
    {
        force = MicForce();
    }

    private float MicForce()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        if (loudness < threshold)
        {
            loudness = 0;
        }
        return loudness;
    }
}
