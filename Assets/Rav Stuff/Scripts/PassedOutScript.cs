using UnityEngine;
using UnityEngine.Events;
using System.Collections;
public class PassedOutScript : MonoBehaviour
{
    public bool value = false;
    [SerializeField] float timeTillWakeUp;
    public UnityEvent m_WokenUp;


    //counts up till 'timeTillWakeUp' and then sets isPassedOut to true. Need to implement any animation coding here.
    public IEnumerator passedOutTimer()
    {
        value = true;
        yield return new WaitForSeconds(timeTillWakeUp);
        m_WokenUp.Invoke();
        value = false;
    }

}
