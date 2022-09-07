using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_AK : GunFire
{
    public Transform GetFirePos;


    private void MouseInput()
    {
        if (Input.GetButton("Fire1") && !b_isReloading && isAmmo())
        {
            Fire();
        }
        if (Input.GetButtonUp("Fire1") || !isAmmo())
        {
            MuzzleCtrl(false);
        }
        if (Input.GetKeyDown(KeyCode.R) && !b_isReloading)
        {
            Reload("Reload");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initial(30,20,100,2f,15f,0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        MouseInput();
        Run();
        Walk();
        AutoReload();
        CrossHairAniCtrl();
    }
}
