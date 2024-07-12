using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paper War/WorkshopUpgradeGroupData")]
public class WorkshopUpgradeGroupData : ScriptableObject
{
	public WorkshopCategory WorkshopCategory;

	public string Title;

	public BigDouble Cost;

	public List<WorkshopUpgradeData> WorkshopUpgradeDatas;
}
