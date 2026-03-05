using NUnit.Framework.Internal;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlinkingScript : MonoBehaviour
{
    public Transform LeftTop, LeftBottom, RightTop, RightBottom;
    public KeyCode blinkLeftKey, blinkRightKey;
    private PassedOutScript passedOutScript;
    public Material _leftBlurMat, _rightBlurMat;
    private float leftBlurAmount, rightBlurAmount;
    public float blurModifier;


    //key reminder signifiers
    [SerializeField] CanvasGroup leftBlinkReminder, rightBlinkReminder;
    [SerializeField] float timeToRemind;

    private void Start()
    {
        passedOutScript = GetComponent<PassedOutScript>();
    }

    private void Update()
    {
        //setting blur amount which will then be input into the material variables
        leftBlurAmount += blurModifier * Time.deltaTime;
        rightBlurAmount += blurModifier * Time.deltaTime;


        _leftBlurMat.SetFloat("_Blur", leftBlurAmount);
        _rightBlurMat.SetFloat("_Blur", rightBlurAmount);

        //if not already passed out, check for blinking key presses
        if (!passedOutScript.value)
        {
            if (Input.GetKey(blinkLeftKey))
            {
                CloseLeftEye();
            }
            else
            {
                OpenLeftEye();
            }

            if (Input.GetKey(blinkRightKey))
            {
                CloseRightEye();
            }
            else
            {
                OpenRightEye();
            }
        }
        else
        {
            OpenLeftEye();
            OpenRightEye();
        }

        showKeyReminders(); //makes a key icon with the button for the appropriate blink appear if haven't blinked in a while
    }

    private void CloseLeftEye()
    {
        LeftTop.gameObject.SetActive(true);
        LeftBottom.gameObject.SetActive(true);
        leftBlurAmount = 0;

        //get rid of signifier
        leftBlinkReminder.alpha = 0;
    }
    void CloseRightEye()
    {
        RightTop.gameObject.SetActive(true);
        RightBottom.gameObject.SetActive(true);
        rightBlurAmount = 0;

        //get rid of signifier
        rightBlinkReminder.alpha = 0;
    }
    void OpenLeftEye()
    {
        LeftTop.gameObject.SetActive(false);
        LeftBottom.gameObject.SetActive(false);
    }
    void OpenRightEye()
    {
        RightTop.gameObject.SetActive(false);
        RightBottom.gameObject.SetActive(false);
    }

    void showKeyReminders()
    {
        if(leftBlurAmount > timeToRemind)
        {
            LeanTween.alphaCanvas(leftBlinkReminder, 1, 2);
        }
        if (rightBlurAmount > timeToRemind)
        {
            LeanTween.alphaCanvas(rightBlinkReminder, 1, 2);
        }
    }
}
