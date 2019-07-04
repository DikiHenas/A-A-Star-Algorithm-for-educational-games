using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    Main_menu fromMenu;
    CharacterBehave heroscrpit;    
    public Animator monsterAnimator;

    public Transform target;    
    Vector3[] path;
    int targetIndex;

    bool idle = true, move = false, attack = false, die=false;
    float jarakserang;

    private void Start()
    {
        //fromMenu = GameObject.Find("ScriptManaggerMenu").GetComponent<Main_menu>();
        heroscrpit = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehave>();        
             
        
    }
    private void Update()
    {
        
       
        jarakserang = Vector3.Distance(this.transform.position, target.position);
        if (jarakserang < 3)
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
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
            if (pathSuccessful)
            {
                path = newPath;
                targetIndex = 0;
                // Debug.Log("path " + path.Length);                                 
                if (path.Length > 0 && die==false)
                {
                attack = false;
                idle = false;
                move = true;
                die = false;
                monsterAnimator.SetBool("Move", move);
                monsterAnimator.SetBool("Attack", attack);
                monsterAnimator.SetBool("Idle", idle);
                monsterAnimator.SetBool("Die", die);                               
                }
            }
        }    
    IEnumerator Tewas()
    {
        yield return new WaitForSeconds(3);        
        die = false;
        Destroy(gameObject);        
    }
    //IEnumerator FollowPath()
    //{                
    //    Vector3 currentWaypoint = path[0];        
    //    while (true)
    //    {
            
    //        if (path ==null)
    //        {
    //            move = false;
    //            idle = true;
    //            monsterAnimator.SetBool("Move", move);
    //            monsterAnimator.SetBool("Idle", idle);
    //            break;
    //        }
           
    //        if (transform.position == currentWaypoint)
    //        {
                
    //            targetIndex++;
    //            if (targetIndex >= path.Length)
    //            {                    
    //                yield break;
    //            }
    //            currentWaypoint = path[targetIndex];
               
    //        }
    //        if (move==true)
    //        {
                
    //            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
    //            yield return null;
    //        }
    //        transform.LookAt(target.position);

    //    }
    //}

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            Debug.Log("KENA HIT");
            if (gameObject != null) {
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
    
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
