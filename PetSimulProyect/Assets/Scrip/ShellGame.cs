using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShellGame : MonoBehaviour
{
    private Coroutine rutina = null;

    [SerializeField]
    GameObject pelota;
    [SerializeField]
    TMP_Text instrucciones;
    private List<float> lista = new List<float> { 0,(float)-1.8,(float)1.8 };
    public GameObject[] vasos;
    [SerializeField]
    private float speed = 300;
    public GameObject pet;
    private int ubicacion;
    public GameObject panel;

    private void Start()
    {
        instrucciones.SetText("Luego de presionar empezar, adivina donde se esconde la pelota");
        pelota.transform.position = new Vector3(0,(float)-0.6,0);
        for (int i = 0; i < vasos.Length; i++)
        {
            vasos[i].SetActive(true);
            vasos[i].GetComponent<Button>().enabled = false;
        }
    }

    public void empezarBoton()
    {
        if (rutina == null)
            rutina = StartCoroutine(game(lista));
    }
    IEnumerator game(List<float> ubicaciones)
    {
        pet.SetActive(false);
        for (int i = 0; i < vasos.Length; i++)
        {
            vasos[i].SetActive(true);
            vasos[i].GetComponent<Button>().enabled = false;
        }
        float t = 5;
        while (t > 0){
            instrucciones.SetText ("Recuerda que la pelota comienza en el centro siempre, El juego empezara en: " + t);
            t--;
            yield return new WaitForSeconds(1f);
        }
        yield return null;
        instrucciones.SetText("Adivina donde esta la pelota");
        int movimientos = 0;
        while (movimientos < 10)
        {
            Vector3 nuevaposi = pelota.transform.position;
            ubicacion =(int)Random.Range(0, ubicaciones.Count);
            nuevaposi.x = ubicaciones[ubicacion];
            //Debug.Log("Aleatoria:" + ubicacion);
            while (!Mathf.Approximately(pelota.transform.position.x, nuevaposi.x))
            {
                pelota.transform.position = Vector3.MoveTowards(pelota.transform.position, nuevaposi, 10* speed * Time.deltaTime);
                yield return null;
            }
            movimientos++;
            yield return null;
        }
        for (int i = 0; i < vasos.Length; i++)
        {
            vasos[i].GetComponent<Button>().enabled = true;
        }
        rutina = null;
        yield break;
    }

    public void validarResultado(int respuesta)
    {
        if (respuesta == ubicacion)
        {
            instrucciones.SetText("Ganaste, si quieres jugar de nuevo presiona empezar");
            pet.GetComponent<PetScrip>().updatehapines(25);
        }
        else
        {
            pet.GetComponent<PetScrip>().updatehapines(-10);
            instrucciones.SetText("Perdiste, si quieres jugar de nuevo presiona empezar");
        }
        for (int i = 0; i < vasos.Length; i++)
        {
            vasos[i].SetActive(true);
            vasos[i].GetComponent<Button>().enabled = false;
        }
        rutina = null;
    }

    public void verpanel()
    {
        rutina = null;
        if (panel != null) {
            bool isActive = panel.activeSelf;
            pet.SetActive(isActive);
            panel.SetActive(!isActive);
            pelota.SetActive(!isActive);
        
        }
    }
}

