using System.Collections;
using UnityEngine;

public class WakingUpScript : MonoBehaviour
{
    //show heart beat key press animation
    //when heart beat is constant
    //show lung breathing animation side by side
    //then passedOut = false and remove vignette slowly
    private PassedOutScript PassedOutScript;
    public IEnumerator WakeUpSequence()
    {
        while (PassedOutScript.value)
        {
            
        }
    }
}
