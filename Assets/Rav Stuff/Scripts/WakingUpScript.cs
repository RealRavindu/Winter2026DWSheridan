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
    [SerializeField] float timeToMaintainHeartBeat;
    [SerializeField] Slider heartProgressBar, lungProgressBar;
    public CanvasGroup heartIcons, lungIcons, sliderIcons;

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
        StartCoroutine(WakeUpSequence(true));
    }
    public IEnumerator WakeUpSequence(bool firstTime)
    {
        float t = 0;
        float heartProgress = 0;
        heartIcons.transform.position = Camera.main.ViewportToScreenPoint(new Vector2(0.5f + 0.25f * (!firstTime ? 1 : 0), 0.5f));
        //fade key and prog bar into view
        LeanTween.alphaCanvas(heartIcons, 1, 0.5f);
        LeanTween.alphaCanvas(lungIcons, firstTime ? 0 : 1, 0.5f);
        LeanTween.alphaCanvas(sliderIcons, 1, 0.5f);

        t += Time.deltaTime;

        bool achievedHeartRate = false;

        while (PassedOutScript.value)
        {
            if (heartScript.heartRate > 18)
            {
                heartProgress += Time.deltaTime;
                Mathf.Clamp(heartProgress, 0, timeToMaintainHeartBeat);

                if (heartProgress > timeToMaintainHeartBeat && !achievedHeartRate)
                {
                    LeanTween.move(heartIcons.gameObject, Camera.main.ViewportToScreenPoint(new Vector2(0.75f, 0.5f)), 0.25f);
                    LeanTween.alphaCanvas(lungIcons, 1, 0.5f);
                    achievedHeartRate=true;
                }

            }
            else
            {
                heartProgress -= Time.deltaTime;
            }

            heartProgressBar.value = heartProgress / timeToMaintainHeartBeat;
            lungProgressBar.value = lungScript.oxygenCapacity / 80;

            if (lungScript.oxygenCapacity > 80)
            {
                SucceedWakingUp();
            }

            yield return null;
        }

    }

    void SucceedWakingUp()
    {
        PassedOutScript.value = false;
        PassedOutScript.passingOutCR = null;
        LeanTween.alphaCanvas(heartIcons, 0, 0.35f);
        LeanTween.alphaCanvas(lungIcons, 0, 0.35f);
        LeanTween.alphaCanvas(sliderIcons, 0, 0.35f);
        StartCoroutine(PassedOutScript.VignetteChanger(true, 1, 0));
    }
}
