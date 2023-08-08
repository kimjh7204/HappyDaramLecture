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
        PlaySoundNode(key);
    }

    public void PlaySound(string key, Vector3 pos)
    {
        var nodeTransform = PlaySoundNode(key);
        nodeTransform.position = pos;
    }

    public void PlaySound(string key, Transform parent)
    {
        var nodeTransform = PlaySoundNode(key);
        nodeTransform.SetParent(parent);
        nodeTransform.localPosition = Vector3.zero;
    }

    private Transform PlaySoundNode(string key)
    {
        if (!soundsDictionary.ContainsKey(key))
        {
            Debug.LogError("SFX is not found : " + key);
            return null;
        }

        var node = soundsPool.Dequeue();
        node.PlaySound(soundsDictionary[key]);
        return node.transform;
    }

    public void EnqueueNode(SoundNode node)
    {
        node.transform.SetParent(transform);
        node.transform.localPosition = Vector3.zero;
        soundsPool.Enqueue(node);
        //Debug.Log("Enqueue", node.gameObject);
    }
}

[Serializable]
public class SoundInfo
{
    public string key;
    public AudioClip clip;
}