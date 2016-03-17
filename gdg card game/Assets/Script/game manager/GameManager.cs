using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
    //player attributes
    public int resourcesmax;
    public int resources;
    public Stats _attributes;
    public GameObject _terrain;


    //card array;
    public GameObject[] _cards;
    public GameObject[] _handcards;
    public GameObject temp;
    public int index;

    //for raycast place character
    public Camera cam;
    RaycastHit hitt = new RaycastHit(); //to get the point of mouse in 3D world
    public GameObject _char = null;
    int layermask = 1 << 10;   //layer 10 is the teerain



    int UIlayer = 1 << 5;


    //for selection
    RaycastHit hit;
    RaycastHit hit2;
    public GameObject selected;
    public GameObject lastSelected;
    public LayerMask layer;
    public LayerMask layer2;
    public int turn_count;
    public turns_manager _manager;
    public Stats selectedScript;
    public TapToMove moveScript;
    public int t;
    public int t2;
    public Stats targetScript;
    public bool isAttacking;
    public bool isMoving;
    int unitlayers = 1 << 11 | 1 << 12;


    public float moveDistance;
    public Vector3 hitpoint;
    public float dist;
    public float maxMove;
    public Vector3 position;

    public GameObject Class1;
    public GameObject Class2;
    public GameObject Class3;
    public GameObject Class4;
    public GameObject Class5;

    public GameObject _button;
    public Vector2 newPos;


    public int i;
    public int j;
    public int currentCard;
    public Vector2[] pos;
    private bool switched;
    public bool UIhit;

    //attacking/healing
    GameObject Attacker;
    GameObject Target;

    //range indicator
    public GameObject Move_range_indicator;
    public GameObject Att_range_indicator;
    Vector3 Position;

    //attack animation
    Animator anim;

	public bool debug;
	public GameObject debugObj;

    public GameObject mainUnit;
    public float summonDistance;

    //assign character to variable _char from the card.
    public void _getchar1(GameObject character) {
        _char = Instantiate(character, position, Quaternion.identity) as GameObject;
        if (_char != null) {
            _char.transform.parent = Class1.transform;
            if (_attributes == null) {
                _attributes = _char.GetComponent<Stats>();
                _char.transform.position = mainUnit.transform.position;
            }
        }
        resetSelected();
    }

    public void _getchar2(GameObject character) {
        _char = Instantiate(character, position, Quaternion.identity) as GameObject;
        if (_char != null) {
            _char.transform.parent = Class2.transform;
            if (_attributes == null) {
                _attributes = _char.GetComponent<Stats>();
                _char.transform.position = mainUnit.transform.position;
            }
        }
        resetSelected();
    }

    public void _getchar3(GameObject character) {
        _char = Instantiate(character, position, Quaternion.identity) as GameObject;
        if (_char != null) {
            _char.transform.parent = Class3.transform;
            if (_attributes == null) {
                _attributes = _char.GetComponent<Stats>();
                _char.transform.position = mainUnit.transform.position;
            }
        }
        resetSelected();
    }

    public void _getchar4(GameObject character) {
        _char = Instantiate(character, position, Quaternion.identity) as GameObject;
        if (_char != null) {
            _char.transform.parent = Class4.transform;
            if (_attributes == null) {
                _attributes = _char.GetComponent<Stats>();
                _char.transform.position = mainUnit.transform.position;
            }
        }
        resetSelected();
    }

    public void _getchar5(GameObject character) {
        _char = Instantiate(character, position, Quaternion.identity) as GameObject;
        if (_char != null) {
            _char.transform.parent = Class5.transform;
            if (_attributes == null) {
                _attributes = _char.GetComponent<Stats>();
                _char.transform.position = mainUnit.transform.position;
            }
        }
        resetSelected();
    }


    void placechar() {
        //character follow the mouse
        if (_char != null && _attributes.cost < resources) {

            Position = mainUnit.transform.position + new Vector3(0, 0, 1); // position of the range
            Move_range_indicator.transform.localScale = new Vector3(2 * summonDistance, 1, 2 * summonDistance); //size of the range
            Move_range_indicator.transform.position = Position; //place the range
            

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitt, 100, layermask);
            if ((hitt.point - mainUnit.transform.position).magnitude < summonDistance) {
                _char.transform.position = hitt.point;
            }

            //if left mouse is clicked;
			if (Input.GetMouseButtonDown(0)) {
                //to place the character
                t2 = Time.frameCount;
                _char = null;
                _attributes = null;
                _button.transform.position = newPos;

                for (i = 0; i < 5; i++) {
                    if (_handcards[i] == _button) {
                        _handcards[i] = null;
                    }
                }
                arrangeCards();
            }
            if (Input.GetMouseButton(1)) {
                Destroy(_char);
                Move_range_indicator.transform.position = new Vector3 (0,0,0);
           }
        } else if (_attributes != null && _attributes.cost > resources) {
            //show warning and play sound effect
            Debug.Log("not enough resource");
        }
        
    }

    public void setButton(GameObject button) {
        _button = button;
    }

    void selectChar() {
        if (_manager.turn_count % 2 == 0) {
            isAttacking = false;

            //selects character
            //need to add another condition here to allow heals again
            if (Input.GetMouseButtonDown(0)&&Time.frameCount!=t2) {
                Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray1.origin, ray1.direction, out hit, 300, layer.value)) {
                    Debug.Log("Hit!");
                    if (selected != null) {
                        lastSelected = selected;
                    }
                    selected = hit.collider.gameObject;

                    //edited by duwei 3 lines
                        selectedScript = selected.GetComponent<Stats>();
                        selectedScript.isSelected = true;

                        if (selectedScript.hasMoved == false) {
                            Position = selected.transform.position + new Vector3(0, 0, 1); // position of the range
                            Move_range_indicator.transform.localScale = new Vector3(2 * maxMove, 1, 2 * maxMove); //size of the range
                            Move_range_indicator.transform.position = Position; //place the range
                        }
                        else {
                            Move_range_indicator.transform.position = new Vector3(0, 0, 0);
                        }

                        if (selectedScript.turnEnded) {
                            resetSelected();
                            Debug.Log("Turn Ended. Reset.");
                    }
                    t = Time.frameCount;
                }
            }
            //deselects character
            if (!isMoving && Input.GetMouseButtonDown(1) && selectedScript != null) {
				setPosition ();
            }

            if (selected != null) {
                moveScript = selected.GetComponent<TapToMove>();
                isMoving = moveScript.isMoving;
            } else {
                isMoving = false;
            }

            if (!isMoving) {
                attack();
            }

            if (!isAttacking) {
                move();
            }
        }
    }

    void move() {
        Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (selectedScript != null) {
                if (selectedScript.isSelected == true && t != Time.frameCount) {
                    if (Input.GetMouseButtonDown(0)) {
                        Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);
						if (Physics.Raycast(ray1.origin, ray1.direction, out hit, 300, layer2.value)) {
                            hitpoint = hit.point;
                            Debug.Log("Moving!");
                            moveScript = selected.GetComponent<TapToMove>();
                            moveScript.ready_to_move = true;
                            selectedScript.hasMoved = true;
                        }
                        else {
                            Debug.Log("Too Far!");
                        }
                    }
                }
            }
        }
    }


    void attack() {
        if (selectedScript != null && selected != null) {
            if (Input.GetMouseButtonDown(0) && selectedScript.hasAttacked == false && t != Time.frameCount) {
                Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2.origin, ray2.direction, out hit, 300, unitlayers)) {
                    Debug.Log("Attacking!");



                    isAttacking = true;
                    Target = hit.collider.gameObject;
                    targetScript = Target.GetComponent<Stats>();
                    selectedScript.hit();
                    if (targetScript.isAttacked()) {
                        selectedScript.hasMoved = true;

                        //edited by duwei
                        anim = selected.GetComponent<Animator>();
                        if (anim != null) {
                            anim.SetInteger("IsAtt", 1);
                            anim.SetInteger("IsAtt", 0);
                        }

                        //edited by duwei 1 line
                        Position = selected.transform.position + new Vector3(0, 0, 1);

                        resetSelected();
                    } else {
                        selectedScript.hasAttacked = false;
                    }

                }
            }
        }

    }


	public void resetPosition(){
		if (!isMoving && selectedScript != null) {
			Debug.Log("Reset!");
			//edited bu duwei 1line (reversed the move option, return to the original place)
			if (selected.transform.position != Position - new Vector3(0, 0, 1)) {
				selected.transform.position = Position - new Vector3(0, 0, 1);
				selectedScript.hasMoved = false;
			}

			Move_range_indicator.transform.position = new Vector3(0, 0, 0);

			selectedScript.isSelected = false;
			selected = null;
			selectedScript = null;
		}
	}

    //resets selected units to null
    public void resetSelected() {
        Move_range_indicator.transform.position = new Vector3(0, 0, 0);
        selected = null;
        selectedScript = null;
    }

    public void setPosition() {
        if (selectedScript != null) {
            selectedScript.turnEnded = true;
            selectedScript.hasMoved = true;
            resetSelected();
        }
    }

	void shaffle_card(){
		for (int i=0; i<20; i++) {
			index = Random.Range (i,20);
			temp = _cards[index];
			_cards[index] = _cards[i];
			_cards[i] = temp;
		}
	}

	public void drawcard(){
        for (i = 0; i < 5; i++) {
            if (_handcards[i] == null) {
                _handcards[i] = _cards[currentCard];
                currentCard++;
				break;
            }
        }
		
	}

    public void arrangeCards() {
		for (i=0;i<5;i++){
			switched = false;
			if (_handcards[i]==null){
				for (j = i+1; j < 5&&!switched; j++) {
					if (_handcards [j] != null) {
						_handcards [i] = _handcards [j];
						_handcards [j] = null;
						switched = true;
					}
				}
			}
		}
        for (i=0; i<5; i++) {
			if (_handcards [i] != null) {
				_handcards [i].transform.position = pos [i];
			}
        }
    }

	// Use this for initialization
	void Start () {
		shaffle_card ();
        currentCard = 0;
		//draw 5 cards
		drawcard ();
		drawcard ();
		drawcard ();
		drawcard ();
		drawcard ();

        arrangeCards();

		_manager = this.GetComponent<turns_manager>();

        summonDistance = 25;

	}


    // Update is called once per frame
    void Update () {
        _manager.checkEnd();

        if (_char != null) {
            placechar();
        }

		turn_count=_manager.turn_count;
		if (turn_count % 2 == 0) {
			selectChar ();
		}
        
	}
}
