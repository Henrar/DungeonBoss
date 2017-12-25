using UnityEngine;

public class FlyingMovement : MonoBehaviour
{
    public float Speed = 10;

    private Rigidbody2D _rigidBody;

	// Use this for initialization
	void Start ()
	{
	    _rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    var horizontal = Input.GetAxis("Horizontal");
	    var vertical = Input.GetAxis("Vertical");
        //Debug.Log(horizontal + " " + vertical);
	    var move = new Vector2(horizontal, vertical);
	    _rigidBody.velocity = Speed*move;
	}
}
