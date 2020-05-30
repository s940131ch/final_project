using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class mySQL_connection : MonoBehaviour
{

    public void search()
    {
            StartCoroutine(IGetData());
    }

    IEnumerator IGetData()
    {
        WWW www = new WWW("http://203.222.24.233:80/search.php?username=abc&password=abc");
        yield return www;

        if(www.error == null)
        {
            Debug.Log("登入失敗");
            yield return null;
        }
        else
        {
            Debug.Log("登入成功");
            yield return null;
        }
    }

}
   

