using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainBehavior : MonoBehaviour {
    public playerBehavior playerScript;
    public int coinCount;

	void Update ()
    {
        coinCount = playerScript.coins;
	}
}
