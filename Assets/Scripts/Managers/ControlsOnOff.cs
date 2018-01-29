using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsOnOff : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if UNITY_ANDROID
		CanvasGroup cg = GetComponent <CanvasGroup> ();
		cg.interactable = true;
		cg.alpha = 1;
#endif
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
