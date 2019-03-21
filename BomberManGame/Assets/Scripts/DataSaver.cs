using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour {

	// Use this for initialization
	public static int[] scores = new int[4];
	static bool addedToDontDestroy = false;
	void Awake () {
		if (!addedToDontDestroy) {
			DontDestroyOnLoad (gameObject);
			addedToDontDestroy = true;
		}else{
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}