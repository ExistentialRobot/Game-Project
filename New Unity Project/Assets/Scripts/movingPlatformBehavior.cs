using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatformBehavior : MonoBehaviour {
    public Vector2 pointA;
    public Vector2 pointB;

    public float speed;
    public float waitTime;
    public bool waited;

    private Rigidbody2D rig;
    public Vector2 point;
    public Vector2 objPos;
    private bool switcher;

    void Start ()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        objPos = rig.position;
        point = pointB;
        waited = true;
	}
	
	void Update ()
    {
        objPos = rig.position;
        if (objPos == pointA)
        {
            switcher = false;
        }
        else if (objPos == pointB)
        {
            switcher = true;
        }
        if (objPos == point)
        {
            waited = false;
            switch (switcher)
            {
                case true:
                    point = pointA;
                    break;
                case false:
                    point = pointB;
                    break;
            }
            StartCoroutine("Wait");
        }
        if (waited == true)
        {
            rig.transform.position = Vector2.MoveTowards(objPos, point, speed * Time.deltaTime);
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        waited = true;
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.SetParent(null);
        }
    }

}
