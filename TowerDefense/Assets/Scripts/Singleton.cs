﻿using UnityEngine;

public class Singleton <T> : MonoBehaviour where T : MonoBehaviour {
private static T instance;

public static T Instance {
	get{
		if (instance == null)
		instance = FindObjectOfType<T>();
		else if (instance != FindObjectOfType<T>())
		Destroy (FindObjectOfType<T>());

		DontDestroyOnLoad(FindObjectOfType<T>());

		return instance;
	}

}
}
