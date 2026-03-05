using StarterAssets;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class MoveModifier : MonoBehaviour
{
    private float maxHeartRate;
    private float minHeartRate;
    private float heartRate;
    private LubDubScript lubDubScript;

    private float airCapacity;
    private float maxAirCapacity;
    private BreathingImproved breathingImproved;

    public float maxJumpHeight;
    public float minJumpHeight;
    private float jumpModifier;
    private float jumpHeight;

    public float maxMoveSpeed;
    public float minMoveSpeed;
    private float moveSpeed;
    private FirstPersonController firstPersonController;
    

    private void SetValues()
    {
        lubDubScript = GetComponent<LubDubScript>();
        breathingImproved = GetComponent<BreathingImproved>();
        firstPersonController = GetComponent<FirstPersonController>();

        maxHeartRate = lubDubScript.redlineLimit;
        minHeartRate = lubDubScript.stallLimit;
        heartRate = lubDubScript.heartRate;

        maxAirCapacity = breathingImproved.airMaxCapacity;
        airCapacity = breathingImproved.airCapacity;

        jumpHeight = firstPersonController.JumpHeight;
        moveSpeed = firstPersonController.MoveSpeed;
    }

    private void Start()
    {
        SetValues();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           float temp = GetJumpModified(jumpHeight);
            float temp2 = GetMoveModified(moveSpeed);
        }
    }

    public float GetJumpModified(float jumpModified)
    {
        float jumpRatio = Mathf.InverseLerp(minHeartRate, maxHeartRate, heartRate);
        float jumpTemp = Mathf.Lerp(minJumpHeight, maxJumpHeight, jumpRatio);
        float lungModifier = Mathf.InverseLerp(0, maxAirCapacity, airCapacity);
        Debug.Log($"New jump height {jumpTemp}, lung mod {lungModifier}, Old jump height {jumpHeight}");
        jumpModified = jumpTemp * jumpRatio;
        return jumpModified;
    }

    public float GetMoveModified(float moveModified)
    {
        float moveRatio = Mathf.InverseLerp(minHeartRate, maxHeartRate, heartRate);
        moveModified = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, moveRatio);
        Debug.Log($"New move speed {moveModified}, Old move speed{moveSpeed}");
        return moveModified;
    }


}
