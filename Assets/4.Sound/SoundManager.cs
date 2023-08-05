using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;    //Singleton Pattern
                                            //Programming Design Pattern


    public GameObject soundNodePrefab;
    public List<SoundInfo> soundInfos;

    private Dictionary<string, AudioClip> soundsDictionary = new Dictionary<string, AudioClip>();

    private Queue<SoundNode> soundsPool = new Queue<SoundNode>();
    private const int poolSize = 50;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < poolSize; i++) 
        {
            var go = Instantiate(soundNodePrefab, transform);
            soundsPool.Enqueue(go.GetComponent<SoundNode>());
        }

        foreach(var soundInfo in soundInfos)
        {
            soundsDictionary.Add(soundInfo.key, soundInfo.clip);
        }    
    }

    public void PlaySound(string key)
    {
        if (!soundsDictionary.ContainsKey(key))
        {
            Debug.LogError("SFX is not found : " + key);
            return;
        }

        var node = soundsPool.Dequeue();
        node.PlaySound(soundsDictionary[key]);
    }

    public void EnqueueNode(SoundNode node)
    {
        soundsPool.Enqueue(node);
        Debug.Log("Enqueue", node.gameObject);
    }
}

[Serializable]
public class SoundInfo
{
    public string key;
    public AudioClip clip;
}