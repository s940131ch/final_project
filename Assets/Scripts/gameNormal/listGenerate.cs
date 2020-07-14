using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listGenerate : MonoBehaviour
{
    public GameObject MainList;
    GameObject Feed;
    GameObject Toy;
    GameObject FeedSubList;
    GameObject ToySubList;

    // Start is called before the first frame update
    void Start()
    {
        
        Feed = MainList.transform.GetChild(0).gameObject;
        Toy = MainList.transform.GetChild(1).gameObject;
        FeedSubList = Feed.transform.GetChild(0).gameObject;
        ToySubList = Toy.transform.GetChild(0).gameObject;
        MainList.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openMainList()
    {
        if (MainList != null)
        {
            bool isActive = MainList.activeSelf;
            Feed.transform.GetChild(0).gameObject.SetActive(false);
            Toy.transform.GetChild(0).gameObject.SetActive(false);
            MainList.SetActive(!isActive);
        }
    }
    public void openFeedList()
    {
        if (FeedSubList != null)
        {
            bool isActive = FeedSubList.activeSelf;
            FeedSubList.SetActive(!isActive);
        }
    }
    public void openToyList()
    {
        if (ToySubList != null)
        {
            bool isActive = ToySubList.activeSelf;
            ToySubList.SetActive(!isActive);
        }
        
    }
}
