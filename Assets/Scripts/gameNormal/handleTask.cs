using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class handleTask
{
    public static int Front = 0;
    public static int Rear = 0;
    public static int MAX = 3;
    public static GameObject[] taskQueue = new GameObject[MAX];
    public static void pushTask(GameObject obj)
    {
		if (!isFull())
		{
			Rear = (Rear + 1) % MAX;
			taskQueue[Rear] = obj;
			//Debug.Log(taskQuene[Rear].transform.position);
		}
	}
	public static void popTask()
	{
		if (!isEmpty())
		{
			Front = (Front + 1) % MAX;
		}
	}
	public static bool isFull()
	{
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

