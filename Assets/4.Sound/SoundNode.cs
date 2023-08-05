using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundNode : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.PlayOneShot(clip);

        StartCoroutine(WaitForSoundEnd());
    }


    private IEnumerator WaitForSoundEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        //lamda expression + 익명 메서드

        SoundManager.instance.EnqueueNode(this);
    }

    //private bool SoundIsPlaying() => audioSource.isPlaying;
    //lamda expression
}
