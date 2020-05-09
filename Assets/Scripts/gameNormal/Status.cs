using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public Image healthBar;
    public Image waterBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = healthBar.GetComponent<Image>();
        waterBar = waterBar.GetComponent<Image>();
    }
    void Update()
    {
        healthBar.fillAmount = StatusController.getHealth() / 100.0f;
        waterBar.fillAmount = StatusController.getWater() / 100.0f;
    }

}

   
