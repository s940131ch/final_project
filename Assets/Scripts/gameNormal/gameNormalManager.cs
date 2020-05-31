
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameNormalManager : MonoBehaviour
{
    public GameObject pet1;
    public GameObject pet2;

    // Start is called before the first frame update
    void Start()
    {
        if (StatusController.getPetType() == 1)
            Instantiate<GameObject>(pet1,new Vector3(-0.099f, 0.12f, -0.13f),new Quaternion(0.0f,0.0f,0.0f,0.0f));
        else if(StatusController.getPetType() == 2)
            Instantiate<GameObject>(pet2, new Vector3(-0.099f, 0.12f, -0.13f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    public void onClick(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
