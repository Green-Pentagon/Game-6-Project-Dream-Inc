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
            if (employee != null)
            {
                EmployeeBehaviourScript temp = employee.GetComponent<EmployeeBehaviourScript>();
                temp.WakeUp();
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
