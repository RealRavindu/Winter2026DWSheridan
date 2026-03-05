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


    private void Start()
    {
        passedOutScript = GetComponent<PassedOutScript>();
    }

    private void Update()
    {
        leftBlurAmount += blurModifier * Time.deltaTime;
        rightBlurAmount += blurModifier * Time.deltaTime;


        _leftBlurMat.SetFloat("_Blur", leftBlurAmount);
        _rightBlurMat.SetFloat("_Blur", rightBlurAmount);

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


    }

    private void CloseLeftEye()
    {
        //LeftTop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(-200, 112.5f, 0));
        //LeftTop.transform.localPosition = new Vector3(-testvar * Screen.currentResolution.width, 0.25f * Screen.currentResolution.height, 0);
        //LeftBottom.transform.localPosition = new Vector3(-testvar * Screen.currentResolution.width, -0.25f * Screen.currentResolution.height, 0);
        LeftTop.gameObject.SetActive(true);
        LeftBottom.gameObject.SetActive(true);
        leftBlurAmount = 0;
    }
    void CloseRightEye()
    {
        //RightTop.transform.localPosition = new Vector3(testvar * Screen.currentResolution.width, 0.25f * Screen.currentResolution.height, 0);
        //RightBottom.transform.localPosition = new Vector3(testvar * Screen.currentResolution.width, -0.25f * Screen.currentResolution.height, 0);
        RightTop.gameObject.SetActive(true);
        RightBottom.gameObject.SetActive(true);
        rightBlurAmount = 0;
    }
    void OpenLeftEye()
    {
        //LeftTop.transform.localPosition = new Vector3(-testvar * Screen.currentResolution.width, 1f * Screen.currentResolution.height, 0);
        //LeftBottom.transform.localPosition = new Vector3(-testvar * Screen.currentResolution.width, -1f * Screen.currentResolution.height, 0);
        LeftTop.gameObject.SetActive(false);
        LeftBottom.gameObject.SetActive(false);
    }
    void OpenRightEye()
    {
        //RightTop.transform.localPosition = new Vector3(testvar * Screen.currentResolution.width, 1f * Screen.currentResolution.height, 0);
        //RightBottom.transform.localPosition = new Vector3(testvar * Screen.currentResolution.width, -1f * Screen.currentResolution.height, 0);
        RightTop.gameObject.SetActive(false);
        RightBottom.gameObject.SetActive(false);
    }
}
