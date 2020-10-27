using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip clickSound, menuSound, hitSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        clickSound = Resources.Load<AudioClip>("click_1");
        menuSound = Resources.Load<AudioClip>("menu_1");
        hitSound = Resources.Load<AudioClip>("hit_1");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string sound)
    {
        switch (sound)
        {
            case "click_1":
                audioSrc.PlayOneShot(clickSound);
                break;
            case "hit_1":
                audioSrc.PlayOneShot(hitSound);
                break;
            case "menu_1":
                audioSrc.PlayOneShot(menuSound);
                break;
        }
    }
}
