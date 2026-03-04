using UnityEngine;
using UnityEngine.UI;
public class BarScript : MonoBehaviour
{
    public Slider oxygenBar;
    [SerializeField] private float oxygenRate, timeOfLastBreath;
    public float oxygenDecreaseRate;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeOfLastBreath = Time.time;
        }

        oxygenRate = 1 / (Time.time - timeOfLastBreath);
        oxygenBar.value = oxygenRate;
    }
}
