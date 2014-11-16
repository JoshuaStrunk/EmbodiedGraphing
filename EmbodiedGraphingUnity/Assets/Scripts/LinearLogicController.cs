using UnityEngine;
using System.Collections;

public class LinearLogicController : MonoBehaviour {

	enum State { Slope, YIntercept, Both};


	public Vector2 pointA;
	public Vector2 pointB;

	//Slope of the line
	public float targetM = 1f;

	//Y intercept of the line
	public int targetB = 0;

	// allowed amount of difference between target and current
	public float tolerance = .1f;

	//Amount of time value must be held correctly
	public float holdTime = 2f;
	//Amount of time the has been held correctly 
	float heldTime = 0f;


	public int testingNum = 5;
	int currentTestNum = 0;

	State state = State.Slope;
	

	//graphicsScript;

	public void loadValues(Vector2 pointA, Vector2 pointB) {
		this.pointA = pointA;
		this.pointB = pointB;
	}

	// Use this for initialization
	void Start () {
		//graphicsScript = gameObject.GetComponent<>();
	}
	
	// Update is called once per frame
	void Update () {

		float currentM;
		int currentB;

		//Calculate slope
		currentM = (pointA.y - pointB.y) / (pointA.x - pointB.x);

		//Calulate y-intercept
		currentB = (int) (pointA.y - (pointA.x * currentM));

		//graphicScript.setValue();

		switch (state) {

			case State.Slope:
				
				if(slopeCheck(currentM)) {
					if(timeCheck()) {
						slopeGood();
					}
				}
				else {
					heldTime = 0f;
				}
				break;

			case State.YIntercept:
				if(interceptCheck(currentB)) {
					if(timeCheck()) {
						interceptGood();
					}
				}
				else {
					heldTime = 0f;
				}
				break;

			case State.Both:
				if(slopeCheck(currentM) && interceptCheck(currentB)) {
					if (timeCheck()) {
						bothGood();
					}
				}
				else {
					heldTime = 0f;
				}
				break;

			default:
				Debug.Log("Issue in LinearLogicController, Update(), switch, default case called");
				break;
		}
	
	}

	void slopeGood() {
		Debug.Log("Slope Good");
		//graphicScript.success();

		if(++currentTestNum >= testingNum) {
			//graphicScript.changeState((int) State.YIntercept);
			state = State.YIntercept;
			targetB = generateB();
			currentTestNum = 0;
		}
		else {
			targetM = generateM();
		}
		heldTime = 0f;
	}
	void interceptGood() {
		Debug.Log("Intercept Good");
		//graphicScript.success();
		if(++currentTestNum >= testingNum) {
			//graphicScript.changeState((int) State.Both);
			state = State.Both;
			targetM = generateM();
			currentTestNum = 0;
		}
		targetB = generateB();
		heldTime = 0f;
	}
	void bothGood() {
		Debug.Log("Both Good");
		//graphicScript.success();
		if(++currentTestNum >= testingNum) {
			Debug.Log("!!!FINISHED!!!");
			//Application.LoadLevel();
		}
		targetM = generateM();
		targetB = generateB();
		heldTime = 0f;
	}


	float generateM() {
		return ((float)(int)Random.Range(-4, 4)) / ((float)(int)Random.Range(1, 4));
	}
	int generateB() {
		return (int) Random.Range(-8, 8);
	}


	bool timeCheck() {
		heldTime += Time.deltaTime;
		return heldTime > holdTime;
	}
	bool slopeCheck(float currentM) {
		return Mathf.Abs(currentM - targetM) < tolerance;
	}
	bool interceptCheck(int currentB) {
		return Mathf.Abs(currentB - targetB) < tolerance;
	}

}
