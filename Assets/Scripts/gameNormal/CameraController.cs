using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    public float near = 20.0f;
    public float far = 100.0f;

    public float sensitivetyZ = 2f;
    public float sensitivityX = 0.1f;
  
    public float sensitivityY = 10f;
    public float sensitivetyMove = 2f;
    public float sensitivetyMouseWheel = 2f;

    float rotateH;
    float transH;
    float rotateV;
    float transVx;
    float transVy;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        rotateH = Input.GetAxis("Horizontal") * 30.0f;
        transH = Input.GetAxis("Horizontal") * -20.0f;

        if (Input.GetAxis("Vertical") >= 0.0f)
        {
            rotateV = (1 - Input.GetAxis("Vertical")) * 35.0f;
            transVx = 9.0f * (1 - Input.GetAxis("Vertical"));
            transVy = -15.0f *(1 - Input.GetAxis("Vertical"));
        }
        
        transform.position = new Vector3(transH, transVx, transVy);
        transform.rotation = Quaternion.Euler(rotateV, rotateH, 0.0f);
        
    }
}
