using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using StarterAssets;
public class PassedOutScript : MonoBehaviour
{
    public bool value = false;
    [SerializeField] float timeTillWakeUp;
    public UnityEvent m_WokenUp;
    private FirstPersonController controller;

    private void Start()
    {
        controller = GetComponent<FirstPersonController>();
    }
    //counts up till 'timeTillWakeUp' and then sets isPassedOut to true. Need to implement any animation coding here.
    public IEnumerator passedOutTimer()
    {
        controller.enabled = false;
        value = true;
        yield return new WaitForSeconds(timeTillWakeUp);
        m_WokenUp.Invoke();
        value = false;
        controller.enabled = true;
    }

}
