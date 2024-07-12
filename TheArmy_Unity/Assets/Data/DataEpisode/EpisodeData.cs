using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paper War/EpisodeData")]
public class EpisodeData : ScriptableObject
{
	public Color EnemyColor;

	public List<UnitData> Enemies;

	public Sprite Sprite;

	public BigDouble StartHP;

	public float LevelGrowthFactor;
}
