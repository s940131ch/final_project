using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listGenerate : MonoBehaviour
{
    public GameObject MainList;
    // Start is called before the first frame update
    void Start()
    {
        MainList.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openMainList()
    {
        if (MainList != null)
        {
            bool isActive = MainList.activeSelf;
            MainList.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            MainList.SetActive(!isActive);
        }
    }
}
