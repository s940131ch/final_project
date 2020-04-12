using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class handleTask
{
    public static int Front = 0;
    public static int Rear = 0;
    public static int MAX = 10;
    public static GameObject[] taskQuene = new GameObject[MAX];
    public static void pushTask(GameObject obj)
    {
		if (!isFull())
		{
			Rear = (Rear + 1) % MAX;
			taskQuene[Rear] = obj;
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


}

