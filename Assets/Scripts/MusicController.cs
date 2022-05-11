using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{

    public static MusicController instance;

    [SerializeField] public AudioSource ball, lvl,over, can;
    [SerializeField] public AudioClip shoot, next, fail, exp,eb,nm;



    void Start()
	{
	}
    public void PlayShootSound()
    {
        ball.PlayOneShot(shoot);
    }

    public void PlaynxtlvlSound()
    {
        lvl.PlayOneShot(next);
    }

    public void Playfailsound()
    {
        over.PlayOneShot(fail);
    }
    public void PlayExplodeSound()
    {
        can.PlayOneShot(exp);
    }
    public void PlayebSound()
    {
        can.PlayOneShot(eb);
    }
    public void PlaynmSound()
    {
        can.PlayOneShot(nm);
    }
    private void Awake()
    {
        

        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
