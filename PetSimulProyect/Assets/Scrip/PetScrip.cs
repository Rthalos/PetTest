using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

public class PetScrip : MonoBehaviour
{
    [SerializeField]
    private int _hunger; //que tan habriento esta la pet (0 a 100)
    [SerializeField]
    private int _hapines; //que tanta felicidad tiene la pet (0 a 100)
    [SerializeField]
    private string _name;
    public GameObject shitPrefab;
    private GameObject shitEnEscena;

    private int direction; //la direccion inicia en 0 y se usa para indicar el movimineto de la pet en la pantalla (0=derecha, 1=izquierda)
    private int _clickCount;
    private readonly bool servertime;

    private Transform target;

    public float speed = 200f;
    public float nextWaypointDistance;

    Path path;
    int currentwaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(PlayerPrefs.GetString("date")+ "esta es la hora guardada");       //test
        //Debug.Log(PlayerPrefs.GetInt("_hunger")+"esta es la comida guardada");      //test
        //Debug.Log(PlayerPrefs.GetInt("_hapines")+"esta es la felicidad guardada");  //test
        InvokeRepeating("updatepath", 0f, 0.1f);
        direction = 1;
        //PlayerPrefs.SetString("date", "01/09/2022 15:20:12"); //inicializa el tiempo para el juego (de manera forzada borrar al terminar)
        if (!PlayerPrefs.HasKey("name") || PlayerPrefs.GetString("name")==null)
        {
            PlayerPrefs.SetString("name", "Crab");
        }
        InvokeRepeating("updatestatus", 0f, 5f);//metodo controlador de los valores numericos de la pet pj
        _name = PlayerPrefs.GetString("name");
        _hunger = PlayerPrefs.GetInt("hunger");
        _hapines = PlayerPrefs.GetInt("hapines");
    }

    // Update is called once per frame
    void FixedUpdate(){   
        getclicked();
        movement();
        //Debug.Log("el current waypoint es: " + currentwaypoint);
        //if (path != null)
        //Debug.Log("el path vector waypoint es: " + path.vectorPath.Count);

    }

    void updatepath () 
    {
        GameObject cherry = GameObject.Find("cherry(Clone)");
        if (cherry!=null)
        {
            target = cherry.transform;
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
                direction = 2;
            }
        }      
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentwaypoint = 0;
        }

    }
    void updatestatus()
    {
        if (!PlayerPrefs.HasKey("hunger")) // actualiza el valor de alimento en el savelog del juego
        {
            //Debug.Log("comida existe?:" + PlayerPrefs.HasKey("hunger"));
            _hunger = 100;
            PlayerPrefs.SetInt("hunger", _hunger);
        }
        else {
            _hunger = PlayerPrefs.GetInt("hunger"); 
            //Debug.Log("comida existe?:" + PlayerPrefs.HasKey("hunger") + "----valor es: "+_hunger);
        }

        if (!PlayerPrefs.HasKey("hapines"))    // actualiza el valor de felicidad en el savelog del juego
        {
            //Debug.Log("felicidad existe?:" + PlayerPrefs.HasKey("hapines"));
            _hapines = 100;
            PlayerPrefs.SetInt("hapines", _hapines);
        }
        else
        {
            _hapines = PlayerPrefs.GetInt("hapines");
            //Debug.Log("Felicidad existe?:" + PlayerPrefs.HasKey("hapines") + "-----el valor es:" +_hapines);
        }

        if (!PlayerPrefs.HasKey("DateOfBirth")) {                     //obtiene el valor del tiempo del savelog del juego
            PlayerPrefs.SetString("DateOfBirth", getstringtime());    //Fechaa en la que se creo la pet
            PlayerPrefs.SetString("DateOfFeed", getstringtime());     //fecha en la que se alimento la pet
            PlayerPrefs.SetString("PlayingDate", getstringtime());    //fecha en la que se jugo con la pet
        }

        TimeSpan df = getTimeSpan(PlayerPrefs.GetString("DateOfFeed"));

        if (df.TotalHours >= 1) { 
        _hunger = _hunger - (int)(df.TotalHours * 4);
        }
        if (_hunger <= 0)
        {
            _hunger = 0;
        }
        TimeSpan py = getTimeSpan(PlayerPrefs.GetString("PlayingDate"));
        if (py.TotalHours >= 1)
        {
            _hapines =_hapines - ((int)((100 - _hunger) * (py.TotalHours / 2)));
        }
        if (_hapines <= 0)
            {
            _hapines = 0;
            }
        //Debug.Log(getTimeSpan() + "Este es el lapso de tiempo completo");
        //Debug.Log(PlayerPrefs.GetInt("_hunger")+"esta es la comida guardada");      //test
        //Debug.Log(PlayerPrefs.GetInt("_hapines")+"esta es la felicidad guardada");  //test
        //Debug.Log(getTimeSpan().ToString()); //debug para probar el tiempo
        //Debug.Log(getTimeSpan().TotalHours+"Estas son las horas"); //debug para probar el tiempo
        //Debug.Log((int)ts.TotalHours+"Estas son las horas en int");
        //Debug.Log((int)ts.TotalHours*4 +"Estas son las horas *4");
        //Debug.LogError((_hunger - (int)ts.TotalHours * 4) + "Estas son restadas al alimento");  
        //if (servertime)
        //{
        //    updateServer();
        //}
        //else
        //{
        //    updateDevice();   // guarda el valor del tiempo en el savelog del juego (hora de equipo)
        ////}
    }
    void updateServer() // guarda el valor del tiempo en el savelog del juego (hora de un server)
    {
    }

    void updateDevice() // guarda el valor del tiempo en el savelog del juego (hora de equipo)
    {
        //Debug.Log(getstringtime()+ "Save pet (o por lo menos la hora) y la hora es:");
       // Debug.Log(getTimeSpan()+"");
        PlayerPrefs.SetInt("hapines", _hapines);
        PlayerPrefs.SetInt("hunger", _hunger);  
        //Debug.Log(PlayerPrefs.GetInt("hunger", _hunger)+"= Hunger");
        //Debug.Log(PlayerPrefs.GetInt("hapines", _hapines) + "= Hapines");
        //Debug.LogError("luego del guardado");
    }
    TimeSpan getTimeSpan(string fecha)  // obtiene el tiempo transcurrido desde la hora guardada en el savelog (date) y el valor actual now del metodo getstringtime
    { if (servertime)
            return new TimeSpan();
        else
            return DateTime.Now - Convert.ToDateTime(fecha);
    }
    string getstringtime() // obtiene el valor del tiempo del juego
    {
        DateTime now = DateTime.Now;
        return now.Day + "/" + now.Month + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
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
    public string petname // valor publico para poder trabajar el nombre de la pet
    {
        get { return _name; }
        set { _name = value; }
    }
    public void updatehapines(int i) {
        _hapines += i;
        if (_hapines > 100)
        {
            _hapines = 100;
        }
        PlayerPrefs.SetInt("hapines", _hapines);
        PlayerPrefs.SetString("PlayingDate", getstringtime());
    } //aumenta los valores de felicidad
    public void updatehunger(int i)
    {
        _hunger += i;
        if (_hunger > 100)
        {
            _hunger = 100;
        }
        PlayerPrefs.SetInt("hunger", _hunger);
        PlayerPrefs.SetString("DateOfFeed", getstringtime());
    }   // aumenta los valores de comida
    public void getclicked()//detecta la ubicaion en la pantalla de la pet y aumenta la felicidad con min 3 clicks, usa "updatehapines"
    {
        GetComponent<Animator>().SetBool("Touch", gameObject.transform.position.y > -2.9f);
        if (Input.GetMouseButtonUp(0)) 
        {
            //Debug.Log("Clicked");  // TEST

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            //Debug.DrawRay(mousePos2D, Vector2.zero, Color.red, 100f, false); //TEST
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
               // Debug.Log(hit.collider.gameObject.name);  // TEST
                hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }

            //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));  // TEST

            if (hit)
            {
                //Debug.Log($"Hit: " +  hit.transform.gameObject.name);  // TEST
                //Debug.Log($"Hit obj: " +  hit.transform.gameObject.tag);  // TEST
                if (hit.transform.gameObject.tag == "Pets")
                {
                    _clickCount++;
                    //Debug.Log($"Click Count: " + _clickCount);  // TEST
                    if (_clickCount >= 3)
                    {
                        _clickCount = 0;
                        updatehapines(1);
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100));
                    }
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Right")
        {
            direction = -1;
            //Debug.Log("Collision con el limite:" + collision.gameObject.name);
        }
        if (collision.gameObject.name == "Left")
        {
            direction = 1;
            //Debug.Log("Collision con el limite:" + collision.gameObject.name);
        }
        if (collision.gameObject.tag == "Shit")
        {
            if (direction == 1)
                direction = -1;
            else
                direction = 1;
        }
    }

    private void distanceWall()
    {
        float _distance1 = Vector2.Distance((Vector2)this.transform.position, (Vector2)(GameObject.Find("Right").GetComponent<Transform>().transform.position));
        float _distance2 = Vector2.Distance((Vector2)this.transform.position, (Vector2)(GameObject.Find("Left").GetComponent<Transform>().transform.position));
        if (_distance1 > _distance2)
            direction = -1;
        else direction = 1;

    }
    public void movement() //movimiento de la pet en pantalla
    {
        if (path == null)
        {
            gameObject.transform.Translate(Vector3.right *direction* Time.deltaTime);
            if (direction == 1)
            { 
               gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (direction ==-1)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            return;
        }

        if (currentwaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return; 
        }else
        {
            reachedEndOfPath = false;
        }
        Vector2 Pdirection = ((Vector2)path.vectorPath[currentwaypoint]- rb.position).normalized;
        Vector2 force = Pdirection * speed* Time.deltaTime;

        rb.AddForce(force,ForceMode2D.Force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentwaypoint]);
        //Debug.Log("distacia entre el nodo mas cercano y yo  " + distance);
        if (distance < nextWaypointDistance)
        {
            currentwaypoint++; 
        }
        //Debug.Log("el currentwaypoint es: "+currentwaypoint);
        //Debug.Log("el path vector count es de: " + path.vectorPath.Count);
    }

    public void savepet(){   
        if (!servertime)
        updateDevice();
    }

    public int cantidadCacas()
    {
        TimeSpan df = getTimeSpan(PlayerPrefs.GetString("DateOfFeed"));
        int cacas = (int)(df.TotalHours / 6); 
        return cacas;
    }
    public void LlamadoDeFruta(int newhunger)
    {
        updatehunger(newhunger);
        path.vectorPath.Clear();
        path = null;
        seeker = null;
        distanceWall();
    }
}
