using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlinkingScript : MonoBehaviour
{
    public Transform LeftTop, LeftBottom, RightTop, RightBottom;
    public KeyCode blinkLeftKey, blinkRightKey;
    private PassedOutScript passedOutScript;
    public Material _leftBlurMat, _rightBlurMat;
    private void Start()
    {
        passedOutScript = GetComponent<PassedOutScript>();
    }

    private void Update()
    {
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
        LeftTop.transform.position = Camera.main.ViewportToScreenPoint(new Vector3(0.25f, 0.75f, 0));
        LeftBottom.transform.position = Camera.main.ViewportToScreenPoint(new Vector3(0.25f, 0.25f, 0));
    }
    void CloseRightEye()
    {
        RightTop.transform.position = Camera.main.ViewportToScreenPoint(new Vector3(0.75f, 0.75f, 0));
        RightBottom.transform.position = Camera.main.ViewportToScreenPoint(new Vector3(0.75f, 0.25f, 0)); ;
    }
    void OpenLeftEye()
    {
        LeftTop.transform.position = Camera.main.ViewportToScreenPoint(new Vector3(0.25f, 2, 0));
        LeftBottom.transform.position = Camera.main.ViewportToScreenPoint(new Vector3(0.25f, 2, 0));
    }
    void OpenRightEye()
    {
        RightTop.transform.position = Camera.main.ViewportToScreenPoint(new Vector3(0.75f, 2, 0));
        RightBottom.transform.position = Camera.main.ViewportToScreenPoint(new Vector3(0.75f, 2, 0));
    }
}
