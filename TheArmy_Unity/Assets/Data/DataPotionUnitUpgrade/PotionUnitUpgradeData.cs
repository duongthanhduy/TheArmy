using UnityEngine;

[CreateAssetMenu(menuName = "Paper War/PotionUnitUpgradeData")]
public class PotionUnitUpgradeData : ScriptableObject
{
	public enum PotionUpgrade
	{
		WarriorPower = 0,
		WarriorHealth = 1,
		WarriorSplashDamage = 2,
		WarriorShield = 3,
		WarriorUnlimitedShield = 4,
		ArcherPower = 5,
		ArcherHealth = 6,
		ArcherTimeBomb = 7,
		ArcherTimeBombExpansion = 8,
		ArcherProgrammedTimeBomb = 9,
		BladePower = 10,
		BladeHealth = 11,
		BladeSplashDamage = 12,
		BladeStun = 13,
		BladeStunDuration = 14,
		WizardPower = 15,
		WizardHealth = 16,
		WizardPoison = 17,
		WizardPoisonPower = 18,
		WizardStun = 19,
		WizardStunDuration = 20,
		HealerPower = 21,
		HealerHealth = 22,
		HealerTeamHeal = 23,
		HealerTeamHealPower = 24
	}

	public PotionUpgrade Upgrade;

	public string Title;

	public string Description;

	public PolynomialData ValuePolynomialData;

	public int MaxLevel;
}
