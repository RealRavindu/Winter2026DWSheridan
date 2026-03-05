using UnityEngine;

public class Beat : MonoBehaviour
{
    public float beat = 10;
    public float BPM = 1;
    public float beatSpeed = 50;
    public Vector3 beatSize;
    public bool r = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (r==true)
        {
            beat += BPM;
            if(beat > beatSpeed)
            {
                r = false;
            }
        }
        else if (r == false)
        {
            beat -= BPM;
            if (beat < 10)
            {
                r = true;
            }
        }
        beatSize.x = beat/10;
        beatSize.y = beat/10;
        transform.localScale = beatSize;
    
    }
}
