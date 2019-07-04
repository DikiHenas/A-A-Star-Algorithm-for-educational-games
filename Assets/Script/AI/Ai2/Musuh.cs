using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musuh : MonoBehaviour {
    Main_menu fromMenu;
    CharacterBehave heroscrpit;    
    public Animator monsterAnimator;

    public Transform target;       

    bool idle = true, move = false, attack = false, die=false;
    float jarakserang;

	// Use this for initialization
	void Start () {
        //fromMenu = GameObject.Find("ScriptManaggerMenu").GetComponent<Main_menu>();
        heroscrpit = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehave>();        
    }
	
	// Update is called once per frame
	void Update () {
        jarakserang = Vector3.Distance(this.transform.position, target.position);
        if (jarakserang < 0.5)
        {
            Debug.Log(jarakserang);
            idle = false;
            move = false;
            attack = true;
            monsterAnimator.SetBool("Attack", attack);
            monsterAnimator.SetBool("Idle", idle);
            monsterAnimator.SetBool("move", move);
        }
    }

     	

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            Debug.Log("KENA HIT");
            if (gameObject != null)
            {
                if (heroscrpit.health > 0)
                {
                    heroscrpit.health -= 5;
                }
                if (heroscrpit.point > 0)
                {
                    heroscrpit.point -= 5;
                }
                //else { return; }
                if (heroscrpit.isiInventory.Count != 0)
                {
                    int i = Random.Range(0, heroscrpit.isiInventory.Count);
                    Destroy(heroscrpit.isiInventory[i]);
                    heroscrpit.isiInventory.Remove(heroscrpit.isiInventory[i]);
                }
            }

        }
    }
    IEnumerator Tewas()
    {
        yield return new WaitForSeconds(3);
        die = false;
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Serangan")
        {
            idle = false;
            move = false;
            attack = false;
            die = true;
            monsterAnimator.SetBool("Attack", attack);
            monsterAnimator.SetBool("Move", move);
            monsterAnimator.SetBool("Idle", idle);
            monsterAnimator.SetBool("Die", die);
            StartCoroutine("Tewas");
        }
    }    
}
