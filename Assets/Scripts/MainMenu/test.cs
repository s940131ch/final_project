using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class test : MonoBehaviour
{
    private bool sear = false;
    private bool stat = false;
    public InputField id;
    public InputField password;
    bool flag = false;
    private string text;
    public void Search()
    {
        
        StartCoroutine(IGetData());

    }

    IEnumerator IGetData()
    {
        string ID = id.text;
        string Pass = password.text;
        WWW www = new WWW("http://203.222.24.233:80/search.php?username=" + ID + "&password=" + Pass);
        yield return www;

        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);
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
            }
            else
                sear = false;
        }

        Debug.Log(sear);
        StatusController.setHealth(100.0f);
        StatusController.setWater(100.0f);

    }
}
