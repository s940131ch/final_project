using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageContorl : MonoBehaviour
{

    float timer = 0.0f;
    float time = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(Camera.main.transform);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        this.GetComponent<Image>().color = new Color(255, 255, 255, 1 - (timer / 5));
        Destroy(gameObject, time);
    }
    
}
