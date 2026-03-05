using UnityEngine;

public class HeartSpriteAnim : MonoBehaviour
{
    private bool secondBeat = false;
    public float heartDecreaseRate, small, big;
    private Vector3 startingSize;


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
        HeartRateShake(heart.heartRate, heart.redlineLimit);
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

    void HeartRateShake(float heartRate, float maxHeartRate)
    {
        Debug.Log($"AAAAAAAL{maxHeartRate}, {heart.redlineLimit}, {heartRate}");
        float frequency = heartRate / maxHeartRate;
        print("freq AAAAAAAAAAAAAH: " + frequency);
        Vector2 newPos = new Vector2(Mathf.Cos(Time.time * frequency), 0) + (Vector2)transform.position;
        print("newpos AAAAAAAAAAAAAH: " + newPos);
        print("math.cos no freq AAAAAAAAAAAAAH: " + Mathf.Cos(Time.time%1));
        transform.position = newPos;
        
    }
}
