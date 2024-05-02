using UnityEngine;
using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    public SerializedDictionary<string, AudioSource> soundDictionary = new SerializedDictionary<string, AudioSource>();

    private void Start()
    {
        Play("AmbientMusic");
        Play("Rain");
        
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = s.clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
            source.loop = s.loop;

            soundDictionary.Add(s.name, source);
        }
    }

    public void Play(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            bool _additive = IsSoundAdditive(soundName);
            AudioSource audioSource = soundDictionary[soundName];
            if (!audioSource.isPlaying )
            {
                audioSource.Play();
            }
            else if (audioSource.isPlaying && _additive)
            {
                audioSource.Play();
            }

            // Debug
            if (audioSource.isPlaying)
            {
                Debug.Log("Clip " + soundName + " is playing.");
            }
            else
            {
                Debug.Log("Clip " + soundName + " is not playing.");
            }
        }
        else
        {
            Debug.LogWarning("Sound with name " + soundName + " not found!");
        }
    }

    public void Stop(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            soundDictionary[soundName].Stop();
        }
        else
        {
            Debug.LogWarning("Sound with name " + soundName + " not found!");
        }
    }

    public bool IsSoundAdditive(string soundName)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == soundName && s.additive)
            {
                return true;
            }
            
        }
        return false;
    }

    // Add more functions here as needed, such as Pause, Stop, etc.
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    public bool loop;

    public bool additive = false;
}
