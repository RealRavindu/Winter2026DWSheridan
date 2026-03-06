using UnityEngine;
using System.Collections;
using UnityEditor.ShaderKeywordFilter;
public class HeartSpriteAnim : MonoBehaviour
{
    private bool secondBeat = false;
    public float heartDecreaseRate, small, big, shakeAmplitude;
    private Vector3 startingSize;
    private Coroutine heartShakeCR;
    [SerializeField] float shakeRate;

    [SerializeField] LubDubScript heart;
    public Transform childSprite;
    private void Start()
    {
        startingSize = childSprite.localScale;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (heart.input == 0)
            {
                HeartIncreaseSmall();

            } else
            {
                HeartIncreaseBig();
            }

        }

        DecreaseHeartSize();
        if(heartShakeCR == null) heartShakeCR = StartCoroutine(HeartRateShake(0, heart.redlineLimit, transform.position));
    }

    void HeartIncreaseSmall()
    {
        Vector2 heartSize = (Vector2)transform.localScale + Vector2.one* small;
        //childSprite.localScale = heartSize;
        LeanTween.scale(childSprite.gameObject, heartSize, 0.02f).setEaseOutSine();
    }

    void HeartIncreaseBig()
    {
        Vector2 heartSize = (Vector2)transform.localScale + Vector2.one * big;
        //childSprite.localScale = heartSize;
        LeanTween.scale(childSprite.gameObject, heartSize, 0.02f).setEaseOutSine();
    }

    void DecreaseHeartSize()
    {
        if (childSprite.localScale.magnitude > startingSize.magnitude)
        {
            Vector2 heartSize = childSprite.localScale;
            heartSize -= Vector2.one * heartDecreaseRate;
            childSprite.localScale = heartSize;
        }
    }
    
    private IEnumerator HeartRateShake(float currentFreq, float maxHeartRate, Vector2 startPos)
    {
        while (true)
        {
            float frequency = heart.heartRate / maxHeartRate;
            currentFreq += (frequency - currentFreq) * shakeRate * Time.deltaTime;
            Vector2 newPos = new Vector2(Mathf.Cos(Time.time * (1 + currentFreq)), 0) * shakeAmplitude + startPos;
            transform.position = newPos;
            yield return null;
        }
    }
}
