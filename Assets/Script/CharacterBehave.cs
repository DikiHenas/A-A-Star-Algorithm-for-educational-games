using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterBehave : MonoBehaviour
{
    Main_menu fromMenu = new Main_menu();
    //Virtual Joystick
    public VJHandler joystick;
    Vector3 movefw, movelr;
    float joyHor, joyVer;

    Vector3 move;
    public GameObject badan;
    float rotPlayer;
    // tas inventory
    public GameObject content, contentList, inventory;

    public float speed = 2f;
    float InputX, InputZ;

    bool idle = false, kiri=false ,kanan= false,serang=false, maju=false, mundur=false;

    public List<GameObject> isiInventory = new List<GameObject>();
    public Animator heroanimation;
    
    //score
    public int point,pointUtama;
    public Text Health,Points,Timer;

    //health
    public int health = 100;
    public float healthPercent;
    public Scrollbar healthBar;

    //timer
    public float timeLeft;
    public GameObject Lose_pnl, Win_pnl;
        
    // Use this for initialization
    void Start()
    {
        fromMenu = GameObject.Find("ScriptManaggerMenu").GetComponent<Main_menu>();
        timeLeft =  fromMenu.waktuMain; //80;
        pointUtama = fromMenu.getPoint;//10;

        Points.text = "Points : " + 0 + "/100";

    }

    // Update is called once per frame
    void Update()
    {
        rotPlayer = transform.localEulerAngles.y;
        Debug.Log(rotPlayer);     
        Health.text = "Health : \n" + health + "/100";
        healthPercent = health / 100f;
        if(healthBar.size> healthPercent)
        {
            healthBar.size = healthPercent;
        }
        else
        {
            if (healthPercent <= 1)
                healthBar.size = healthPercent;
        }
        if (timeLeft < 0)
        {
            Lose_pnl.SetActive(true);
        }
        else
        {

            timeLeft -= Time.deltaTime;
        }
        if (point >= 100)
            point = 100;
        if (health ==0)
        {
            Lose_pnl.SetActive(true);

        }
        

        if (point >= 100)
        {
            Win_pnl.SetActive(true);
        }
        Jalan();
        Timer.text ="Time\n"+(int)timeLeft;
        

    }

    void Jalan()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        joyHor = joystick.Horizontal();
        joyVer = joystick.Vertical();
        move = new Vector3(0, 0, InputZ);
        move = transform.TransformDirection(move);
        move *= speed;

        movefw = new Vector3(0, 0, joyVer);
        movelr = new Vector3(joyHor, 0, 0);


        if ((InputZ != 0 && InputZ > 0.1) || (movefw.z >= 0.4 || movefw.z <= -0.4))
        {


            movefw = transform.TransformDirection(movefw);
            movefw *= speed;
            // transform.localEulerAngles = new Vector3(0f, Mathf.Atan2(joyHor, joyVer) * 180 / Mathf.PI, 0f); // this does the actual rotaion according to inputs
            transform.Translate(movefw * speed * Time.deltaTime, Space.World);
            idle = false;
            kiri = false;
            kanan = false;
            mundur = false;
            maju = false;
            heroanimation.SetBool("Idle", idle);
            heroanimation.SetBool("Kanan", kanan);
            heroanimation.SetBool("Kiri", kiri);
            heroanimation.SetBool("Maju", maju);
            heroanimation.SetBool("Mundur", mundur);

            // player hadap kiri
            if (rotPlayer < 305 && rotPlayer > 240)
            {
                if (movefw.x <= -0.4)
                {
                    maju = true;
                    heroanimation.SetBool("Maju", maju);
                   
                }
                else if (movefw.x >= 0.4)
                {
                    mundur = true;
                    heroanimation.SetBool("Mundur", mundur);
                }


            }
            //hadap kanan
            else if (rotPlayer > 55 && rotPlayer < 125)
            {
                if (movefw.x >= 0.4)
                {
                    maju = true;                    
                    heroanimation.SetBool("Maju", maju);
                }
                else if (movefw.x <= -0.4)
                {
                    mundur = true;
                    heroanimation.SetBool("Mundur", mundur);
                }

            }
            //hadap belakang
            else if (rotPlayer > 125 && rotPlayer < 240)
            {

                if (movefw.z <= -0.4)
                {
                    maju = true;
                    heroanimation.SetBool("Maju", maju);
                }
                else if (movefw.z >= 0.4)
                {

                    mundur = true;
                    maju = false;
                    heroanimation.SetBool("Mundur", mundur);
                    heroanimation.SetBool("Maju", maju);
                }

            }
            // hadap depan
            else
            {

                if (movefw.z >= 0.4)
                {
                    Debug.Log("hadap depan");
                    maju = true;
                    heroanimation.SetBool("Maju", maju);

                }
                else if (movefw.z <= -0.4)
                {
                    mundur = true;
                    heroanimation.SetBool("Mundur", mundur);
                }
            }
        }
        else if (movefw.z <= 0.3 && movelr.x >= 0.7 || movelr.x <= -0.7)
        {
            movelr = transform.TransformDirection(movelr);
            movelr *= speed;
            //transform.localEulerAngles =  new Vector3(0f, Mathf.Atan2(joyHor, joyVer) * -180 / Mathf.PI, 0f); // this does the actual rotaion according to inputs
            transform.Translate(movelr * speed * Time.deltaTime, Space.World);
            idle = false;
            kiri = false;
            kanan = false;
            mundur = false;
            maju = false;
            heroanimation.SetBool("Idle", idle);
            heroanimation.SetBool("Kanan", kanan);
            heroanimation.SetBool("Kiri", kiri);
            heroanimation.SetBool("Maju", maju);
            heroanimation.SetBool("Mundur", mundur);
            Debug.Log(movelr);
            // player hadap kiri
            if (rotPlayer < 305 && rotPlayer > 240)
            {
                if (movelr.z >= 0.7)
                {
                    kanan = true;
                    heroanimation.SetBool("Kanan", kanan);
                }
                else if (movelr.z <=- 0.7)
                {
                    kiri = true;
                    heroanimation.SetBool("Kiri", kiri);
                }

            }
            //hadap kanan
            else if (rotPlayer > 55 && rotPlayer < 125)
            {
                if (movelr.z<= -0.7)
                {
                    kanan = true;
                    heroanimation.SetBool("Kanan", kanan);
                }
                else if (movelr.z >= 0.7)
                {
                    kiri = true;
                    heroanimation.SetBool("Kiri", kiri);
                }

            }
            //hadap belakang
            else if (rotPlayer > 125 && rotPlayer < 240)
            {

                if (movelr.x <= -0.7)
                {
                    kanan = true;
                    heroanimation.SetBool("Kanan", kanan);
                }
                else if (movelr.x >= 0.7)
                {
                    kiri = true;
                    heroanimation.SetBool("Kiri", kiri);
                }

            }
            // hadap depan
            else
            {
                if (movelr.x >= 0.7)
                {
                    kanan = true;
                    heroanimation.SetBool("Kanan", kanan);
                }
                else if (movelr.x <= -0.7)
                {
                    kiri = true;
                    heroanimation.SetBool("Kiri", kiri);
                }

            }
        }
        else
        {
            idle = true;
            kiri = false;
            kanan = false;
            mundur = false;
            maju = false;
            heroanimation.SetBool("Idle", idle);
            heroanimation.SetBool("Kanan", kanan);
            heroanimation.SetBool("Kiri", kiri);
            heroanimation.SetBool("Maju", maju);
            heroanimation.SetBool("Mundur", mundur);
        }       
        

    }

        public void Serang()
    {
        heroanimation.SetTrigger("Kick");

        if (heroanimation.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {            
            idle = true;
            heroanimation.ResetTrigger("Kick");
            heroanimation.SetBool("Idle", idle);                      
        }               
               
    }


    // collison atau tubrukan
    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag== "Sampah" )
        {
            inventory= Instantiate(content) as GameObject;            
            inventory.transform.SetParent(contentList.transform, false);
            isiInventory.Add(inventory);
            //Debug.Log("isi tas"+isiInventory.Count);
            
            Destroy(other.gameObject);
        }
        
        }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "b3Area")
        {                        
            for (int k = 0; k < isiInventory.Count; k++  )
            {
                if (isiInventory.Count!=0 && isiInventory[k].GetComponentInChildren<Text>().text == "PisauCutter" || isiInventory[k].GetComponentInChildren<Text>().text == "Obat"|| 
                	isiInventory[k].GetComponentInChildren<Text>().text == "BahanKimia"|| isiInventory[k].GetComponentInChildren<Text>().text == "JarumSuntik")
            {
                Destroy(isiInventory[k]);
                    isiInventory.Remove(isiInventory[k]);
                    point += pointUtama;
                    if(health<100)
                    health += 5;
                    Points.text = "Points : " + point + "/100";
                }
            }            
        }
        else if (other.tag == "anorganik")
        {
            
            for (int k = 0; k < isiInventory.Count; k++)
            {
                if (isiInventory.Count!= 0 && isiInventory[k].GetComponentInChildren<Text>().text == "Plastik" || isiInventory[k].GetComponentInChildren<Text>().text == "Kertas"||
                	isiInventory[k].GetComponentInChildren<Text>().text == "Kaleng")
                {
                    point += pointUtama;
                    Destroy(isiInventory[k]);
                    isiInventory.Remove(isiInventory[k]);
                    Points.text = "Points : " + point + "/100";
                }
            }            
        }
        else if (other.tag == "organik")
        {           
            for (int k = 0; k < isiInventory.Count; k++)
            {
                if (isiInventory.Count != 0 && isiInventory[k].GetComponentInChildren<Text>().text == "Roti" || isiInventory[k].GetComponentInChildren<Text>().text == "Sayuran" ||
                    isiInventory[k].GetComponentInChildren<Text>().text == "Daging" || isiInventory[k].GetComponentInChildren<Text>().text == "Buah")
                {                    
                    point += pointUtama/ 2;
                    Destroy(isiInventory[k]);
                    isiInventory.Remove(isiInventory[k]);
                    Points.text = "Points : " + point + "/100";
                }
            }            
        }
    }    
}
