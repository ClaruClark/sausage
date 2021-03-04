using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioClip mainTheme;
    [SerializeField] AudioClip[] sausageSounds;
    [SerializeField] AudioClip[] fxSounds;
    [SerializeField] AudioSource MainSource;
    [SerializeField] AudioSource[] sourcesCube;
    [SerializeField] AudioSource sausage;
    [SerializeField] AudioSource fxSource;

    private void Start()
    {
       // MainSource.Play();
    }

    public void SetCube(int ind)
    {
        if (sourcesCube[ind].isPlaying)
        {
            StopCube(ind);
        }
        //sourcesCube[ind].clip = audioClips[ind];
        sourcesCube[ind].Play();
        
    }

    private void StopCube(int ind)
    {
        sourcesCube[ind].Stop();
    }

    public void SausageSound()
    {
        int rand = Random.Range(0, sausageSounds.Length);
        sausage.PlayOneShot(sausageSounds[rand]);
    }

    public void Effect(int efID)
    {
        fxSource.PlayOneShot(fxSounds[efID]);
    }
}
