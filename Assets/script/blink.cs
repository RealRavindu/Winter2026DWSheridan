using UnityEngine;

public class blink : MonoBehaviour
{
    public SpriteRenderer sr;
    public Color c;
    public float tired;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        c = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        c.a = tired;
        sr.color = c;
    }
}
