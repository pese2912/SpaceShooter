using UnityEngine;
using System.Collections;

public class FireCtrl : MonoBehaviour {

    public GameObject bullet; //Bullet Prefab
    public Transform firePos;
    public AudioClip fireSfx;

    public MeshRenderer muzzleFlash;

    private AudioSource _audio;

    public float fireRate = 0.1f;
    private float nextFire = 0.0f;


	// Use this for initialization
	void Start () {
        _audio = GetComponent<AudioSource>();
        muzzleFlash.enabled = false;
	}
	
	// Update is called once per frame
   
    RaycastHit hit;
	void Update () {

        Debug.DrawRay(firePos.position, firePos.forward * 10.0f, Color.green); // 파이어포스가 보는 지점으로 그려줌.

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= nextFire)
            {
                Fire();

                if (Physics.Raycast(firePos.position, firePos.forward, out hit, 10.0f,1<<LayerMask.NameToLayer("MONSTER_BODY"))) // 무엇인가 걸리면 발사범위 10
                {
                   
                        Debug.Log("Monster Hit!!!!!");
                        hit.collider.gameObject.GetComponent<MonsterCtrl>().OnDamage(50, hit.point);

                }

                nextFire = Time.time + fireRate;
            }
        }
	}

    void Fire()
    {
        //Instantiate(bullet, firePos.position, firePos.rotation);
        _audio.PlayOneShot(fireSfx, 0.8f);
        StartCoroutine(this.ShowMuzzleFlash());
//        _audio.clip = fireSfx;
//        _audio.volume = 0.8f;
//        _audio.Play();

    }

    IEnumerator ShowMuzzleFlash()
    {
        float angle = Random.Range(0.0f, 360.0f);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);
        float scale = Random.Range(2.0f, 4.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale; //new Vector3(scale, scale, scale);
        //Vector3(1,1,1) * 2.0f = (2.0, 2.0, 2.0)
        muzzleFlash.enabled = true;
        //waiting...
        yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));
        muzzleFlash.enabled = false;
    }
}
