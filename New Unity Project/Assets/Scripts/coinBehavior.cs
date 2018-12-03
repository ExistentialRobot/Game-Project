using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinBehavior : MonoBehaviour {
    public Vector2 pointA;
    public Vector2 pointB;
    public Vector2 coinPos;
    public float speed = .2f;

    private bool switcher;
    private Vector2 point;
    private Rigidbody2D rig;

	void Start ()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        coinPos = rig.position;
        pointA = new Vector2(coinPos.x, coinPos.y + 0.02f);
        pointB = new Vector2(coinPos.x, coinPos.y - 0.02f);
	}
	
	void Update ()
    {
        coinPos = rig.position;
        if(coinPos == pointA)
        {
            switcher = false;
        }
        else if(coinPos == pointB)
        {
            switcher = true;
        }
        switch (switcher)
        {
            case true:
                point = pointA;
                break;
            case false:
                point = pointB;
                break;
        }
        rig.transform.position = Vector2.MoveTowards(rig.position, point, speed * Time.deltaTime);
    }
}
