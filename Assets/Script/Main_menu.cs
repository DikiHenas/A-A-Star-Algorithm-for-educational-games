using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Main_menu : MonoBehaviour
{
    public GameObject panel, play, exit, guide;
    public GameObject hero;
    public GameObject Guide_pnl, Objective_pnl, jenis_pnl;
    public GameObject[] waypoints;
    int current = 0;
    int flag;
    public float speed = 1;
    float WPradius = 1;
    bool sampaitujuan = false;

    bool idle = true, victory,move;
    public Animator heroanimation;
    public float speedNpc, waktuMain;
    public int getPoint;
    public Button buttonPrev, buttonNext;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (hero != null)
        {
            heroanimation.SetBool("Idle", idle);
            heroanimation.SetBool("Maju",move);
            heroanimation.SetBool("Victory", victory);
            
            if (Input.GetKey("escape"))
            {
                panel.SetActive(true);
            }
            if (!sampaitujuan)
            {
                pergerakan();
            }
        }
        if(Guide_pnl.gameObject != null)
        {
            if (Guide_pnl.gameObject.activeSelf == true)
                buttonPrev.gameObject.SetActive(false);
            else
                buttonPrev.gameObject.SetActive(true);
            if (jenis_pnl.gameObject.activeSelf == true)
                buttonNext.gameObject.SetActive(false);
            else
                buttonNext.gameObject.SetActive(true);
        }
        
    }

    public void easyLv(){
    speedNpc=0.2f;
        getPoint = 20;
        waktuMain = 120f;
     SceneManager.LoadScene("SampleScene");       
    }
    public void medLv(){
    speedNpc=0.3f;
        getPoint = 15;
        waktuMain = 240f;
     SceneManager.LoadScene("SampleScene");   
    }
    public void hardLv(){
    speedNpc=0.5f;
        getPoint = 10;
        waktuMain = 500f;
     SceneManager.LoadScene("SampleScene");   
    }    
    public void keluarapps()
    {
        Application.Quit();
    }
    public void nextBtnOnClick()
    {
        
        flag++;
        if (flag==3)
            flag= 0;

        if (flag== 1)
        {
            Guide_pnl.gameObject.SetActive(false);
            Objective_pnl.gameObject.SetActive(true);
            jenis_pnl.gameObject.SetActive(false);
        }
        else if (flag== 2)
        {
            Guide_pnl.gameObject.SetActive(false);
            Objective_pnl.gameObject.SetActive(false);
            jenis_pnl.gameObject.SetActive(true);
        }
        else
        {
            Guide_pnl.gameObject.SetActive(true);
            Objective_pnl.gameObject.SetActive(false);
            jenis_pnl.gameObject.SetActive(false);
        }
    }
    public void prevBtnOnClick()
    {

        flag--;
        if (flag == -1)
            flag = 2;

        if (flag == 1)
        {
            Guide_pnl.gameObject.SetActive(false);
            Objective_pnl.gameObject.SetActive(true);
            jenis_pnl.gameObject.SetActive(false);
        }
        else if (flag == 2)
        {
            Guide_pnl.gameObject.SetActive(false);
            Objective_pnl.gameObject.SetActive(false);
            jenis_pnl.gameObject.SetActive(true);
        }
        else
        {
            Guide_pnl.gameObject.SetActive(true);
            Objective_pnl.gameObject.SetActive(false);
            jenis_pnl.gameObject.SetActive(false);
        }
    }
        public void pergerakan()
    {
        //Debug.Log(hero.transform.position);
        if (Vector3.Distance(waypoints[current].transform.position, hero.transform.position) < WPradius)
        {
            current++;
            if (current >= waypoints.Length)
            {
                sampaitujuan = true;
                idle = true;
                victory = true;
                move =false;
                play.SetActive(true);
                exit.SetActive(true);
                guide.SetActive(true);
            }
            

        }
        if (!sampaitujuan)
        {
            idle = false;
            move=true;
            hero.transform.position = Vector3.MoveTowards(hero.transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
            hero.transform.LookAt(waypoints[current].transform.position);
            heroanimation.SetBool("Maju",move);
        }
    }
}
