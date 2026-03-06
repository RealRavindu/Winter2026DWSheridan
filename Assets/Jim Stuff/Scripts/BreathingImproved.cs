using UnityEngine;
using UnityEngine.UI;
public class BreathingImproved : MonoBehaviour
{
    [SerializeField] KeyCode pulseHeart;

    private LubDubScript LubDubScript;
    private float heartRate;
    private float heartRedline;
    private float heartStall;

    public float airCapacity = 0; //amount of air in the lungs
    public float airMaxCapacity = 100; //maximum capacity of lungs
    private float airFlow; //base intake of lungs
    private float airFlowModified; //intake of lungs modified by heart rate
    private float breathTimer;
    [SerializeField] float airTimeToMaxBase = 3;
    public AnimationCurve airFlowCurve;
    public AnimationCurve breathScaleCurve;

   
    public float oxygenCapacity; //amount of oxygen the player has
    private float oxygenDecay;
    private float oxygenModifiedDecay;
    private float oxygenMaxCapacity;
    [SerializeField] float oxygenConverstionRatio = 3f;
    [SerializeField] float oxygenDecayTime = 10f;
    public AnimationCurve oxygenDecayCurve;
    public float decayOffset = 2;



    [SerializeField] float faintTimer = 10;
    [SerializeField] float faintThreshold = 40;
    private PassedOutScript PassedOutScript;

    [SerializeField] Slider oxygenBar;
    [SerializeField] Gradient barColor;

    private void Start()
    {
        LubDubScript = GetComponent<LubDubScript>();
        if (LubDubScript == null)
        {
            Debug.Log("Error, no LubDub script found!"); return;
        }
        
        PassedOutScript = GetComponent<PassedOutScript>();
        if (PassedOutScript == null)
        {
            Debug.Log("Error, no PassedOut script found!"); return;
        }
        SetRates();
    }

    private void Update()
    {
        UpdateRates();

        if (Input.GetKey(pulseHeart))
        {
            BreathIn();
            //Debug.Log("Breathing in!");
        }
        else
        {
            BreathOut();
            //Debug.Log("Breathing Out!");
        }
        Idle();
        Faint();
        UpdateOxygenBar();
    }

    private void SetRates()
    {
        heartRedline = LubDubScript.redlineLimit;
        heartStall = LubDubScript.stallLimit;

        airFlow = airMaxCapacity / airTimeToMaxBase;

        oxygenMaxCapacity = airMaxCapacity * oxygenConverstionRatio;
        //use curve to scale heart rate limits to smaller value to alter decay time
        oxygenDecay = oxygenMaxCapacity / oxygenDecayTime;
    }

    private void UpdateRates()
    {
        heartRate = LubDubScript.heartRate;
        //Debug.Log($"Modified airFlow {ModifyFlowValue()}, decay {ModifyDecayValue()}");
        airFlowModified =  airFlow * ModifyFlowValue();
        oxygenModifiedDecay = oxygenDecay * ModifyDecayValue();
    }

    private void Idle()
    {
        oxygenCapacity -= oxygenModifiedDecay * Time.deltaTime;
        airCapacity = Mathf.Clamp(airCapacity, 0, airMaxCapacity);
        oxygenCapacity = Mathf.Clamp(oxygenCapacity, 0, oxygenMaxCapacity);
    }

    private void BreathIn()
    {
        airCapacity += airFlowModified * ModifyBreathValue() * Time.deltaTime;

        if (airCapacity < airMaxCapacity)
        {
            oxygenCapacity += (airFlowModified + (decayOffset * oxygenDecay)) * ModifyBreathValue() * Time.deltaTime;
            //Debug.Log("Increasing oxygen!");
        }
    }

    private void BreathOut()
    {
        breathTimer = 0;
        airCapacity -= airFlowModified * Time.deltaTime;
    }

    private float ModifyDecayValue()
    {
        float heartRateModifier = Mathf.InverseLerp(heartStall, heartRedline, heartRate);
        //Debug.Log($"hear rate modifier {heartRateModifier}");
        float modifiedDecay = oxygenDecayCurve.Evaluate(heartRateModifier);
        return modifiedDecay;
    }
    private float ModifyFlowValue()
    {
        float heartRateModifier = Mathf.InverseLerp(heartStall, heartRedline, heartRate);
        float modifiedFlow = airFlowCurve.Evaluate(heartRateModifier);
        return modifiedFlow;
    }
    private float ModifyBreathValue()
    {
        float timeToMaxLungCapacity = airMaxCapacity / airFlowModified;
       // Debug.Log($"breathing time {timeToMaxLungCapacity}");
        breathTimer += Time.deltaTime;
        float breathModifier = Mathf.InverseLerp(0, timeToMaxLungCapacity, breathTimer);
        float modifiedBreath = breathScaleCurve.Evaluate(breathModifier);
       // Debug.Log($"breathing modifier {modifiedBreath}");
        return modifiedBreath;
    }

    private void Faint()
    {
        //print("FAAAAAAAAAAAAAAAAAAAINT");
        if (faintTimer > 10) faintTimer = 10;
        if (!PassedOutScript.value)
        {
            if (oxygenCapacity < faintThreshold)
            {
                faintTimer -= Time.deltaTime;
            }
            else
            {
                faintTimer += Time.deltaTime;
            }
        } else
        {
            faintTimer = 10;
        }


        if (faintTimer < 0)
        {
           // Debug.Log("Player has feinted from lack of oxygen");
            faintTimer = 10;
            //Passout script here
            PassedOutScript.PassOut();
        }
    }

    private void UpdateOxygenBar()
    {
       // print("AAAAAAAAAAAAAAAAAAA");
        oxygenBar.value = airCapacity / airMaxCapacity;

        Image bar = oxygenBar.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        bar.color = barColor.Evaluate((10 - faintTimer) / 10);
    }
}