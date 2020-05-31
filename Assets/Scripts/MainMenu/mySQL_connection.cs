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
    IEnumerator<WWW> UpdatePetData(string ID, string Pass, int HasPet, int PetType)
    {
        WWW www = new WWW("http://203.222.24.233/Update.php?Username=" + ID + "&Password=" + Pass + "&HasPet=" + HasPet + "&PetType=" + PetType);
        yield return www;

        if (www.error != null)
        {
            Debug.Log(www.error);   
        }
        else
        {
            Debug.Log("www message:" + www.text);
            if (www.text.Length == 1)
            {
                Debug.Log("成功更新");
            }
            else
            {
                Debug.Log("更新失敗");
            }
        }
    }

}
   

