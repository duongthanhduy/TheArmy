using UnityEngine;

[CreateAssetMenu(menuName = "Paper War/PrestigeBadgeData")]
public class WorkshopUpgradeData : ScriptableObject
{
	public WorkshopUpgrade WorkshopUpgrade;

	public bool IsOverrideCost;

	public string Title;

	public string Description;

	public string ValueFormat;

	public string Prefix;

	public string Suffix;

	public int ValueFraction;

	public PolynomialData CostPolynomailData;

	public PolynomialData ValuePolynomialData;

	public Sprite Sprite;

	public int MaxLevel;

	private void OnValidate()
	{
	}
}
