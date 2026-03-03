using UnityEngine;

public class BreathingScript : MonoBehaviour
{
    public float oxygenLevelCap = 100f;
    [SerializeField] float oxygenLevel = 100f;
    [SerializeField] float oxygenConsumtionRate = 1f;
    [SerializeField] float oxygenReplenishRate = 1f;
    [SerializeField] float idealReplenishRange;
    [SerializeField] float idealReplenishFraction = 3f;
    [SerializeField] float replenishTimer = 0f;
    [SerializeField] float replenishTimerLimit = 5f;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 point1;
    [SerializeField] Vector3 point2;
    [SerializeField] float velocity;
    [SerializeField] float velocitymodifier;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void Update()
    {
        if (player != null)
        {
            if (point1 == null)
            {
                point1 = player.transform.position;
            }
            if (point2 == null && point1 != null)
            {
                point2 = player.transform.position; 
            }
            velocity = point1.magnitude;
            Debug.Log("Current velocity is: " + velocity);
            if (oxygenLevel > 0)
            {
                float modifier = oxygenConsumtionRate * (velocity * velocitymodifier);
                oxygenLevel -= modifier * Time.deltaTime;
                Debug.Log($"Decreased oxygen level by {modifier}, oxygen level now {oxygenLevel}");
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                if (oxygenLevel < idealReplenishRange && idealReplenishRange == 0)
                {
                    replenishTimer = 0f;
                    idealReplenishRange = oxygenLevel * (1 / idealReplenishFraction);
                }
                oxygenLevel += oxygenReplenishRate * Time.deltaTime;
                Debug.Log($"Increased oxygen level by {oxygenReplenishRate}, oxygen level now {oxygenLevel}, ideal replenish range now {idealReplenishRange}");
            }
            if(idealReplenishRange > 0)
            {
                replenishTimer += Time.deltaTime;
                Debug.Log($"replenishTimer = {replenishTimer}");
            }
            if (replenishTimer >= replenishTimerLimit)
            {
                Debug.Log("Character fainted due to poor oxygen intake");
            }
        }
    }

    private void LogMessage()
    {
        Debug.Log("Current velocity is: " + velocity);
        Debug.Log($"replenishTimer = {replenishTimer}");
        Debug.Log($"Increased oxygen level by {oxygenReplenishRate}, oxygen level now {oxygenLevel}, ideal replenish range now {idealReplenishRange}");
        Debug.Log($"Decreased oxygen level by modifier, oxygen level now {oxygenLevel}");
    }
}
