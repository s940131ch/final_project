using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets;

public class LoginManager : MonoBehaviour
{

    public InputField ID;
    public InputField Password;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Signin()
    {
        Debug.Log(ID.text);
        SqlAccess sql = new SqlAccess();
        sql.Select(ID.text, Password.text);
        sql.Close();
    }

    public void Signup()
    {
        Debug.Log(ID.text);
        SqlAccess sql = new SqlAccess();
        sql.Insert(ID.text, Password.text);
        sql.Close();
    }
}
