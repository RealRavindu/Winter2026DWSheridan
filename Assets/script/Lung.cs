using UnityEngine;

public class Lung : MonoBehaviour
{
    public float t = 1;
    public Vector3 lungSize;
    public bool r = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lungSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (r==true)
        {
            t += Time.deltaTime;
            if(t > 2)
            {
                r = false;
            }
        }
        else if (r == false)
        {
            t -= Time.deltaTime;
            if (t < 1.5)
            {
                r = true;
            }
        }
        lungSize.x = t;
        lungSize.y = t;
        transform.localScale = lungSize;
    
    }
}
