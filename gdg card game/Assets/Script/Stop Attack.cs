using UnityEngine;
using System.Collections;

public class StopAttack : MonoBehaviour {
    Animator anim;
    public float t;
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        t = Time.time;
        Debug.Log(anim);
    }
	
	// Update is called once per frame
	void Update () {
        if (t + 1 < Time.time)
        {
            anim.SetInteger("IsAtt", 0);
            Debug.Log("test");
            Destroy(this);
        }
	}
}
