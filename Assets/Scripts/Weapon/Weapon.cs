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
    private GameObject ammo_prefab;
    private int weapon_ammo;
    public RangeWeapon(int weapon_damage, float weapon_cooldown, GameObject ammo_prefab, int weapon_ammo)
        : base(weapon_damage, weapon_cooldown)
    {
        this.ammo_prefab = ammo_prefab;
        this.weapon_ammo = weapon_ammo;
    }
    public override void Launch((Vector2, Vector2) input)
    {
        Debug.Log("I am doing range attack");    
    }
}
