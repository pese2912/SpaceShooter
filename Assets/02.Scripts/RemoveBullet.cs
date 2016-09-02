﻿using UnityEngine;
using System.Collections;

public class RemoveBullet : MonoBehaviour {

    public GameObject sparkEffect;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET")
        {
            Object obj = Instantiate(sparkEffect, coll.transform.position, Quaternion.identity);
            Destroy(coll.gameObject);
            Destroy(obj,0.2f);
            //Destroy(this);
            //Destroy(this.gameObject);
        }
    }

}
