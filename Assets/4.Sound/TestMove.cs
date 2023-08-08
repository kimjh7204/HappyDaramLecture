using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{

    private bool isRight = false;

    private float timer;
    private float duration = 2f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isRight)
            transform.Translate(Time.deltaTime * 3f, 0f, 0f);
        else
            transform.Translate(-Time.deltaTime * 3f, 0f, 0f);

        timer += Time.deltaTime;
        if (timer >= duration)
        {
            isRight = !isRight;
            timer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.instance.PlaySound("sound1", transform.position);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SoundManager.instance.PlaySound("sound1", transform);

        }
    }
}
