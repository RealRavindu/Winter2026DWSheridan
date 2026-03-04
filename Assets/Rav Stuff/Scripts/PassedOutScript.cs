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

    private void Start()
    {
        controller = GetComponent<FirstPersonController>();
        if(!volume.profile.TryGet(out vignette))
        {
            Debug.Log("Vignette effect not found");
        }
    }
    //counts up till 'timeTillWakeUp' and then sets isPassedOut to true. Need to implement any animation coding here.
    public IEnumerator passedOutTimer()
    {
        controller.enabled = false;
        value = true;
        StartCoroutine(VignetteChanger(false, vignette.intensity.value, 0));
        yield return new WaitForSeconds(timeTillWakeUp);
        m_WokenUp.Invoke();
        value = false;
        controller.enabled = true;
        StartCoroutine(VignetteChanger(true, vignette.intensity.value, 0));
    }

    public IEnumerator VignetteChanger(bool wakingUp, float startingIntensity, float t)
    {
        while(t < 1)
        {

            print("vignette running, t: " + t);
            float endIntensity = wakingUp ? 0 : 1;
            print(endIntensity);
            t += Time.deltaTime / faintTime;

            vignette.intensity.value = Mathf.Lerp(startingIntensity, endIntensity, t);

            yield return null;
        }
    }
}
