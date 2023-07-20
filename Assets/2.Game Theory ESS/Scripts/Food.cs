using System;
using System.Collections;
using System.Collections.Generic;
using _2.Game_Theory_ESS.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class Food : MonoBehaviour
{
	public int energy = 100;

	private const float Scaler = 0.01f;
	
	//public int hawkCount = 0;
	//bool flag 대신에 사용하세요.

	private List<Blob> blobs;

	private void Awake()
	{
		blobs = new List<Blob>();
		ESSManager.instance.FoodEatingEvent += EatingFood;
	}
	
	private void EatingFood()
	{
		foreach (var blob in blobs)
		{
			blob.EatFood();
			energy--;

			if (energy <= 0)
			{
				blob.FinishEating();
				ESSManager.instance.FoodEatingEvent -= EatingFood;
				Destroy(gameObject);
				return;
			}
			
			transform.localScale = Vector3.one * energy * Scaler;
		}
	}

	public void BlobRegistration(Blob blob)
	{
		//타입 검사 방법
		//if (blob.GetType() == typeof(BlobHawk))
		//또는
		//if (blob is BlobHawk)
		blobs.Add(blob);
	}
}
