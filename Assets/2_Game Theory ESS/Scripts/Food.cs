using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	public int energy = 100;

	private const float Scaler = 0.01f;
	
	public int hawkCount = 0;
	public bool isHawk => hawkCount > 0; //property 프로퍼티 기능
	public bool isHawkMoreThenTwo => hawkCount > 1;
		
	private List<Blob> blobs;
	private List<Blob> blobsRemoveReserved;

	private void Awake()
	{
		blobs = new List<Blob>();
		ESSManager.instance.FoodEatingEvent += EatingFood;
	}
	
	private void EatingFood()
	{
		foreach (var blob in blobs)
		{
			blob.EatFood(isHawkMoreThenTwo);
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
		blobsRemoveReserved = new List<Blob>();
			
		if (blob is BlobHawk)
		{
			hawkCount++;
			foreach (var b in blobs)
			{
				if (b is BlobDove)
					b.ResetFood();
			}
			RemoveBlob();
		}

		if (blob is BlobDove && isHawk)
			return;
			
		blobs.Add(blob);
	}

	public void RemoveBlobReserve(Blob blob)
	{
		if(blobsRemoveReserved == null)
			blobsRemoveReserved = new List<Blob>();
		blobsRemoveReserved.Add(blob);
	}

	private void RemoveBlob()
	{
		foreach (var blob in blobsRemoveReserved)
		{
			blobs.Remove(blob);
		}
	}
}