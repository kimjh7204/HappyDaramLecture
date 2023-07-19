using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	public int energy = 100;

	private const float Scaler = 0.01f;
	
	private List<Blob> blobs;

	private void Awake()
	{
		blobs = new List<Blob>();
		ESSManager.instance.FoodEatingEvent += FoodConsum;
	}
	
	private void FoodConsum()
	{
		foreach (var blob in blobs)
		{
			blob.EatFood();
			energy--;

			if (energy <= 0)
			{
				blob.FinishEating();
				ESSManager.instance.FoodEatingEvent -= FoodConsum;
				Destroy(this);
				return;
			}
			
			transform.localScale = Vector3.one * energy * Scaler;
		}
	}

	public void BlobRegistration(Blob blob)
	{
		blobs.Add(blob);
	}
}
