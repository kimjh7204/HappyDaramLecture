using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum BlobState
{
	Idle,
	Wandering,
	FoodTracing,
	Eating
}

public abstract class Blob : MonoBehaviour
{
	protected NavMeshAgent agent;

	protected Food foundFood;

	protected int energy = 50;

	private const float Scaler = 0.01f;
	private const float MinimumScale = 0.5f;

	protected BlobState curState = BlobState.Idle;
	protected BlobState nextState = BlobState.Idle;

	protected Vector3 moveTargetPos;
	protected const float WanderingRange = 10f;

	private bool isStateChanged = true;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		ESSManager.instance.FoodComsumeEvent += FoodConsume;
	}

	void Update()
	{
		transform.localScale = Vector3.one * (energy * Scaler + MinimumScale);
		if (energy >= 100)
			Duplication();

		curState = nextState;

		if (isStateChanged) StateEnter();
		isStateChanged = false;

		StateUpdate();

		isStateChanged = TransitionCheck();

		if (isStateChanged) StateExit();
	}

	protected abstract void StateEnter();
	protected abstract void StateUpdate();

	protected abstract void StateExit();

	//------------------------------------------------------------------
	protected abstract bool TransitionCheck();

	protected void FoodFinding()
	{
		RaycastHit[] hits = Physics.SphereCastAll(
			transform.position,
			WanderingRange,
			Vector3.up,
			0f, 1 << 3);

		if (hits.Length > 0)
		{
			if (this is BlobDove)
			{
				foreach (var hit in hits)
				{
					var foundFoodTemp = hit.transform.GetComponent<Food>();

					if (!foundFoodTemp.isHawk)
					{
						foundFood = foundFoodTemp;
						break;
					}
				}
			}
			else if (this is BlobHawk)
			{
				foundFood = hits[0].transform.GetComponent<Food>();
			}
		}
		else
			foundFood = null;
	}

	public void EatFood(bool cancel)
	{
		if (!cancel) energy++;
	}

	public void FinishEating()
	{
		foundFood = null;
	}

	private void FoodConsume()
	{
		energy -= 1;
		if (energy <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void Duplication()
	{
		energy -= 50;

		var randomAngle = Random.value % (Mathf.PI * 2f);
		var posX = Mathf.Sin(randomAngle);
		var posY = Mathf.Cos(randomAngle);

		var newPos = new Vector3(posX, 0f, posY) * WanderingRange;

		if (this.GetType() == typeof(BlobDove)) //if(this is BlobDove)
		{
			Instantiate(ESSManager.instance.blobDove,
				transform.position + newPos,
				Quaternion.identity);
			ESSManager.instance.curDoveCount++;
		}

		if (this.GetType() == typeof(BlobHawk)) //if(this is BlobHawk)
		{
			Instantiate(ESSManager.instance.blobHawk,
				transform.position + newPos,
				Quaternion.identity);
			ESSManager.instance.curHawkCount++;
		}
	}

	public void ResetFood()
	{
		//Debug.Log(foundFood != null);
		if (foundFood != null) foundFood.RemoveBlobReserve(this);
		foundFood = null;
	}

	protected void OnDestroy()
	{
		ESSManager.instance.FoodComsumeEvent -= FoodConsume;
	}
}