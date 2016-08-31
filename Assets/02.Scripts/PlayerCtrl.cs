using UnityEngine;
using System.Collections;

[System.Serializable] // 직렬화
public class Anims
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runRight;
    public AnimationClip runLeft;
   // public AnimationClip[] dies;
}


public class PlayerCtrl : MonoBehaviour {


    private Transform tr;
    private Animation anim;

    public Anims anims;

    public float moveSpeed = 5f;
    public float rotSpeed = 80.0f;

	// Use this for initialization
	void Start () {

        tr = this.gameObject.GetComponent<Transform>();
        anim = this.gameObject.GetComponent<Animation>();
        anim.clip = anims.idle;
        anim.Play();
	}

	// Update is called once per frame
	void Update () {
	
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		float r = Input.GetAxis("Mouse X");




		Debug.Log("h=" + h);
		Debug.Log("v="+v);

		//transform.position += new Vector3(0f, 0f, 0.1f);

		Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir.normalized* Time.deltaTime * moveSpeed);

		//transform.Translate(Vector3.forward * 0.1f*v);
		//transform.Translate(Vector3.right * 0.1f*h);
        tr.Rotate(Vector3.up * rotSpeed *Time.deltaTime* r);

        if (v >= 0.1f)
        {
            anim.CrossFade(anims.runForward.name, 0.3f);
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade(anims.runBackward.name, 0.3f); 
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade(anims.runRight.name, 0.3f); 
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade(anims.runLeft.name, 0.3f); 
        }
        else
        {
            anim.CrossFade(anims.idle.name, 0.3f); 
        }
		/*
		 * Vector3.forward 	= Vector3(0,0,1)
		 * Vector3.right   	= Vector3(1,0,0)
		 * Vector3.up 		= Vector3(0,1,0)
		 * 
		 * Vector3.zero 	= Vector3(0,0,0)
		 * Vector3.one 		= Vector3(1,1,1)
		 * 
		 * */
	}

}
