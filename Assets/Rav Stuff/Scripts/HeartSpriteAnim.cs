using UnityEngine;

public class HeartSpriteAnim : MonoBehaviour
{
    private bool secondBeat = false;
    public float heartDecreaseRate, small, big;
    private Vector3 startingSize;

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
}
