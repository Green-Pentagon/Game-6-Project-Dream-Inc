using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeBehaviourScript : MonoBehaviour
{
    private GlobalPingSystem GlobalPingSys;

    private bool asleep = false;
    private float timeTillSleep;
    private float currentTime;
    private float CashGainFactor = 1.0f; //how much gain/loss player incurs depending on employee state.

    private SpriteRenderer debugSR;

    private float getNewTime()
    {
        return Random.Range(5.0f, 10.0f);
    }

    private void resetTimer()
    {
        currentTime = 0.0f;
        timeTillSleep = getNewTime();
    }


    public void WakeUp()
    {
        if (asleep)
        {
            asleep = false;
            resetTimer();
            GlobalPingSys.EmployeeAlterCashGain(CashGainFactor*2);
            debugSR.color = Color.white;
        }
    }

    private void FallAsleep()
    {
        asleep = true;
        GlobalPingSys.EmployeeAlterCashGain(-CashGainFactor*2);
        debugSR.color = Color.blue;
    }


    // Start is called before the first frame update
    void Start()
    {
        GlobalPingSys = GameObject.FindGameObjectWithTag("GlobalPing").GetComponent<GlobalPingSystem>();
        resetTimer();
        debugSR = GetComponent<SpriteRenderer>();
        GlobalPingSys.EmployeeAlterCashGain(CashGainFactor);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= timeTillSleep && !asleep)
        {
            FallAsleep();
        }
    }
}
