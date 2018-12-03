using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBehavior : MonoBehaviour {
    public float inputH;
    public Vector2 jumpHeight;
    public bool grounded;
    public GameObject player;
    public GameObject objCol;
    public Animator anim;
    public float speed = 5f;
    public int coins;
    public Button prompt;

    private Rigidbody2D rig;

    void Start()
    {
        anim = player.GetComponent<Animator>();
        rig = player.GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {

        inputH = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("InputH", inputH);
        if(inputH > 0)
        {
            rig.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if(inputH < 0)
        {
            rig.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if(Input.GetButtonDown("Jump") && grounded == true)
        {
            anim.SetBool("Jump", true);
            rig.AddForce(jumpHeight, ForceMode2D.Impulse);   
        }
        if(rig.velocity.y < 0)
        {
            Debug.Log("Falling");
            anim.SetBool("Jump", false);
        }
        if(grounded == false && anim.GetBool("Jump"))
        {
            anim.SetBool("Can Jump", false);
        }
        else if(grounded == true)
        {
            anim.SetBool("Can Jump", true);
        }
        if(Input.GetKey(KeyCode.E) && objCol.tag == "brick")
        {
            objCol.transform.SetParent(transform);
            speed = 2.5f;
        }
        else
        {
            objCol.transform.SetParent(null);
            objCol = null;
            speed = 5f;
        }
	}
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            grounded = true;
        }
        if (col.gameObject.tag == "brick")
        {
            prompt.gameObject.SetActive(true);
            objCol = col.gameObject;
            if (inputH >= 0 && col.transform.position.x > player.transform.position.x && objCol != col.gameObject)
            {
                speed = 0f;
            }
            else if (inputH >= 0 && col.transform.position.x < player.transform.position.x && objCol != col.gameObject)
            {
                speed = 5f;
            }
            else if (inputH <= 0 && col.transform.position.x < player.transform.position.x && objCol != col.gameObject)
            {
                speed = 0f;
            }
            else if (inputH <= 0 && col.transform.position.x > player.transform.position.x && objCol != col.gameObject)
            {
                speed = 5f;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        {
            anim.SetBool("Jump", false);
        }
        if (col.gameObject.tag == "brick")
        {
            objCol = col.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            grounded = false;
        }
        if(col.gameObject.tag == "brick")
        {
            prompt.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.layer == 10)
        {
            if (trig.gameObject.tag == "coin")
            {
                coins += 1;
                Destroy(trig.gameObject);
            }
        }
    }
}
