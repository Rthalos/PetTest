using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class GameManager : MonoBehaviour
{
    public GameObject hungertext;
    public GameObject hapinestext;
    public GameObject nametext;
    public GameObject menupanel;
    public GameObject nameinput;
    public GameObject pet;
    public GameObject cherryPrefab;
    private GameObject cherryEnEscena;
    public GameObject minigamepanel;
    public GameObject shit;
    private GameObject shitenescena;
    private List<GameObject> shitsPrefabs;
    public List<GameObject> needs;

    private void Start()
    {
        shitsPrefabs = new List<GameObject>();
        Application.targetFrameRate = 60;
        InvokeRepeating("generarkk", 0f, 600f);
    }
    void Update() {
        int hapi = pet.GetComponent<PetScrip>().hapines;
        int hun = pet.GetComponent<PetScrip>().hunger;

        hapinestext.GetComponent<Text>().text = "" + hapi;
        hungertext.GetComponent<Text>().text = "" + hun;
        nametext.GetComponent<Text>().text = pet.GetComponent<PetScrip>().petname;
        if (hapi <= 49)
            needs[0].SetActive(true);
        else needs[0].SetActive(false);
        if (hun <= 49)
            needs[1].SetActive(true);
        else needs[1].SetActive(false);
        if (shitsPrefabs.Count > 0)
            needs[2].SetActive(true);
        else needs[2].SetActive(false);
    }

    public void trigermainpanel(bool b) {
        menupanel.SetActive(!menupanel.activeInHierarchy);
        if (b) {
            pet.GetComponent<PetScrip>().petname = nameinput.GetComponent<TMP_InputField>().text;
            PlayerPrefs.SetString("name", pet.GetComponent<PetScrip>().petname);
            nameinput.GetComponent<TMP_InputField>().text = null;
        }
    }
    public void buttonbehaibor(int i)
    { switch (i)
        {
            case 0:
            default:
                break;
            case 1: //Food button
                if (cherryEnEscena == null)
                    cherryEnEscena = (GameObject)Instantiate(cherryPrefab, new Vector3(Random.Range(-3.3f, 3.3f), 0f, 0f), Quaternion.identity);
                break;
            case 2:
                minigamepanel.GetComponent<ShellGame>().verpanel();//Hapi button
                break;
            case 3: //Med button
                break;
            case 4:
                if (shitsPrefabs.Count > 0)//Clean button
                    for (int t = 0; t < shitsPrefabs.Count; t++)
                    {
                        Destroy(shitsPrefabs[t]);
                    }
                shitsPrefabs.Clear();
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
            case 8:
                menupanel.SetActive(!menupanel.activeInHierarchy);
                break;
        }
    }

    private void generarkk()
    {
        if (shitsPrefabs.Count <= 0)
        {
            for (int i = 0; i < pet.GetComponent<PetScrip>().cantidadCacas(); i++)
            {
                shitenescena = (GameObject)Instantiate(shit, new Vector3(UnityEngine.Random.Range(-5, 5), -4f, 0f), Quaternion.identity);
                shitsPrefabs.Add(shitenescena);
            }
        }

    }

}
