using UnityEngine;
using System.Collections;
public class HeartSpriteAnim : MonoBehaviour
{
    private bool secondBeat = false;
    public float heartDecreaseRate, small, big, shakeAmplitude;
    private Vector3 startingSize;
    private Coroutine heartShakeCR;

    [SerializeField] LubDubScript heart;

    private void Start()
    {
        startingSize = transform.localScale;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!secondBeat)
            {
                secondBeat = true;
                HeartIncreaseSmall();

            } else
            {
                HeartIncreaseBig();
                secondBeat = false;
            }

        }

        DecreaseHeartSize();
        if(heartShakeCR == null) heartShakeCR = StartCoroutine(HeartRateShake(heart.heartRate, heart.redlineLimit, transform.position));
    }

    void HeartIncreaseSmall()
    {
        Vector2 heartSize = (Vector2)transform.localScale + Vector2.one* small;
        transform.localScale = heartSize;
        LeanTween.scale(transform.gameObject, heartSize, 0.08f).setEaseOutSine();
    }

    void HeartIncreaseBig()
    {
        Vector2 heartSize = (Vector2)transform.localScale + Vector2.one * big;
        transform.localScale = heartSize;
        LeanTween.scale(transform.gameObject, heartSize, 0.08f).setEaseOutSine();
    }

    void DecreaseHeartSize()
    {
        if(transform.localScale.magnitude > startingSize.magnitude)
        {
            Vector2 heartSize = transform.localScale;
            heartSize -= Vector2.one * heartDecreaseRate;
            transform.localScale = heartSize;
        }
    }

    private IEnumerator HeartRateShake(float heartRate, float maxHeartRate, Vector2 startPos)
    {
        while (true)
        {
            float frequency = heartRate / maxHeartRate;
            Vector2 newPos = new Vector2(Mathf.Cos(Time.time % Mathf.PI ), 0) * shakeAmplitude + startPos;
            transform.position = newPos;
            yield return null;
        }
    }
}
