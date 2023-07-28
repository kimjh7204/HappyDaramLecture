using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlobHawk : Blob
{
	protected override void StateEnter()
	{
		switch (curState)
		{
			case BlobState.Idle:
				break;
			case BlobState.Wandering:
				moveTargetPos = transform.position; //타겟 포지션 초기화
				ESSManager.instance.tickRate += FoodFinding;
				break;
			case BlobState.FoodTracing:
				if (foundFood == null) break;
				moveTargetPos = foundFood.transform.position;
				agent.SetDestination(moveTargetPos);
				break;
			case BlobState.Eating:
				agent.velocity = Vector3.zero;
				agent.isStopped = true;
				foundFood?.BlobRegistration(this);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	protected override void StateUpdate()
	{
		switch (curState)
		{
			case BlobState.Idle:
				break;
			case BlobState.Wandering:

				if (Vector3.Distance(transform.position, moveTargetPos) < 0.1f)
				{
					var randomCircle = Random.insideUnitCircle;
					moveTargetPos =
						transform.position +
						new Vector3(randomCircle.x, 0f, randomCircle.y) * WanderingRange;

					var width = ESSManager.width - 2f;
					if (moveTargetPos.x > width)
						moveTargetPos.x = width;
					if (moveTargetPos.x < -width)
						moveTargetPos.x = -width;
					if (moveTargetPos.z > width)
						moveTargetPos.z = width;
					if (moveTargetPos.z < -width)
						moveTargetPos.z = -width;

					agent.SetDestination(moveTargetPos);
				}

				break;
			case BlobState.FoodTracing:
				break;
			case BlobState.Eating:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	protected override void StateExit()
	{
		switch (curState)
		{
			case BlobState.Idle:
				break;
			case BlobState.Wandering:
				ESSManager.instance.tickRate -= FoodFinding;
				break;
			case BlobState.FoodTracing:
				break;
			case BlobState.Eating:
				agent.isStopped = false;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	//------------------------------------------------------------------

	protected override bool TransitionCheck()
	{
		switch (curState)
		{
			case BlobState.Idle:
				nextState = BlobState.Wandering;
				return true;
			case BlobState.Wandering:
				if (foundFood != null)
				{
					nextState = BlobState.FoodTracing;
					return true;
				}

				break;
			case BlobState.FoodTracing:
				if (Vector3.Distance(transform.position, moveTargetPos) < 1f)
				{
					nextState = BlobState.Eating;
					return true;
				}

				break;
			case BlobState.Eating:
				if (foundFood == null)
				{
					nextState = BlobState.Idle;
					return true;
				}

				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		return false;
	}

	private void OnDestroy()
	{
		base.OnDestroy();
		ESSManager.instance.tickRate -= FoodFinding;
		ESSManager.instance.curHawkCount--;
	}
}