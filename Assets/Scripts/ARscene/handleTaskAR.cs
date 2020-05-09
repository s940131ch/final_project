using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleTaskAR : MonoBehaviour
{

	public int Front = 0;
	public int Rear = 0;
	public int MAX = 5;
	public GameObject[] taskQueue;

	void Start()
	{
		taskQueue = new GameObject[MAX];
	}
	void Update()
	{
		Debug.Log("Rear" + Rear);
		Debug.Log("Front" + Front);
	}
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


}
