using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinijuegoScrip : MonoBehaviour
{
    public GameObject minigamepanel;
    public GameObject[] vasos;
    public GameObject pelota;
    public float speed;
    public GameObject instrucciones;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void iniciarminijuego()
    {
        int cd = 5;
        do {
            instrucciones.GetComponent<Text>().text = "El juego va a empezar en: "+cd+"...";
            cd -= (int)Time.deltaTime;
        } while (cd == 0);
        for (int i = 0; i < vasos.Length; i++)
        {
            vasos[i].GetComponent<Button>().enabled =false;
        }
        minijuegos();
    }
    public void minijuegos()
    {int ubicaionactualpelota = 1;
        for(int i=0; i<10; i++) {
            int nuevaubicacionpelota = (int)Random.Range(0, 3);
            do{
                if (nuevaubicacionpelota>ubicaionactualpelota) { pelota.transform.position += Vector3.right * speed * Time.deltaTime; }
                if (nuevaubicacionpelota< ubicaionactualpelota) { pelota.transform.position += Vector3.left * speed * Time.deltaTime; }
            } while (pelota.transform.position == vasos[nuevaubicacionpelota].transform.position);
            ubicaionactualpelota = nuevaubicacionpelota;
        }
        for (int t = 0; t < vasos.Length; t++)
        {
            vasos[t].GetComponent<Button>().enabled = true;
        }
    }
    public void verificarpelota(int boton)
    {
        if (pelota.transform.position == vasos[boton].GetComponent<Transform>().position)
        { instrucciones.GetComponent<Text>().text = "Ganaste, si quieres jugar de nuevo presiona empezar";
        }else { instrucciones.GetComponent<Text>().text = "Perdiste, si quieres jugar de nuevo presiona empezar"; }
    }
    public void trigerMiniGamePanel() { if (minigamepanel.activeInHierarchy) { minigamepanel.SetActive(false);
            instrucciones.GetComponent<Text>().text = "Luego de presionar empezar para adivinar donde se esconde la pelota";
        } }
}
