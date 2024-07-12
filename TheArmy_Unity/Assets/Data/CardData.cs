using UnityEngine;

[CreateAssetMenu(menuName = "Paper War/CardData")]
public class CardData : ScriptableObject
{
	public CardEnum Card;

	public string Title;

	public string Description;

	public CardRarity Rarity;

	public PolynomialData ValuePolynomialData;

	public int ValueFraction;

	public Sprite Sprite;

	private void OnValidate()
	{
	}
}
