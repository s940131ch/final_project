using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class SigninMananger : MonoBehaviour
{
    private bool sear = false;
    private bool stat = false;
    public InputField id;
    public InputField password;
    bool flag = false;
    private string text;
    public GameObject ip;
    public GameObject SQLErrorMesage;
    public GameObject LoadingMessage;
    float time=0.0f;
    float angle = 0.0f;
    bool loadingFlag = false;
    public AudioSource sound;
    public string server_ip = "203.222.24.37";
    void Update()
    {

        if (loadingFlag)
        {
            Debug.Log("abc");
            time += Time.deltaTime;
            if (time >= 0.3f)
            {
                time = 0.0f;
                angle += 45.0f;
                if (angle > 180.0f)
                    angle = 0.0f;
                LoadingMessage.transform.GetChild(0).GetComponent<RectTransform>().Rotate(new Vector3(0.0f, 0.0f, angle));
            }
        }
    }
    public void Search()
    {
        sound.Play();
        StartCoroutine(IGetData());
        
    }

    public void DestoryMessage()
    {
        
        SQLErrorMesage.SetActive(false);
    }

   

    IEnumerator IGetData()
    {
        string ID = id.text;
        string Pass = password.text;
        WWW www = new WWW("http://" + server_ip + "/search.php?username=" + ID + "&password=" + Pass);
        LoadingMessage.SetActive(true);
        loadingFlag = true;
        yield return www;
        loadingFlag = false;
        LoadingMessage.SetActive(false);

        if (www.error != null)
        {
            SQLErrorMesage.SetActive(true);
            SQLErrorMesage.transform.GetChild(0).GetComponent<Text>().text = "伺服器關機中";
            Invoke("DestoryMessage", 2f);
            Debug.Log(www.error);   
        }
        else
        {
            Debug.Log("www message:" + www.text);
            if (www.text.Length > 1)
            {
                sear = true;
                text = www.text;
                string[] value = text.Split(',');
                Debug.Log("hunger :" + value[0]);
                Debug.Log("thir :" + value[1]);
                Debug.Log("love :" + value[2]);

                Debug.Log("float hun: " + float.Parse(value[0]));
                Debug.Log("float thir: " + float.Parse(value[1]));
                Debug.Log("float love: " + float.Parse(value[2]));

                StatusController.setHealth(float.Parse(value[0]));
                StatusController.setWater(float.Parse(value[1]));
                StatusController.setLove(float.Parse(value[2]));
                StatusController.setHasPet(int.Parse(value[3]));
                StatusController.setPetType(int.Parse(value[4]));
                StatusController.setUsername(ID);
                StatusController.setPassword(Pass);
                Destroy(ip);
                Debug.Log("hunger :" + value[0]);
                Debug.Log("thir :" + value[1]);
                Debug.Log("love :" + value[2]);
                Debug.Log("float hun: " + float.Parse(value[0]));
                Debug.Log("float thir: " + float.Parse(value[1]));
                Debug.Log("float love: " + float.Parse(value[2]));
            }
            else
            {
                sear = false;
                SQLErrorMesage.SetActive(true);
                Invoke("DestoryMessage", 2f);
                
            }
                
        }

        Debug.Log(sear);
    }

}
