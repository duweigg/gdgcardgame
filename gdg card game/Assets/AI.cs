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

    public float distTravelled;

    public bool isMoving;
    public bool isSelected;
    public bool isColliding;

    public GameObject collided;


    // Use this for initialization
    void Start() {
        tm = FindObjectOfType<turns_manager>();
        attackdist = 4;
        maxdist = 15;
        speed = 0.2f;
        isMoving = true;
    }

    // Update is called once per frame
    void Update() {
    }

    public IEnumerator AIMove() {
        //Find closest Unit
        mindist = Vector3.Distance(transform.position, tm.StatsLists[0].gameObject.transform.position);
        closest = tm.StatsLists[0];
        distTravelled = 0;
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

        transform.LookAt(targetpos);
        Vector3 castDirection = transform.localRotation * new Vector3(10, 0, 0);
        
        //move to within 4 units
        if (mindist > 4) {
            if (mindist < maxdist + 4) {
                targetpos = Vector3.MoveTowards(transform.position, targetpos, (mindist - attackdist));
                targetpos = new Vector3(targetpos.x, 70, targetpos.z);
                while (targetpos != transform.position && distTravelled < 15) {
                    if (isMoving && distTravelled < 15) {
                        isSelected = true;
                        Vector3 deltaDist = transform.position - Vector3.MoveTowards(transform.position, targetpos, speed);
                        distTravelled = distTravelled + deltaDist.magnitude;
                        transform.position = Vector3.MoveTowards(transform.position, targetpos, speed);
                        transform.position = new Vector3(transform.position.x, 70, transform.position.z);
                    }
                    yield return null;
                }
                isSelected = false;
            }
            else {
                targetpos = Vector3.MoveTowards(transform.position, targetpos, maxdist);
                targetpos = new Vector3(targetpos.x, 70, targetpos.z);
                while (targetpos != transform.position && distTravelled < 15) {
                    if (isMoving && distTravelled < 15) {
                        isSelected = true;
                        Vector3 deltaDist = transform.position - Vector3.MoveTowards(transform.position, targetpos, speed);
                        distTravelled = distTravelled + deltaDist.magnitude;
                        transform.position = Vector3.MoveTowards(transform.position, targetpos, speed);
                        transform.position = new Vector3 (transform.position.x, 70, transform.position.z);
                    }
                    yield return null;
                }
                isSelected = false;
            }
        }
        Debug.Log("MoveEnded");
        yield return null;
    }
    
    public void OnTriggerEnter(Collider other) {
        Debug.Log("Triggered");
        collided = other.gameObject;
        isColliding = true;
        //may need to tweak value 0.3 here depending on size and speed of final objects
        isMoving = false;
    }

    public void OnTriggerStay(Collider other) {
        collided = other.gameObject;
        AI otherStat = collided.GetComponent<AI>();
        //don't like how this works. have to check in
        if (isSelected||!isSelected&&otherStat.isSelected==false) {
            Vector3 castDirection = transform.localRotation * new Vector3 (10, 0, 0);

            Vector3 deltaPos = Vector3.MoveTowards(transform.position, collided.transform.position, -speed)- transform.position;
            targetpos = targetpos + deltaPos;
            Debug.DrawLine(targetpos, transform.position, Color.green, 10, false);
            transform.position = Vector3.MoveTowards(transform.position, castDirection, -speed);
                    }
    }


    public void OnTriggerExit() {
        Debug.Log("Left");
        isMoving = true;
        isColliding = false;
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
