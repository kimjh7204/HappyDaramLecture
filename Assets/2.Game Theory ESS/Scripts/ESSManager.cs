using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ESSManager : MonoBehaviour
{
    public GameObject blobDove;
    public GameObject blobHawk;
    public GameObject food;

    private List<Transform> allFood;

    public delegate void TickRate();
    public TickRate tickRate;
    
    public event TickRate FoodEatingEvent;

    private const float width = 50f;
    
    public static ESSManager instance;
    
    private void Awake()
    {
        instance = this;
        
        allFood = new List<Transform>();
        StartCoroutine(Timer());
        StartCoroutine(FoodEatingTimer());
    }

    private void Update()
    {
        
    }

    // public Vector3 NearestFood(Vector3 myPos)
    // {
    //     var minDist = float.MaxValue;
    //     var result = Vector3.zero;
    //     
    //     // for (int i = 0; i < allFood.Count; i++)
    //     // {
    //     //     var dist = (myPos - allFood[i].position).magnitude;
    //     //     if (dist < minDist)
    //     //     {
    //     //         minDist = dist;
    //     //         result = allFood[i].position;
    //     //     }
    //     // }
    //
    //     foreach (var foodTransform in allFood)
    //     {
    //         var dist = (myPos - foodTransform.position).magnitude;
    //         if (dist < minDist)
    //         {
    //             minDist = dist;
    //             result = foodTransform.position;
    //         }
    //     }
    //
    //     return result;
    // }
    
    
    private IEnumerator Timer()
    {
        while (true)
        {
            //먹이 생성 코드
            var posX = Random.Range(-width, width);
            var posY = Random.Range(-width, width);

            var foodInstance = Instantiate(food, new Vector3(posX, 0f, posY), Quaternion.identity);
            allFood.Add(foodInstance.transform);

            if (tickRate != null)
                tickRate();

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator FoodEatingTimer()
    {
        while (true)
        {
            if (FoodEatingEvent != null)
                FoodEatingEvent();
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
