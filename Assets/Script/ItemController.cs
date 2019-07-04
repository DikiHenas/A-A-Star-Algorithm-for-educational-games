using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {
    public Image gambar;
    public Text namaItem;
    public Sprite[] Itemimage;

	// Use this for initialization
	void Start () {
        gambar = this.GetComponentInChildren<Image>();
        namaItem = this.GetComponentInChildren<Text>();
        int randomItem = Random.RandomRange(0, Itemimage.Length);
        changeSprite(randomItem);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void changeSprite(int i)
    {
        gambar.sprite = Itemimage[i];
        namaItem.text = gambar.sprite.name;
        //Debug.Log("nama gambar : "+ gambar.sprite.name);
    }

}
