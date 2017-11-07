using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MassDriver : RangedWeapon_Class
{
    float Charge;
    float ChargeRate = 10;
    float RotationSpeed;
    int damageModl = 75;
    public bool refresh;

    public GameObject[] Spinners;

    void Start()
    {
        //StartCoroutine(ChargeUpRoutine());
    }

    IEnumerator ChargeUpRoutine()
    {
        refresh = true;
        if (currentWeaponState == State.Ready)
        {
            if (Charge < 100)
            {
                Charge = Charge + ChargeRate;
            }
        }

        refresh = false;
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ChargeUpRoutine());
        refresh = false;
    }

    void FixedUpdate()
    {
        if (refresh == false)
        {
            StartCoroutine(ChargeUpRoutine());
        }

        RotationSpeed = 2.5f * currentAmmo;

        foreach (GameObject x in Spinners)
        {
            int index = Array.IndexOf(Spinners, x) + 1;
            if (Charge > ((index * 10) + 10))
            {
                x.transform.Rotate(0, 0, 1 * RotationSpeed);
            }
        }

        if (currentWeaponState != State.Ready)
        {
            Charge = 0;
        }

        DamageModifer = Mathf.RoundToInt(damageModl * (Charge / 100));

        if (refresh == true)
        {
            refresh = false;
        }
    }

    public override void Setup()
    {
        weaponName = "Mass Driver";
        FactionTag = Faction.Robot;
        DamageModifer = 80;
        projectileSpeed = 20f;
        fireRate = 1;
        ProjectileSpread = 0;
        maxAmmoCapacity = 2;
        reserveAmmo = 8;
        maxReserveAmmoCapacity = 18;
        reloadSpeed = 5;
        currentAmmo = maxAmmoCapacity;
    }
}
