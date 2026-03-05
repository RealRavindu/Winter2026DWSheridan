using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using StarterAssets;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PassedOutScript : MonoBehaviour
{
    public bool value = false;
    [SerializeField] float timeTillWakeUp;
    public UnityEvent m_WokenUp;
    private FirstPersonController controller;
    [SerializeField] Volume volume;
    private Vignette vignette;
    public float faintTime;
    private WakingUpScript wakingupscript;


    public Coroutine passingOutCR;

    private void Start()
    {
        controller = GetComponent<FirstPersonController>();
        wakingupscript = GetComponent<WakingUpScript>();

        if(!volume.profile.TryGet(out vignette))
        {
            Debug.Log("Vignette effect not found");
        }
    }

    public void PassOut()
    {
        if (passingOutCR == null) passingOutCR = StartCoroutine(passingOut());
    }
    //counts up till 'timeTillWakeUp' and then sets isPassedOut to true. Need to implement any animation coding here.
    public IEnumerator passingOut()
    {
        controller.enabled = false;
        value = true;
        StartCoroutine(VignetteChanger(false, vignette.intensity.value, 0));
        yield return null;
    }

    public IEnumerator VignetteChanger(bool wakingUp, float startingIntensity, float t)
    {
        while(t < 1)
        {

            float endIntensity = wakingUp ? 0 : 1;
            t += Time.deltaTime / faintTime;
            vignette.intensity.value = Mathf.Lerp(startingIntensity, endIntensity, t);
            yield return null;
        }
        if (!wakingUp) StartCoroutine(wakingupscript.WakeUpSequence(0, 0));
    }
}
