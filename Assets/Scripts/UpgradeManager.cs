using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour {

	[SerializeField]
	Text megaBallLevelText;
	[SerializeField]
	Text wallLevelText;
	[SerializeField]
	Text gripLevelText;
	[SerializeField]
	Text IncreaseSizeLevelText;
	[SerializeField]
	Text totalScoreText;
	[SerializeField]
	QuestionDialog questionDialog;
	[SerializeField]
	MessageDialog messageDialog;
	[SerializeField]
	LanguageManager languageManager;

	string selectedUpgrade;

	public void upgrade(PowerUp powerUp, Text txt)
	{
		if (Global.TotalStars >= powerUp.Cost)
		{
			Global.TotalStars -= powerUp.Cost;
			PlayerPrefs.SetInt("Stars", Global.TotalStars);
			totalScoreText.text = "" + Global.TotalStars;

			powerUp.Upgrade();
			txt.text = "" + powerUp.Level;

			
		} else {
			messageDialog.Popup(languageManager.GetTextByValue("NotEnought"));
		}
	}

	public void upgrade_btn(string selectedUpgrade) {
		this.selectedUpgrade = selectedUpgrade;
		if (selectedUpgrade == "MegaBall")
		{
			questionDialog.Popup(languageManager.GetTextByValue("Upgrade") + "\n" + Global.MegaBall.Cost + " ✯", yes_btn);
		}
		else if (selectedUpgrade == "Wall")
		{
			questionDialog.Popup(languageManager.GetTextByValue("Upgrade") + "\n" + Global.Wall.Cost + " ✯", yes_btn);
		}
		else if (selectedUpgrade == "Grip")
		{
			questionDialog.Popup(languageManager.GetTextByValue("Upgrade") + "\n" + Global.Grip.Cost + " ✯", yes_btn);
		}
		else if (selectedUpgrade == "IncreaseSize")
		{
			questionDialog.Popup(languageManager.GetTextByValue("Upgrade") + "\n" + Global.IncreaseSize.Cost + " ✯", yes_btn);
		}
	}

	int yes_btn() {
		if (selectedUpgrade == "MegaBall")
		{
			upgrade (Global.MegaBall, megaBallLevelText);
		}
		else if (selectedUpgrade == "Wall")
		{
			upgrade (Global.Wall, wallLevelText);
		}
		else if (selectedUpgrade == "Grip")
		{
			upgrade (Global.Grip, gripLevelText);
		}
		else if (selectedUpgrade == "IncreaseSize")
		{
			upgrade (Global.IncreaseSize, IncreaseSizeLevelText);
		}

		return 0;
	}
}
