using StarterAssets;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class MoveModifier : MonoBehaviour
{
    private float maxHeartRate; // linked to heart redline
    private float minHeartRate; // linked to heart stall
    private float heartRate; // linked to current heart rate
    private LubDubScript lubDubScript;

    private float airCapacity; // linked to current air in lungs
    private float maxAirCapacity; // linked to maximum lung capacity
    private BreathingImproved breathingImproved;

    public float maxJumpHeight; //Sets maximum jump height possible
    public float minJumpHeight; //Sets minimum jump height possible

    public float maxMoveSpeed; //Sets maximum move speed possible
    public float minMoveSpeed; //Sets minimum move speed possible
    private FirstPersonController firstPersonController;


    private void SetValues()
    {
        //set linked values from components

        lubDubScript = GetComponent<LubDubScript>();
        if (lubDubScript == null)
        {
            Debug.Log("LubDubScript missing!");
            return;
        }
        breathingImproved = GetComponent<BreathingImproved>();
        if (breathingImproved == null)
        {
            Debug.Log("BreathingImproved missing!");
            return;
        }
        firstPersonController = GetComponent<FirstPersonController>();
        if (firstPersonController == null)
        {
            Debug.Log("FirstPersonController missing!");
            return;
        }

        maxHeartRate = lubDubScript.redlineLimit;
        minHeartRate = lubDubScript.stallLimit;
        

        maxAirCapacity = breathingImproved.airMaxCapacity;
        airCapacity = breathingImproved.airCapacity;
    }

    private void Start()
    {
        SetValues();
    }

    public float GetJumpModified(float jumpModified)
    {
        heartRate = lubDubScript.heartRate;
        airCapacity = breathingImproved.airCapacity;
        float jumpRatio = Mathf.InverseLerp(minHeartRate, maxHeartRate, heartRate);
        float jumpTemp = Mathf.Lerp(minJumpHeight, maxJumpHeight, jumpRatio);
        float lungModifier = Mathf.InverseLerp(0, maxAirCapacity, airCapacity);
        jumpModified = jumpTemp * lungModifier;
       // Debug.Log($"New jump height {jumpModified}, lung mod {lungModifier}");
        return jumpModified;
    }

    public float GetMoveModified(float moveModified)
    {
        heartRate = lubDubScript.heartRate;
        if (moveModified > 0.1)
        {
            float moveRatio = Mathf.InverseLerp(minHeartRate, maxHeartRate, heartRate);
            moveModified = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, moveRatio);
            //Debug.Log($"New move speed {moveModified}");
            return moveModified;
        }
        else
        {
            return moveModified = 0;
        }
    }


}
