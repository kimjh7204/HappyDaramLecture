using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTestSound : MonoBehaviour
{
    public void TestSound(string key)
    {
        SoundManager.instance.PlaySound(key);
    }
}
