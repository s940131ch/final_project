using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class testPosition : MonoBehaviour
{
    Text position;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            Vector3 Direction = hit.point - Camera.main.transform.position;

            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 0.1f, true);
            Debug.Log(hit.transform.name);
            GameObject newBall = Instantiate<GameObject>(ball);
            newBall.transform.position = Camera.main.transform.position;
            newBall.AddComponent<Rigidbody>();
            newBall.AddComponent<SphereCollider>();
            Rigidbody ballRd = newBall.GetComponent<Rigidbody>();
            ballRd.AddForce(Direction * 100.0f);
        }
    }
}
