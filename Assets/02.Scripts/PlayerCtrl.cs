using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Anims
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runRight;
    public AnimationClip runLeft;
    public AnimationClip[] dies;
}

public class PlayerCtrl : MonoBehaviour {

    public Image hpBar;
    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    private Transform tr;
    private Animation anim;

    public Anims anims;

    public float moveSpeed = 0.3f;
    public float rotSpeed = 80.0f;

    private float initHp = 100.0f;
    private float currHp = 100.0f;

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();
        anim.clip = anims.idle; 
        anim.Play();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

//        Debug.Log("h=" + h);
//        Debug.Log("v=" + v);
       
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime *  r);

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
            Vector3.forward = Vector3(0, 0, 1)
            Vector3.right   = Vector3(1, 0, 0)
            Vector3.up      = Vector3(0, 1, 0)

            Vector3.zero    = Vector3(0, 0, 0)
            Vector3.one     = Vector3(1, 1, 1)
         */

	}

    //void OnCollisionEnter

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "PUNCH")
        {
            currHp -= 5.0f;
            hpBar.fillAmount = currHp / initHp; 
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player Die");
        OnPlayerDie();
        GameMgr.instance.isGameOver = true;
        /*
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        foreach (GameObject monster in monsters)
        {
            //monster.GetComponent<MonsterCtrl>().MonsterWin();
            monster.SendMessage("MonsterWin", SendMessageOptions.DontRequireReceiver);
        }
        */
    }

}
