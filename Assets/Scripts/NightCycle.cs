using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCycle : MonoBehaviour
{
    public float timeDilation;
    public GameObject timeObject;
    public Clock clock;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        clock = timeObject.GetComponent<Clock>();
        timeDilation = clock.dilation;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeDilation = clock.dilation;
        anim.speed = timeDilation;
        if(clock.currentHours == 18)
        {
            anim.SetBool("isNight",true);
        }

        if (clock.currentHours == 4)
        {
            anim.SetBool("isNight", false);
        }
    }
}
