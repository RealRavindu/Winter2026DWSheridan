using UnityEngine;
using UnityEngine.Rendering;

public class PlayerControllerOrganControl : MonoBehaviour
{
    public float decayRate = 1.0f;
    public float decayDelay = 0f;
    public float additiveRate = 1.0f;
    private float currentInterval;
    private float previousInterval;
    private float intervalTimer = 0f;
    public bool isActive = false;
    private float velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("LogMessage", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            velocity = ModifyValue(velocity, additiveRate);
        }
    }

    private void FixedUpdate()
    {
        if (velocity != 0)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
        if (isActive)
        {
            intervalTimer += Time.fixedDeltaTime;
            if (previousInterval > 0f)
            {
                decayDelay -= Time.fixedDeltaTime;

                if (decayDelay < 0f)
                {
                    ModifyValue(velocity, decayRate * Time.deltaTime);
                }
            }
        }
    }

    private float ModifyValue(float targetValue, float modifierValue)
    {
        Debug.Log($"Value {targetValue} will modified by {modifierValue}");
        targetValue += modifierValue;
        Debug.Log($"New value is {targetValue}");
        return targetValue;
    }

    private void LogMessage()
    {
        Debug.Log("Current velocity is: " + velocity);
    }
}
