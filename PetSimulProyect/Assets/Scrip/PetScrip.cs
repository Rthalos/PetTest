using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PetScrip : MonoBehaviour
{
    [SerializeField]
    private int _hunger; //que tan habriento esta la pet (0 a 100)
    [SerializeField]
    private int _hapines; //que tanta felicidad tiene la pet (0 a 100)


    private bool servertime;
        
    // Start is called before the first frame update
    void Start() 
    {
        PlayerPrefs.SetString("then", "25/08/2022 11:20:12"); //inicializa el tiempo para el juego (de manera forzada borrar al terminar)
        updatestatus(); //metodo controlador de los valores numericos de la pet pj
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void updatestatus() 
    {
        if (!PlayerPrefs.HasKey("_hunger")) // actualiza el valor de alimento en el savelog del juego
        {
            _hunger = 100;
            PlayerPrefs.SetInt("_hunger", _hunger);
        }
        else {
            _hunger = PlayerPrefs.GetInt("_hunger");
        }

        if (!PlayerPrefs.HasKey("_hapines"))    // actualiza el valor de felicidad en el savelog del juego
        {
            _hapines = 100;
            PlayerPrefs.SetInt("_hapines", _hapines);
        }
        else
        {
            _hapines = PlayerPrefs.GetInt("_hapines");
        }

        if (!PlayerPrefs.HasKey("then"))                    // obtiene el valor del tiempo del savelog del juego
            PlayerPrefs.SetString("then", getstringtime());

        TimeSpan ts = getTimeSpan();
        _hunger -= (int)(ts.TotalHours * 4);
        if (_hunger <= 0)
            _hunger = 0;

        _hapines -= (int)((100 - _hunger) * (ts.TotalHours / 2));
        if (_hapines <= 0)
            _hapines = 0;

        //Debug.Log(getTimeSpan().ToString()); //debug para probar el tiempo
        //Debug.Log(getTimeSpan().TotalHours); //debug para probar el tiempo

        if (servertime)
        {
            updateServer();
        }
        else
            {
            InvokeRepeating("updateDevice", 0f, 30f);   // guarda el valor del tiempo en el savelog del juego (hora de equipo)
        }
    }
    void updateServer() // guarda el valor del tiempo en el savelog del juego (hora de un server)
    { 
    }

    void updateDevice() // guarda el valor del tiempo en el savelog del juego (hora de equipo)
    {
        PlayerPrefs.SetString("then", getstringtime());
    }
    TimeSpan getTimeSpan()  // obtiene el tiempo transcurrido desde la hora guardada en el savelog (then) y el valor actual now del metodo getstringtime
    {   if (servertime)
            return new TimeSpan();
        else
            return DateTime.Now - Convert.ToDateTime( PlayerPrefs.GetString("then"));
    }
    string getstringtime() // obtiene el valor del tiempo del juego
    {
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }
    public int hunger   // valor publico para poder trabajar el hambre de la pet
    { 
        get { return _hunger; }
        set { _hunger = value; }
    }

    public int hapines // valor publico para poder trabajar la felicidad de la pet
    {
        get { return _hapines; }
        set { _hapines = value; }
    }
}
