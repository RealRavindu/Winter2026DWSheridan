using JetBrains.Annotations;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("checkpoint array & target transform.")]
    public Transform[] checkpoint;
    private Transform target;
    private int currentCheckpoint = 0;

    [Header("Platform movement speed")]
    public float moveSpeed;
    
     

    void Start()
    {
        target = checkpoint[currentCheckpoint];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        target = checkpoint[currentCheckpoint];

        if (Vector3.Distance(transform.position, target.position) <= 0)
        {
            currentCheckpoint++;

            if (currentCheckpoint >= checkpoint.Length)
            {
                currentCheckpoint = 0;
            }
        }
    }
}
