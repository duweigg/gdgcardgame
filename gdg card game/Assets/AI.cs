using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
    public turns_manager tm;
    public float mindist;
    public Stats closest;
    public bool triggered;
    public float step;
    public float speed;
    public Vector3 targetpos;
    public float maxdist;
    public float attackdist;
    public Stats selectedscript;

    int i;
    public float dist;


    // Use this for initialization
    void Start() {
        tm = FindObjectOfType<turns_manager>();
        attackdist = 5;
        maxdist = 15;
        speed = 1;
    }

    // Update is called once per frame
    void Update() {

    }

    public IEnumerator AIMove() {
        //Find closest Unit
        mindist = Vector3.Distance(transform.position, tm.StatsLists[0].gameObject.transform.position);
        closest = tm.StatsLists[0];
        for (i = 0; i < 20; i++) {
            if (tm.StatsLists[i] != null) {
                dist = Vector3.Distance(transform.position, tm.StatsLists[i].gameObject.transform.position);
                if (dist < mindist) {
                    mindist = dist;
                    closest = tm.StatsLists[i];
                }
            }
        }
        targetpos = closest.transform.position;
        //move to within 5 units
        if (mindist > 5) {
            if (mindist < maxdist + 5) {
                targetpos = Vector3.MoveTowards(transform.position, targetpos, (mindist - attackdist));
				transform.LookAt (targetpos);
                while (targetpos != transform.position) {
                    transform.position = Vector3.MoveTowards(transform.position, targetpos, speed);
                    yield return null;
                }
            }
            else {
                targetpos = Vector3.MoveTowards(transform.position, targetpos, maxdist);
				transform.LookAt (targetpos);
                while (targetpos != transform.position) {
                    transform.position = Vector3.MoveTowards(transform.position, targetpos, speed);
                    yield return null;
                }
            }
        }
        yield return null;
    }

    public void AIAttack() {
        if (closest != null) {
            selectedscript = GetComponent<Stats>();
            selectedscript.hit();
            if (closest.isAttacked()) {
                selectedscript.hasMoved = true;
            }
            else {
                selectedscript.hasAttacked = false;
            }
        }
    }

}
