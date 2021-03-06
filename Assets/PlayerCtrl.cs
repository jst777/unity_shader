﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

	// Use this for initialization
	public GameObject fellowPrefab = null;
	public Transform[] fellowPos;

	void Start () {
		CreateFellow ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateFellow()
	{
		//fellowPrefab = (GameObject)Resources.Load("Fellow", typeof(GameObject));
		if (fellowPrefab != null) {
			if (fellowPos.Length > 0) {
				string fellowName;
				for(int i=0; i< fellowPos.Length; i++)
				{
					fellowName = "Fellow" + (i + 1).ToString ();
					int hasFellow = PlayerPrefs.GetInt(fellowName);
					if (hasFellow ==1) {
						GameObject fellow = (GameObject)Instantiate (fellowPrefab, fellowPos [i].position, fellowPos [i].rotation);
						fellow.transform.parent = transform;
						FellowCtrl fellowCtrl = fellow.GetComponent<FellowCtrl> ();
						if (fellowCtrl != null) {
							fellowCtrl.SetColorIndex (i, 3);
						}
					}
					//Debug.Log ("fellowPrefab created");
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "EnemyBullet") {
			Destroy (other.gameObject);


			HealthBar healthBar = GetComponent<HealthBar> ();
			if (healthBar != null) {
				healthBar.currHealth -= 1;

				//device vibrate when hp down
				Handheld.Vibrate ();

				if (healthBar.currHealth <= 0) {
					//unactivated object can't found in gameobject.find
					//so i found that in uimgr
					GameObject uiMgrobj = GameObject.Find ("UIManager");
					if (uiMgrobj != null) {
						UIMgr uiMgr = uiMgrobj.GetComponentInChildren<UIMgr> ();
						if (uiMgr != null) {
							uiMgr.gameOver.SetActive (true);
						}
					}

					this.gameObject.SetActive(false);
				}
			}
		}
	}
}
