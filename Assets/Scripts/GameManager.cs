using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public GameObject ball;

	float counter = 120f;
	public GameObject countdown;
	public float ballforce;
	public Transform ballTarget;
	public int totalBalls;
	public  bool readyToshoot; 
	public GameObject[] allLevels;
	public int currentLevel;
	Plane plane = new Plane(Vector3.forward,0);

    public Ball ballScript;
    public bool gameHasStarted;

    public int shootedBall;



	void Awake()
		{
			if(instance == null)
			{
				instance = this;
			}else
			{
				Destroy(this);
			}
		}
	


    public void StartGame()
    {
        gameHasStarted = true;
        readyToshoot = true;
    }
 


	void Update()
	{

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
		if(Timeron())
		{
			if(counter>99)
			{
				counter-= Time.deltaTime;
				countdown.GetComponent<Text>().text= Mathf.Round(counter).ToString();
			}
			else if(counter>9)
			{
				counter-= Time.deltaTime;
				countdown.GetComponent<Text>().text= "0"+Mathf.Round(counter).ToString();
			}
			else if(counter>0&&counter<10)
			{
				counter-= Time.deltaTime;
				countdown.GetComponent<Text>().text= "00"+Mathf.Round(counter).ToString();
			}
			else
			{
				StartCoroutine((GameOver()));
			}


		}

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,5));

		if(Input.GetMouseButton(0) && readyToshoot)
		{
			ball.GetComponent<Animator>().enabled = false;
		
			ball.transform.position = new Vector3(mousePos.x,ball.transform.position.y,ball.transform.position.z);

		}


		Vector3 dir = ballTarget.position - ball.transform.position;
		if(Input.GetMouseButtonUp(0) && readyToshoot)
		{
			//Shoot the ball

			ball.GetComponent<Rigidbody>().AddForce(dir * ballforce,ForceMode.Impulse);
			MusicController.instance.PlayShootSound();
			
			readyToshoot = false;
            shootedBall++;
			totalBalls--;
            UIManager.instance.UpdateBallIcons();

			if(totalBalls <= 0)
			{
				//Check CoolDown
				print("CoolDowm");
                StartCoroutine(Checkcooldown());
			}

		}

		//place the target
		float dist;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(plane.Raycast(ray,out dist))
		{
			Vector3 point = ray.GetPoint(dist);
			ballTarget.position = new Vector3(point.x,point.y,0);
		}

	}


	public void GroupFallenCheck()
	{

			if(AllGrounded())
			{
				// Load next level
				LoadNextLevel();
			}
			
		

	}
	public bool Timeron()
	{
		return true;
	}

	bool AllGrounded()
	{
		Transform canSet = allLevels[currentLevel].transform;
		foreach(Transform t in canSet)
		{
			if(t.GetComponent<Can>().hasFallen == false)
			{
				return false;
			}
		}

		return true;
	}

    public void LoadNextLevel()
    {
        if(gameHasStarted)
        {
            StartCoroutine(LoadNextLevelRoutine());
        }
        
    }

   IEnumerator LoadNextLevelRoutine()
    {
        Debug.Log("Loading Next Level");
        yield return new WaitForSeconds(1.5f);
        UIManager.instance.ShowBlackFade();
        readyToshoot = false;
        allLevels[currentLevel].SetActive(false);
        currentLevel++;

        if (currentLevel >= allLevels.Length) currentLevel = 0;
		MusicController.instance.PlaynxtlvlSound();
        yield return new WaitForSeconds(1.0f);
        UIManager.instance.UpdateScoreMultiplier();
        //shootedBall = 0;
		
        allLevels[currentLevel].SetActive(true);
        UIManager.instance.UpdateBallIcons();
        ballScript.RepositionBall();
        //AddExtraBall(1);
    }


    public void AddExtraBall(int count)
    {
        if(totalBalls < 5)
        {
            totalBalls += count;
            UIManager.instance.UpdateBallIcons();
        }
    }


    IEnumerator Checkcooldown()
    {
        yield return new WaitForSeconds(2);
		if(totalBalls<=0)
		{
        	if (AllGrounded() == false)
        	{
				
            	UIManager.instance.gameOverUI.SetActive(true);
				MusicController.instance.Playfailsound();
				StartCoroutine(CoolDown());
        	}
		}
    }
	IEnumerator CoolDown()
	{
		yield return new WaitForSeconds(5);
		totalBalls =5;
		counter-=5;
        UIManager.instance.UpdateBallIcons();
		UIManager.instance.gameOverUI.SetActive(false);

	}
	IEnumerator GameOver()
	{
		yield return new WaitForSeconds(2);
		UIManager.instance.gameOver.SetActive(true);
		MusicController.instance.Playfailsound();
	}
}
