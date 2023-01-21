using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointAndShoot : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer; // sprite rendererer for the camera!
    public Sprite camNormal;
    public Sprite camFlash;

    public MainMenu menu;

    public bool firstEncounter = false;
    public bool caught; // was a ufo caught with that shot
    public GameObject filmOneCheck; // so we can show player points!
    public GameObject filmTwoCheck;
    public GameObject filmThreeCheck;
    public GameObject filmOneX;
    public GameObject filmTwoX;
    public GameObject filmThreeX;

    public UFO ufo0;
    public UFO ufo1;
    public UFO ufo2;
    public UFO ufo3;
    public UFO ufo4;
    public UFO ufo5;
    public UFO ufo6;
    public UFO ufo7;
    public UFO ufo8;

    public float reloadTime; // how long it takes to be able to take another picture
    public float levelTime; // how long the level is
    public bool canShoot;
    public int film = 3; // dictates how many times the player can snap a pic
    private int startingFilm;
    public int points = 0;
    public int pointsForVictory = 2;

    public GameObject crosshairs;
    public GameObject player; // this is named weirdly but I'm lazy, this is the polaroid camera

    public AudioSource audioSource;
    public AudioClip snapSound;
    private Vector3 target;
    private float timer;
    private float levelTimer = 0f;
    public float ufoTimeBetween = 2.5f;
    private float ufoTimer;
    float randomNumber; // select which ufos go
    private List<int> ufos = Enumerable.Range(0, 8).ToList();
    public System.Random ran = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        startingFilm = film;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.visible == true) // keeps the real cursor hidden
        {
            Cursor.visible = false;
        }
        // don't really know how this stuff works, but it ties the reticle to the cursor
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y   ));
        crosshairs.transform.position = new Vector2(target.x, target.y); 

        if (!canShoot) 
        {
            timer += Time.deltaTime;
            if (timer > reloadTime)
            {
                canShoot = true;
                playerSpriteRenderer.sprite = camNormal;
                timer = 0; // dipshit!
                caught = false;
            }
            
        }

        if (!firstEncounter)
        {
            int index = ran.Next(0, ufos.Count - 1);
            int ufonumber = ufos.ElementAt(index);
            HorribleDecision(ufos.ElementAt(index));
            ufos.RemoveAt(index);
            firstEncounter = true;
        }

        ufoTimer += Time.deltaTime;
        if (ufoTimer > ufoTimeBetween)
        {
            int index = ran.Next(0, ufos.Count - 1);
            int ufonumber = ufos.ElementAt(index);
            HorribleDecision(ufos.ElementAt(index));
            ufos.RemoveAt(index);
                
            ufoTimer = 0; // dipshit!
        }

        if (Input.GetMouseButtonDown(0) && canShoot && film > 0)
        {
            film--; // every time we snap a pic, we lose some film
            Debug.Log("Film = " + film);
            audioSource.PlayOneShot(snapSound);
            ChangePlayerSprite();
            canShoot = false;
            pointTrack();
        }

        levelTimer += Time.deltaTime; // increment time
        if (levelTimer >= levelTime)
        {
            if (points >= pointsForVictory)
            {
                Debug.Log("You win! :)");
                menu.LoadNextLevel("Win Screen");
                //SceneManager.LoadScene("Win Screen");
            }
            else
            {
                Debug.Log("Stinky loser :(");
                menu.LoadNextLevel("Lose Screen");
                //SceneManager.LoadScene("Lose Screen");
            }
            
        }

    }

    void ChangePlayerSprite()
    {
        playerSpriteRenderer.sprite = camFlash;
    }

    public void UfoCaught()
    {
        if (canShoot && film > 0)
        points++;
        caught = true;
    }

    private void pointTrack()
    {
        if (film == startingFilm-1)
        {
            if (caught)
            {
                filmOneCheck.SetActive(true);
            }
            else
            {
                filmOneX.SetActive(true);
            }
        }

        if (film == startingFilm-2)
        {
            if (caught)
            {
                filmTwoCheck.SetActive(true);
            }
            else
            {
                filmTwoX.SetActive(true);
            }
        }

        if (film == startingFilm-3)
        {
            if (caught)
            {
                filmThreeCheck.SetActive(true);
            }
            else
            {
                filmThreeX.SetActive(true);
            }
        }
    }

    private void HorribleDecision (int number) // I'm so so sooooo sorry for this
    {
        if (number == 0)
        {
            ufo0.Enable();
        }
        else if (number == 1)
        {
            ufo1.Enable();
        }
        else if (number == 2)
        {
            ufo2.Enable();
        }
        else if (number == 3)
        {
            ufo3.Enable();
        }
        else if (number == 4)
        {
            ufo4.Enable();
        }
        else if (number == 5)
        {
            ufo6.Enable();
        }
        else if (number == 7)
        {
            ufo7.Enable();
        }
        else if (number == 8)
        {
            ufo8.Enable();
        }
    }
}
