using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignupMananger : MonoBehaviour
{

    public InputField ID;
    public InputField Password;
    public GameObject SQLErrorMesage;
    public GameObject LoadingMessage;
    public InputField CheckPassword;
    public GameObject DoubleCheck;
    bool loadingFlag = false;
    float time = 0.0f;
    float angle = 0.0f;
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
    public void Signup()
    {
        StartCoroutine(signup());
    }
    public void Confirm()
    {
        StartCoroutine(realsignup());
    }
    public void Cancel()
    {
        DoubleCheck.SetActive(false);
    }
    public void DestoryMessage()
    {

        SQLErrorMesage.SetActive(false);
    }

    IEnumerator signup()
    {
        WWW www = new WWW("http://203.222.25.240/Check.php?username=" + ID.text + "&password=" + Password.text);
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

        }
        else
        {
          
            if (www.text == "1")
            {
                SQLErrorMesage.SetActive(true);
                SQLErrorMesage.transform.GetChild(0).GetComponent<Text>().text = "帳號已被註冊，臭傻逼";
                Invoke("DestoryMessage", 2f);
               
            }
            else
            {
                DoubleCheck.SetActive(true);
                DoubleCheck.transform.GetChild(2).GetComponent<Text>().text = ID.text;
            }

        }
    }
    IEnumerator realsignup()
    {

        WWW www = new WWW("http://203.222.24.233/Check.php?username=" + ID.text + "&password=" + Password.text);
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

        }
        else
        {
            if (CheckPassword.text != Password.text)
            {
                SQLErrorMesage.SetActive(true);
                SQLErrorMesage.transform.GetChild(0).GetComponent<Text>().text = "密碼錯誤";
                Invoke("DestoryMessage", 2f);

            }
            else
            {
                www = new WWW("http://203.222.24.233/Signup.php?username=" + ID.text + "&password=" + Password.text);
                LoadingMessage.SetActive(true);
                loadingFlag = true;
                yield return www;
                loadingFlag = false;
                LoadingMessage.SetActive(false);

                SQLErrorMesage.SetActive(true);
                SQLErrorMesage.transform.GetChild(0).GetComponent<Text>().text = "帳號成功註冊";
                Invoke("DestoryMessage", 2f);
                DoubleCheck.SetActive(false);
            }
        }
    }
}
