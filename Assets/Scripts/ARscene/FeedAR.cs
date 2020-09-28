using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedAR : MonoBehaviour
{

    public GameObject food;
    public GameObject water;
    
    bool hasCreateFood = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void createFood()
    {
        if (!hasCreateFood)
        { 
            handletaskAr.pushTask(food);
        }

    }

    public void foundFood()
    {
        hasCreateFood = true;
    }
    public void notFoundFood()
    {     
        hasCreateFood = false;
    }
}
