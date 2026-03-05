using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WakingUpScript : MonoBehaviour
{
    //show heart beat key press animation
    //when heart beat is constant
    //show lung breathing animation side by side
    //then passedOut = false and remove vignette slowly
    private PassedOutScript PassedOutScript;
    private LubDubScript heartScript;
    private BreathingImproved lungScript;
    [SerializeField] float heartBeatRate, breatheHoldDuration;

    public CanvasGroup heartIcons, lungIcons;

    private void Start()
    {
        PassedOutScript = GetComponent<PassedOutScript>();
        heartScript = GetComponent<LubDubScript>();
        lungScript = GetComponent<BreathingImproved>();
        StartCoroutine(FirstStart());
    }

    public IEnumerator FirstStart()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(WakeUpSequence(0, 0));
    }
    public IEnumerator WakeUpSequence(float t, float alpha)
    {
        heartIcons.transform.position = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        //fade key into view
        LeanTween.alphaCanvas(heartIcons, 1, 0.5f);

        t += Time.deltaTime;

        bool achievedHeartRate = false;
        while (PassedOutScript.value)
        {
            if(heartScript.heartRate > 18 && !achievedHeartRate)
            {
                LeanTween.move(heartIcons.gameObject, Camera.main.ViewportToScreenPoint(new Vector2(0.75f, 0.5f)), 0.25f);
                LeanTween.alphaCanvas(lungIcons, 1, 0.5f);
                achievedHeartRate = true;
            }
            if (lungScript.oxygenCapacity > 150 && heartScript.heartRate > 18)
            {
                PassedOutScript.value = false;
                PassedOutScript.passingOutCR = null;
                LeanTween.alphaCanvas(heartIcons, 0, 0.35f);
                LeanTween.alphaCanvas(lungIcons, 0, 0.35f);
                StartCoroutine(PassedOutScript.VignetteChanger(true, 1, 0));
            }
            yield return null;
        }
    }
}
