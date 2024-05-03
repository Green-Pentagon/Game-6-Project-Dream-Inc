using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GlobalPingSystem : MonoBehaviour
{
    private void WakeAllEmployees()
    {
        GameObject[] employees = GameObject.FindGameObjectsWithTag("Employee");
        foreach (GameObject employee in employees)
        {
            if (employee != null)// in the event that objects were incorrectly tagged as employee, the objects are skipped.
            {
                EmployeeBehaviourScript temp = employee.GetComponent<EmployeeBehaviourScript>();
                temp.WakeUp();
                temp.WindowWasBroken();
            }
            
        }
    }

    public void WindowBroken(int repairCashPenalty)
    {
        WakeAllEmployees();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviourScript>().SubtractCash(repairCashPenalty);
    }
    
    public void EmployeeAlterCashGain(float factor)
    {
         GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviourScript>().AlterCashMultiplier(factor);
    }

    public void BossDeskChangeState(bool state)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviourScript>().BossDeskState(state);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
