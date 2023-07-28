using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ESSManager : MonoBehaviour
{
	public GameObject blobDove;
	public GameObject blobHawk;
	public GameObject food;
	
	private List<Transform> allFood;

	public int curDoveCount;
	public int curHawkCount;
	
	private int numOfDove = 1;
	private int numOfHawk = 1;

	public delegate void TickRate();
	public TickRate tickRate;
    
	public event TickRate FoodEatingEvent;
	public event TickRate FoodComsumeEvent;

	public const float width = 48f;
    
	public static ESSManager instance;
    
	private void Awake()
	{
		instance = this;
        
		allFood = new List<Transform>();
	}

	
	//Slider---------------------------------------
	public void SetNumOfDove(float count)
	{
		numOfDove = (int)count;
	}
	
	public void SetNumOfHawk(float count)
	{
		numOfHawk = (int)count;
	}

	public void SetTimeScale(float value)
	{
		Time.timeScale = value;
	}
	//Slider---------------------------------------
	
	public void StartSimulation()
	{
		for (var i = 0; i < numOfDove; i++)
		{
			Instantiate(blobDove, GenerateRamdomPosition(), Quaternion.identity);
			curDoveCount++;
		}
		
		for (var i = 0; i < numOfHawk; i++)
		{
			Instantiate(blobHawk, GenerateRamdomPosition(), Quaternion.identity);
			curHawkCount++;
		}

		StartCoroutine(Timer());
		StartCoroutine(FoodEatingTimer());
	}

	private Vector3 GenerateRamdomPosition()
	{
		var posX = Random.Range(-width, width);
		var posY = Random.Range(-width, width);

		return new Vector3(posX, 0f, posY);
	}
	
	private IEnumerator Timer()
	{
		while (true)
		{
			//var foodInstance = Instantiate(food, new Vector3(posX, 0f, posY), Quaternion.identity);
			var foodInstance = Instantiate(food, GenerateRamdomPosition(), Quaternion.identity);
			allFood.Add(foodInstance.transform);

			if (tickRate != null)
				tickRate();
            
			if(FoodComsumeEvent != null)
				FoodComsumeEvent();
            
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