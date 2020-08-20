
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameNormalManager : MonoBehaviour
{
    public GameObject pet1;
    public GameObject pet2;
    float time = 0.0f;
    public string server_ip = "203.222.25.154";
    // Start is called before the first frame update
    void Start()
    {
        if (StatusController.getPetType() == 1)
            Instantiate<GameObject>(pet1, new Vector3(-0.099f, 0.12f, -0.13f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        else if (StatusController.getPetType() == 2)
            Instantiate<GameObject>(pet2, new Vector3(-0.099f, 0.12f, -0.13f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuClick();
        }
        if (time >= 30.0f)
        {
            StartCoroutine(saveData());
            Debug.Log("記得歸零 臭傻逼");
            time = 0.0f;
        }

    }

    public void onClick(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void menuClick()
    {
        StartCoroutine(backMenu());
    }
    public void Save()
    {
        StartCoroutine(saveData());
    }
    IEnumerator backMenu()
    {

        WWW www = new WWW("http://" + server_ip + "/UpdateStatus.php?Username=" + StatusController.getUsername() + "&Password=" + StatusController.getPassword() + "&Hunger=" + StatusController.getHealth() + "&Thirst=" + StatusController.getWater() + "&Love=" + StatusController.getLove());
        yield return www;
        
        Debug.Log("儲存資料成功1");
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator saveData()
    {
        
        WWW www = new WWW("http://" + server_ip + "/UpdateStatus.php?Username=" + StatusController.getUsername() + "&Password=" + StatusController.getPassword() + "&Hunger=" + StatusController.getHealth() + "&Thirst=" + StatusController.getWater() + "&Love=" + StatusController.getLove());
        yield return www;
        Debug.Log("儲存資料成功2");
    }
}
