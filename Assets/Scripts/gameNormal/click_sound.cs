using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click_sound : MonoBehaviour
{
    AudioSource sound;

    public void onClick()
    {
        sound.Play();
    }
}
