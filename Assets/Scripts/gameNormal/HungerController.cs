using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerController : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        //transform.localPosition = new Vector3(-50.0f, 0.0f, 0.0f);
        Debug.Log(health); 
        if (transform.localPosition.x >= -100.0f)
        {
            transform.localPosition = new Vector3(health, 0.0f, 0.0f);
        }
        else
        {
            Debug.Log("Dead");
        }
    }


}
