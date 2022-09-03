using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject hungertext;
    public GameObject hapinestext;
    public GameObject nametext;
    public GameObject menupanel;
    public GameObject nameinput;
    public GameObject pet;
    public GameObject cherry;
    public GameObject minigamepanel;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    void Update(){
        hapinestext.GetComponent<Text>().text = "" + pet.GetComponent<PetScrip>().hapines;
        hungertext.GetComponent<Text>().text = "" + pet.GetComponent<PetScrip>().hunger;
        nametext.GetComponent<Text>().text = pet.GetComponent<PetScrip>().petname;
    }

    public void trigermainpanel(bool b){
            menupanel.SetActive(!menupanel.activeInHierarchy);
        if (b){
            pet.GetComponent<PetScrip>().petname = nameinput.GetComponent<TMP_InputField>().text;
            PlayerPrefs.SetString("name", pet.GetComponent<PetScrip>().petname);
        }
    }
    public void buttonbehaibor(int i)
    {        switch (i)
        {
            case 0:
                default:    
                break;
            case 1: //Food button
                if (!GameObject.Find("cherry(Clone)"))
                Instantiate(cherry, new Vector3 (Random.Range(-7,7), 0f, 0f), Quaternion.identity);
                break;
            case 2:
                minigamepanel.SetActive(!minigamepanel.activeInHierarchy);//Hapi button
                break;
            case 3: //Med button
                break;
            case 4: //Clean button
                break;
            case 5: //Quit button
                pet.GetComponent<PetScrip>().savepet();
                Application.Quit();
                break;
            case 6: //Mute button
                break;
            case 7:
                menupanel.SetActive(!menupanel.activeInHierarchy);
                PlayerPrefs.DeleteAll();//Reset menu button
                break;
        }
    }

}
