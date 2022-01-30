using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject); // game only has 1 scene. no need to dontDestroyOnload
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    public List<AudioSource> audioSources = new List<AudioSource>();


    //[SerializeField]
    //public Dictionary<string, AudioClip> Clips = new Dictionary<string, AudioClip>();

    public List<AudioClip> clips = new List<AudioClip>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySound(string clipname, int channel)
    {
        AudioClip clip = null;
        //clips.TryGetValue(clipname,out clip);

        for (int i = 0; i < clips.Count; i++)
        {
            if (clips[i].name == clipname)
            {
                clip = clips[i];
            }
        }

        if (clip != null)
        {
            if (audioSources[channel].isPlaying)
            {
                audioSources[channel].Stop();
            }

            audioSources[channel].clip = clip;
            audioSources[channel].Play();
        }
    }
    public void PlaySound(string clipname)
    {
        PlaySound(clipname, 0);
    }


}
