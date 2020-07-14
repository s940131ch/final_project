using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuMananger : MonoBehaviour
{
    public GameObject Panel;
    public InputField id;
    public InputField password;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void onClick(string sceneName)
    {
        if(StatusController.getHasPet() == 0)
        {
            Panel.SetActive(true);
        }
        else
            SceneManager.LoadScene(sceneName);
    }
    public void clickPet1()
    {
        StatusController.setHasPet(1);
        StatusController.setPetType(1);
        Panel.SetActive(false);
        StartCoroutine(UpdatePetData(id.text, password.text, 1, 1));
    }

    public void clickPet2()
    {
        StatusController.setHasPet(1);
        StatusController.setPetType(2);
        Panel.SetActive(false);
        StartCoroutine(UpdatePetData(id.text, password.text, 1, 2));
    }

    IEnumerator<WWW> UpdatePetData(string ID, string Pass, int HasPet, int PetType)
    {
        WWW www = new WWW("http://203.222.25.240/Update.php?Username=" + ID + "&Password=" + Pass + "&HasPet=" + HasPet + "&PetType=" + PetType);
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
