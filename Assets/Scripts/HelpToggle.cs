using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpToggle : MonoBehaviour
{
    private bool active = true;
    public void ToggleToolTips()
    {
        active = !active;
        gameObject.SetActive(active);
    }

    private void Start()
    {
        gameObject.SetActive(active);
    }
}
