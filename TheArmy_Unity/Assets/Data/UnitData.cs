using UnityEngine;

[CreateAssetMenu(menuName = "Paper War/UnitData")]
public class UnitData : ScriptableObject
{
	public int UnitId;

	public string Name;

	public string Description;

	public float Radius;

	public float BasePower;

	public float BaseHealth;

	public float BaseAttackSpeed;

	public float MoveSpeed;

	public float RotateSpeed;

	public float AttackDistance;

	public float ProjectileSpeed;

	public float AreaDamageRadius;

	public Sprite Sprite;

	public GameObject Prefab;

	public GameObject ProjectilePrefab;

	public CharacterType CharacterType;

	public UnitType DataType;
}
