using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    float speed = 5.0f;
    float y = 0.0f;
    float direction = 0.0f;     //朝向前方
    float timeOfDirection = 0;
    float timeOfWalking = 0;
    bool isOk = false;
    Animator am;
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        am.SetBool("isWalk", false);
        
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDirection += Time.deltaTime;
        if (timeOfDirection >= 3.0f)
        {
            direction = Random.Range(0.0f,360.0f);
            transform.eulerAngles = new Vector3(0.0f, direction, 0.0f);
            isOk = true;
            timeOfWalking = 1.5f;
            timeOfDirection = 0.0f;
        }
        if(isOk && timeOfWalking > 0)
        {
            transform.position += transform.forward * Time.deltaTime;
            am.SetBool("isWalk", true);
            timeOfWalking -= Time.deltaTime;
        }
        else
        {
            am.SetBool("isWalk", false);
        }

    }

}
