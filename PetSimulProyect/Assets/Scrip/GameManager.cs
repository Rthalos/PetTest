using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject hungertext;
    public GameObject hapinestext;

    public GameObject pet;
        void Update()
    {
        hapinestext.GetComponent<Text>().text = "" + pet.GetComponent<PetScrip>().hapines;
        hungertext.GetComponent<Text>().text = "" + pet.GetComponent<PetScrip>().hunger; 
    }   
}
