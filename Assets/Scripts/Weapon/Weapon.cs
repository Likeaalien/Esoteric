using System;
using Unity.VisualScripting;
using UnityEngine;
public abstract class Weapon
{
    protected int weapon_damage;
    protected float weapon_cooldown;
    public Weapon(int weapon_damage, float weapon_cooldown)
    {
        this.weapon_damage = weapon_damage;
        this.weapon_cooldown = weapon_cooldown;
    }
    public abstract void Launch((Vector2, Vector2) input);
}
public class MeleeWeapon : Weapon
{
    private float melee_range;
    public MeleeWeapon(int weapon_damage, float weapon_cooldown, float melee_range)
        : base(weapon_damage, weapon_cooldown)
    {
        this.melee_range = melee_range;    
    }
    public override void Launch((Vector2, Vector2) input)
    {
        Debug.Log("I am doing melee" + weapon_damage);
        Physics2D.OverlapCircleAll(input.Item1 + input.Item2, melee_range);
    }
}
public class RangeWeapon : Weapon
{
    private GameObject projectile_prefab;
    private int weapon_ammo;
    private int projectile_velocity;
    public RangeWeapon(int weapon_damage, float weapon_cooldown, GameObject projectile_prefab, int weapon_ammo, int projectile_velocity)
        : base(weapon_damage, weapon_cooldown)
    {
        this.projectile_prefab = projectile_prefab;
        this.weapon_ammo = weapon_ammo;
        this.projectile_velocity = projectile_velocity;
    }
    public override void Launch((Vector2, Vector2) input)
    {
        GameObject rock_object = UnityEngine.Object.Instantiate(projectile_prefab, input.Item1 + input.Item2, Quaternion.identity);
        Rigidbody2D projectile = rock_object.GetComponent<Rigidbody2D>();
        projectile.AddForce(input.Item2.normalized * projectile_velocity);
        UnityEngine.Object.Destroy(rock_object, 3f);
    }
}