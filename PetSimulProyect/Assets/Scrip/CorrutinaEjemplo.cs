using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrutinaEjemplo : MonoBehaviour
{
    private Coroutine corrutine = null;
    [SerializeField]
    Transform pelota;
    // Start is called before the first frame update
    void Start(){
        //StartCoroutine(rutina());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void boton(){
        if (corrutine == null)
            corrutine = StartCoroutine(rutina());
    }

    IEnumerator rutina(){
        float t = 5;
        while (t>0)
        {
            Debug.Log("cuenta regresiva: " + (int)t);
            t --;
            yield return new WaitForSeconds(1f);
        }
        
        Debug.Log("cuenta regresiva terminada");
        yield return null;

        int cantidad = 0;
        while (cantidad < 10 )
        {
            Vector3 nuevaposi = pelota.position;
            nuevaposi.x = Random.Range(0, 3);
            Debug.Log("Aleatoria:" + nuevaposi);
            while (!Mathf.Approximately(pelota.position.x,nuevaposi.x))
            {
                pelota.position = Vector3.MoveTowards (pelota.position, nuevaposi,4*Time.deltaTime);
                yield return null;
            }
            cantidad++;
            yield return null;
        }
        Debug.Log("sali de 3 posisiones");
        corrutine = null;
        yield break;
    }
}
