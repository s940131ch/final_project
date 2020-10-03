using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class handleTaskMutli : MonoBehaviourPun
{
	
	int Front = 0;
	int Rear = 0;
	public int MAX = 5;
	public GameObject[] taskQueue = new GameObject[5];
	
	public void pushTask(GameObject obj)
	{

		if (!isFull())
		{
			Rear = (Rear + 1) % MAX;
			taskQueue[Rear] = obj;

		}
		else
		{
			Debug.Log("Task Queue Is Full");
		}
	}
	public void popTask()
	{
		if (!isEmpty())
		{
			taskQueue[Front] = null;
			Front = (Front + 1) % MAX;

		}
		else
		{
			Debug.Log("Task Queue Is Empty");
		}
	}
	public bool isFull()
	{

		Debug.Log("(Rear + 1) % MAX = " + (Rear + 1) % MAX);
		return ((Rear + 1) % MAX == Front);
	}
	public bool isEmpty()
	{

		return Front == Rear;
	}

	public GameObject getFirst()
	{
		Debug.Log(((Front + 1) % MAX) + "front + 1 % MAX");
		return taskQueue[((Front + 1) % MAX)];
	}
	public void clearQueue()
	{
		Front = 0;
		Rear = 0;
		for (int i = 0; i < 5; i++)
		{
			taskQueue[i] = null;
		}
	}
	public int getFront()
	{
		return Front;
	}
	public int getRear()
	{
		return Rear;
	}
}
