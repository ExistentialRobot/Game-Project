using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerBehavior : MonoBehaviour {
    public float wait = 0;
    private Animator anim;

	void Start ()
    {
        anim = gameObject.GetComponent<Animator>();
        wait = Random.Range(0f, 1f);
        anim.StopPlayback();
        StartCoroutine("WaitTime");
	}
    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(wait);
        anim.Play("Flower");
    }
}
