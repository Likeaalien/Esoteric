using System;
using Unity.VisualScripting;
using UnityEngine;
public enum MeleeType
{
    Sharp,
    Tool
}
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
    public MeleeType weapon_type;
    public MeleeWeapon(int weapon_damage, float weapon_cooldown, float melee_range, MeleeType weapon_type)
        : base(weapon_damage, weapon_cooldown)
    {
        this.melee_range = melee_range;    
        this.weapon_type = weapon_type;
    }
    public override void Launch((Vector2, Vector2) input)
    {
        Debug.Log("I am doing melee" + weapon_damage);

        Collider2D[] hits = Physics2D.OverlapCircleAll(input.Item1 + input.Item2,melee_range);

        foreach (Collider2D hit in hits)
        {
            IHittable hittable = hit.GetComponent<IHittable>();

            if (hittable != null)
            {
                hittable.OnHit(this);
            }
        }
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
        float angle = Mathf.Atan2(input.Item2.y, input.Item2.x) * Mathf.Rad2Deg;
        GameObject game_object = UnityEngine.Object.Instantiate(projectile_prefab, input.Item1 + input.Item2, Quaternion.Euler(0, 0, angle));
        Rigidbody2D projectile = game_object.GetComponent<Rigidbody2D>();
        projectile.AddForce(input.Item2.normalized * projectile_velocity);
        UnityEngine.Object.Destroy(game_object, 3f);
    }
}