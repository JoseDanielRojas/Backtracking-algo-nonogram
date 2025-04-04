using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class picross_square : MonoBehaviour, IPointerDownHandler
{

    public int square_step = 0;

    public Texture cross;


    // Usa esta inicialización
    void Start () {
		
	}

    public void OnPointerDown(PointerEventData pointerData)
    {
        if (this.gameObject.tag == "picross_square")
        {
            this.square_step++;
            if (this.square_step == 1)
            {
                this.gameObject.GetComponent<RawImage>().texture = null;
                this.gameObject.GetComponent<RawImage>().color = new Color32(0, 0, 0, 255);
            }  
            else if (this.square_step == 2)
            {
                this.gameObject.GetComponent<RawImage>().texture = cross;
                this.gameObject.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
            }
            else
            {
                this.gameObject.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
                this.square_step = 0;
                this.gameObject.GetComponent<RawImage>().texture = null;
            }
        }
    }
	
	// Update is called 
	void Update () {
		
	}
}
