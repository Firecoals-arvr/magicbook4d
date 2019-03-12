using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setdefaulspeed : MonoBehaviour
{

    // Use this for initialization
    // khai bao cac planet
    public GameObject Earth;
    public GameObject Earth1;
    public GameObject Mercury;
    public GameObject Mercury1;
    public GameObject Venus;
    public GameObject Venus1;
    public GameObject Mars;
    public GameObject Mars1;
    public GameObject Jupiter;
    public GameObject Jupiter1;
    public GameObject Saturn;
    public GameObject Saturn1;
    public GameObject Uranus;
    public GameObject Uranus1;
    public GameObject Neptune;
    public GameObject Neptune1;
    public GameObject Moon;
    public GameObject alignment;

    //khai bao toc do mac dinh cua cac planet
    const float speedrunearth = -1f;
    const float speedrotateearth = 20f;
    const float speedrunMercury = -2.13f;
    const float speedrotateMercury = 30f;
    const float speedrunvenus = -1.8f;
    const float speedrotatevenus = 17f;
    const float speedrunmars = -0.6f;
    const float speedrotatemars = 20f;
    const float speedrunjupiter = -1f;
    const float speedrotatejupiter = 48f;
    const float speedrunsaturn = -1.6f;
    const float speedrotatesaturn = 47f;
    const float speedrunuranus = -1.2f;
    const float speedrotateuranus = 28f;
    const float speedrunneptune = -1f;
    const float speedrotateneptune = 32f;
    const float speedrunmoon = 6.6f;

    //khai bao compoment
    Autorun mercuryrun;
    Autorun venusrun;
    Autorun earthrun;
    Autorun marsrun;
    Autorun jupiterrun;
    Autorun saturnrun;
    Autorun uranusrun;
    Autorun neptunerun;
    Autorun moonrun;

    Autorotate mercuryrotate;
    Autorotate venusrotate;
    Autorotate earthrotate;
    Autorotate marsrotate;
    Autorotate jupiterrotate;
    Autorotate saturnrotate;
    Autorotate uranusrotate;
    Autorotate neptunerotate;
    BoxCollider colliderr;

    SphereCollider Earthcollider;
    SphereCollider Mercurycollider;
    SphereCollider Venuscollider;
    SphereCollider Marscollider;
    SphereCollider Jupitercollider;
    SphereCollider Saturnscollider;
    SphereCollider Uranuscollider;
    SphereCollider Neptunecollider;



    void Start()
    {
        //khai bao cac compomnet cua cac planet
        //venusrun = new Autorun();
        //venusrotate = new Autorotate();
        venusrun = Venus.GetComponent<Autorun>();
        venusrotate = Venus1.GetComponent<Autorotate>();
        venusrun.speed = speedrunvenus;
        venusrotate.speed = speedrotatevenus;

        //mercuryrun = new Autorun();
        //mercuryrotate = new Autorotate();
        mercuryrun = Mercury.GetComponent<Autorun>();
        mercuryrotate = Mercury1.GetComponent<Autorotate>();
        mercuryrun.speed = speedrunMercury;
        mercuryrotate.speed = speedrotateMercury;



        //earthrun = new Autorun();
        //earthrotate = new Autorotate();
        earthrun = Earth.GetComponent<Autorun>();
        earthrotate = Earth1.GetComponent<Autorotate>();
        earthrun.speed = speedrunearth;
        earthrotate.speed = speedrotateearth;


        //marsrun = new Autorun();
        //marsrotate = new Autorotate();
        marsrun = Mars.GetComponent<Autorun>();
        marsrotate = Mars1.GetComponent<Autorotate>();
        marsrun.speed = speedrunmars;
        marsrotate.speed = speedrotatemars;

        //jupiterrun = new Autorun();
        //jupiterrotate = new Autorotate();
        jupiterrun = Jupiter.GetComponent<Autorun>();
        jupiterrotate = Jupiter1.GetComponent<Autorotate>();
        jupiterrun.speed = speedrunjupiter;
        jupiterrotate.speed = speedrotatejupiter;

        //saturnrun = new Autorun();
        //saturnrotate = new Autorotate();
        saturnrun = Saturn.GetComponent<Autorun>();
        saturnrotate = Saturn1.GetComponent<Autorotate>();
        saturnrun.speed = speedrunsaturn;
        saturnrotate.speed = speedrotatesaturn;

        //uranusrun = new Autorun();
        //uranusrotate = new Autorotate();
        uranusrun = Uranus.GetComponent<Autorun>();
        uranusrotate = Uranus1.GetComponent<Autorotate>();
        uranusrun.speed = speedrunuranus;
        uranusrotate.speed = speedrotateuranus;

        //neptunerun = new Autorun();
        //neptunerotate = new Autorotate();
        neptunerun = Neptune.GetComponent<Autorun>();
        neptunerotate = Neptune1.GetComponent<Autorotate>();
        neptunerun.speed = speedrunneptune;
        neptunerotate.speed = speedrotateneptune;

        //moonrun = new Autorun();
        moonrun = Moon.GetComponent<Autorun>();
        moonrun.speed = speedrunmoon;
        //khai bao collider cua alignment
        colliderr = alignment.GetComponent<BoxCollider>();

        Earthcollider = Earth1.GetComponent<SphereCollider>();
        Mercurycollider = Mercury1.GetComponent<SphereCollider>();
        Venuscollider = Venus1.GetComponent<SphereCollider>();
        Marscollider = Mars1.GetComponent<SphereCollider>();
        Jupitercollider = Jupiter1.GetComponent<SphereCollider>();
        Saturnscollider = Saturn1.GetComponent<SphereCollider>();
        Uranuscollider = Uranus1.GetComponent<SphereCollider>();
        Neptunecollider = Neptune1.GetComponent<SphereCollider>();
    }
    public Slider sli;
    public void changeslider()
    {
        //neu changes slider thi alignment = false
        colliderr.enabled = false;
        //Toc do * changeslider
        moonrun.speed = speedrunmoon * sli.value;
        neptunerun.speed = speedrunneptune * sli.value;
        neptunerotate.speed = speedrotateneptune * sli.value;
        uranusrun.speed = speedrunuranus * sli.value;
        uranusrotate.speed = speedrotateuranus * sli.value;
        saturnrun.speed = speedrunsaturn * sli.value;
        saturnrotate.speed = speedrotatesaturn * sli.value;
        jupiterrun.speed = speedrunjupiter * sli.value;
        jupiterrotate.speed = speedrotatejupiter * sli.value;
        marsrun.speed = speedrunmars * sli.value;
        marsrotate.speed = speedrotatemars * sli.value;
        mercuryrun.speed = speedrunMercury * sli.value;
        mercuryrotate.speed = speedrotateMercury * sli.value;
        earthrun.speed = speedrunearth * sli.value;
        earthrotate.speed = speedrotateearth * sli.value;
        venusrun.speed = speedrunvenus * sli.value;
        venusrotate.speed = speedrotatevenus * sli.value;


        Earthcollider.enabled = true;
        Mercurycollider.enabled = true;
        Venuscollider.enabled = true;
        Marscollider.enabled = true;
        Jupitercollider.enabled = true;
        Saturnscollider.enabled = true;
        Uranuscollider.enabled = true;
        Neptunecollider.enabled = true;
    }
    private bool statusaligment = false;
    // click button aligment
    public void clickbutton()
    {
        // click button aligment thi alignment = true
        if (statusaligment == false)
        {
            colliderr.enabled = true;
            Earthcollider.enabled = false;
            Mercurycollider.enabled = false;
            Venuscollider.enabled = false;
            Marscollider.enabled = false;
            Jupitercollider.enabled = false;
            Saturnscollider.enabled = false;
            Uranuscollider.enabled = false;
            Neptunecollider.enabled = false;
            statusaligment = true;
        }
        else
        {
            colliderr.enabled = false;
            Earthcollider.enabled = true;
            Mercurycollider.enabled = true;
            Venuscollider.enabled = true;
            Marscollider.enabled = true;
            Jupitercollider.enabled = true;
            Saturnscollider.enabled = true;
            Uranuscollider.enabled = true;
            Neptunecollider.enabled = true;
            changeslider();
            statusaligment = false;
        }

    }

}
