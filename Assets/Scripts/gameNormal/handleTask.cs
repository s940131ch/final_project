using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class handleTask
{
	static int Front = 0;
	static int  Rear = 0;
	public static int MAX = 5;
	public static GameObject[] taskQueue = new GameObject[MAX];
	public static void pushTask(GameObject obj)
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
	public static void popTask()
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
	public static bool isFull()
	{

		Debug.Log("(Rear + 1) % MAX = " + (Rear + 1) % MAX);
		return ((Rear + 1) % MAX == Front);
	}
	public static bool isEmpty()
	{

		return Front == Rear;
	}

	public static GameObject getFirst()
	{
		Debug.Log(((Front + 1) % MAX) + "front + 1 % MAX");
		return taskQueue[((Front + 1) % MAX)];
	}
}
