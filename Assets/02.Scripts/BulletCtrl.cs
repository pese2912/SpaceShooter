using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour {
    public float speed = 800.0f;
    public int damage = 10;

    //public Rigidbody rb;

	// Use this for initialization
	void Start () {
       //rb = GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward * speed);
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);
	}
	
}
