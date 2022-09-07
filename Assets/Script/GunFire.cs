using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunFire : MonoBehaviour
{
    private int n_MaxAmmo;
    private int n_HeadDamage;
    private int n_CurrentAmmo;
    private int n_Damage;
    private float f_Recoil;
    private float f_nextTimeToFire;
    private float f_Radius;
    private float f_FireRating;

    protected bool b_isReloading;

    private RaycastHit hitinfo;
    
    public GameObject GetBulletHoleGam;
    public GameObject GetBulletHoleMetalGam;
    public GameObject GetMuzzleGam;
    public TextMeshProUGUI GetMaxAmmoText;
    public TextMeshProUGUI GetCurrentAmmoText;


    private Animator GetCrossHairAni;
    private PlayerCtrl GetPlayerCtrl;
    private Animator GetHandAni;
    private MouseCtrl GetMouseCtrl;

    protected void Initial(int max_ammo, int dmg, int head_dmg, float recoil, float rating, float radius )
    {
        n_MaxAmmo = max_ammo;
        n_CurrentAmmo = max_ammo;
        n_Damage = dmg;
        n_HeadDamage = head_dmg;
        f_Recoil = recoil;
        f_FireRating = rating;
        f_Radius = radius;
        b_isReloading = false;
        GetPlayerCtrl = GetComponentInParent<PlayerCtrl>();
        GetHandAni = GetComponent<Animator>();
        GetMouseCtrl = GetComponentInParent<MouseCtrl>();
        GetCrossHairAni = GameObject.Find("CrossHair").GetComponent<Animator>();
        f_nextTimeToFire = 0f;
        GetMaxAmmoText.text = n_MaxAmmo.ToString();
        GetCurrentAmmoText.text = n_CurrentAmmo.ToString();
    }

    protected bool isAmmo()
    {
        return n_CurrentAmmo > 0 ? true : false;
    }
    
    protected void Fire()
    {
        if(Time.time >= f_nextTimeToFire)
        {
            MuzzleCtrl(true);
            Recoil();
            GetHandAni.CrossFadeInFixedTime("Fire", 0.01f);
            n_CurrentAmmo -= 1;
            GetCurrentAmmoText.text = n_CurrentAmmo.ToString();
            f_nextTimeToFire = Time.time + 1f / f_FireRating;
            if (Physics.Raycast(GetMouseCtrl.transform.position, RandomSpray(), out hitinfo, 100f))
            {

                if (hitinfo.transform.CompareTag("Wall") || hitinfo.transform.CompareTag("Ground"))
                {
                    Instantiate(GetBulletHoleGam, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                }

                else if (hitinfo.transform.CompareTag("Metal"))
                {
                    Instantiate(GetBulletHoleMetalGam, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));

                }
            }
        }

    }


    protected void MuzzleCtrl(bool isFire)
    {
        GetMuzzleGam.SetActive(isFire);
    }

    protected void Walk()
    {
        GetHandAni.SetBool("isWalk", GetPlayerCtrl.isMoving());
    }

    protected void Run()
    {
        GetHandAni.SetBool("isRun", GetPlayerCtrl.isRunning());
    }

    protected void CrossHairAniCtrl()
    {
        GetCrossHairAni.SetBool("isMove", GetPlayerCtrl.isMoving());
    }

    protected void Reload(string name)
    {
        b_isReloading = true;
        GetHandAni.SetTrigger(name);
    }

    public void EndReload()
    {
        n_CurrentAmmo = n_MaxAmmo;
        GetCurrentAmmoText.text = n_CurrentAmmo.ToString();
        b_isReloading = false;
    }

    protected void AutoReload()
    {
        if(n_CurrentAmmo <= 0 && !b_isReloading)
        {
            Reload("Reload_2");
        }
    }

    private void Recoil()
    {
        GetMouseCtrl.Set_Y_Recoil(f_Recoil);
    }

    private Vector3 RandomSpray()
    {
        float radius;
        if (GetPlayerCtrl.isMoving())
        {
            radius = f_Radius + 0.2f;
        }
        else
        {
            radius = f_Radius;
        }
        Vector3 spray = Random.insideUnitCircle * radius;
        spray += GetMouseCtrl.transform.forward;
        spray = spray.normalized;
        return spray;

    }
}
