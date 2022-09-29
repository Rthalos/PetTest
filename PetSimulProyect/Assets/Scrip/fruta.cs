using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruta : MonoBehaviour
{
    public int valor;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.transform.name);
        if (collision.gameObject.CompareTag("Pets"))
        {
            collision.transform.GetComponent<PetScrip>().LlamadoDeFruta(valor);
            Destroy(this.gameObject);
        }
    }
}
