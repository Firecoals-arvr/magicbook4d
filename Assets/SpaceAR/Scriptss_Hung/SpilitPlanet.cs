using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpilitPlanet : DefaultTrackableEventHandler
{
    [SerializeField] UIButton spilitButton;
    private GameObject[] planet;
    private GameObject[] _parentPlanet;
    bool check = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        planet = GameObject.FindGameObjectsWithTag("childplanet");
        _parentPlanet = GameObject.FindGameObjectsWithTag("Planet");
        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        AutoClick();
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        check = false;
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        check = true;
    }

    void RunAnimationOpenPlanet()
    {
        if (check == true)
        {
            for (int i = 0; i < planet.Length; i++)
            {
                if (planet[i].gameObject.transform.parent.name == _parentPlanet[i].name)
                {
                    planet[i].GetComponent<Animation>().Play("Open");
                    check = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < planet.Length; i++)
            {
                if (planet[i].gameObject.transform.parent.name == _parentPlanet[i].name)
                {
                    planet[i].GetComponent<Animation>().Play("Close");
                    check = true;
                }
            }
        }
    }

    public void AutoClick()
    {
        spilitButton.onClick.Clear();
        EventDelegate eventdel = new EventDelegate(this, "RunAnimationOpenPlanet");
        EventDelegate.Set(spilitButton.onClick, eventdel);
        //EventDelegate.Add(spilitButton.onClick, eventdel);
    }

}
