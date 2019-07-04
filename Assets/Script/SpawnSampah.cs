using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSampah : MonoBehaviour {

    public GameObject trashbag;   
    Vector3 posisiBarang;        
    public Transform[] spawnPoints;
    int flag;
    

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 10; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            spawningSampah(spawnPointIndex);
        }

    }
	
	// Update is called once per frame
	void Update () {
        flag = GameObject.FindGameObjectsWithTag("Sampah").Length;
        if (flag < 5) 
        for (int i = 0; i < 10; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            spawningSampah(spawnPointIndex);             
        }
    }


    void spawningSampah(int i)
    {
        // Debug.Log(i);
        GameObject Clonesampah = Instantiate(trashbag, spawnPoints[i].position, spawnPoints[i].rotation);
        Clonesampah.transform.parent = transform;        
    }
    
}
