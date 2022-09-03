using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScrip : MonoBehaviour
{
    private bool activo = true;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(""+activo); test
        if (active == false) { 
            DestroyImmediate(this);
        //Debug.Log("comida destruida"); test
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "pet")
        {
            //Debug.Log("colision with the fruit"); test
            active = false;
        }
    }

    public bool active   // valor publico para poder trabajar el estado de la comida
    {
        get { return activo; }
        set { activo = value; }
    }
}
