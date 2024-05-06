using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeBehaviourScript : MonoBehaviour
{
    private bool isBossDesk = false;

    private GlobalPingSystem GlobalPingSys;

    private bool asleep = false;
    private float timeTillSleep;
    private float currentTime;
    private float CashGainFactor = 1.0f; //how much gain/loss player incurs depending on employee state.

    public SpriteRenderer Sprite;
    public Sprite[] States; //0 - active, 1 - asleep

    private float getNewTime()
    {
        if (isBossDesk)
        {
            return Random.Range(10.0f, 15.0f);
        }
        else
        {
            return Random.Range(5.0f, 10.0f);
        }
       
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
            if (isBossDesk)
            {
                GlobalPingSys.BossDeskChangeState(!asleep);
            }
            else
            {
                GlobalPingSys.EmployeeAlterCashGain(CashGainFactor * 2);  
            }
            resetTimer();
            Sprite.sprite = States[0];

        }
    }

    public void WindowWasBroken()
    {
        resetTimer();
    }

    private void FallAsleep()
    {
        asleep = true;
        if (!isBossDesk)
        {
            GlobalPingSys.EmployeeAlterCashGain(-CashGainFactor * 2);
        }
        else
        {
            GlobalPingSys.BossDeskChangeState(!asleep);
            CashGainFactor = 0.0f;
        }
        
        Sprite.sprite = States[1];

    }


    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Desk")
        {
            isBossDesk = true;
        }
        GlobalPingSys = GameObject.FindGameObjectWithTag("GlobalPing").GetComponent<GlobalPingSystem>();
        resetTimer();
        GlobalPingSys.EmployeeAlterCashGain(CashGainFactor);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;

        if (currentTime >= timeTillSleep && !asleep)
        {
            FallAsleep();
        }
        if (!asleep && !isBossDesk)
        {
            transform.Rotate((Vector3.forward) * (2.0f / (currentTime / timeTillSleep)));
        }
        
    }
}
