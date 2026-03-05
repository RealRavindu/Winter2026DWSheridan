using System.Collections;
using UnityEngine;

public class WakingUpScript : MonoBehaviour
{
    //show heart beat key press animation
    //when heart beat is constant
    //show lung breathing animation side by side
    //then passedOut = false and remove vignette slowly
    private PassedOutScript PassedOutScript;
    [SerializeField] float heartBeatRate, breatheHoldDuration;

    private void Start()
    {
        PassedOutScript = GetComponent<PassedOutScript>();
        StartCoroutine(WakeUpSequence(0));
    }
    public IEnumerator WakeUpSequence(float t)
    {
        t += Time.deltaTime;

        while (PassedOutScript.value)
        {
            
            yield return null;
        }
    }
}
