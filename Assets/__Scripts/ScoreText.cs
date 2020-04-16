using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour
{
	//Public Variables
	#region 
	public string score = null;

	//Initial Color Settings
	public byte r = 0;
	public byte g = 0;
	public byte b = 0;
	public bool isDefaultColor = true;

	//Modes of text Visualization
	//0 - Color Cycle, 1 - Black and White Fade, 2 - Transparency Fade, 3 - Color Cycle and Black and White Fade, 4 - Color Cycle and Transparency Fade
	public int mode = 0;
	public int delay = 5;
	#endregion

	Color color;
	Color itemColor;
	float cycle;
	byte counter = 0;
	byte cycleCounter = 0;
	bool isAscCounter = true;
	private Text scoreText = null;

	// Use this for initialization
	void Start()
	{
		//text.text = score;
		scoreText = GetComponent<Text>();
		score = ScoreKeeper.score.ToString();
		ScoreKeeper.ResetScore();
		itemColor = scoreText.color;

		//Initialize with values set in Editor
		color.a = 1;
		color.r = itemColor.r;
		color.g = itemColor.g;
		color.b = itemColor.b;

	}

	// Update is called once per frame
	void Update()
	{

		if (delay == 0)
		{
			Debug.LogError("Delay is set to 0, which may result in divide by 0 error.");
		}

		else
		{
			if ((counter % delay) == 0)
			{
				if (mode == 0)
				{
					ChangeColor();
				}

				else if (mode == 1)
				{
					BlackWhiteCycle();
				}

				else if (mode == 2)
				{
					StaticColor(isDefaultColor);
					TransparencyCycle();
				}

				else if (mode == 3)
				{
					ChangeColor();
					BlackWhiteCycle();
				}

				else if (mode == 4)
				{
					ChangeColor();
					TransparencyCycle();
				}
			}
		}

		++counter;
	}

	void ChangeColor()
	{
		cycle = Random.Range(0, 256);
		color.r = cycle / 256;
		cycle = Random.Range(0, 256);
		color.g = cycle / 256;
		cycle = Random.Range(0, 256);
		color.b = cycle / 256;
		scoreText.color = color;
		scoreText.text = score;

		//Debug Code
		//print(text.text);
		//Debug.Log(text.text + "Instance ID: " + GetInstanceID());
	}

	void BlackWhiteCycle()
	{
		/*
		cycle = ((float) ((cycleCounter) % 256)) / 256;
		//print(cycle);
		color.r = cycle;
		color.g = cycle;
		color.b = cycle;
		text.color = color;
		text.text = score;
		++cycleCounter;
		*/

		cycle = 1 - (((float)((cycleCounter) % 256)) / 256);

		if (cycleCounter == 255)
		{
			isAscCounter = false;
		}

		else if (cycleCounter == 0)
		{
			isAscCounter = true;
		}

		if (!isAscCounter)
		{
			--cycleCounter;
		}

		else
		{
			++cycleCounter;
		}

		//print(cycle);
		color.r = cycle;
		color.g = cycle;
		color.b = cycle;
		//color.a = cycle;
		scoreText.color = color;
		scoreText.text = score;
	}

	void TransparencyCycle()
	{
		cycle = 1 - (((float)((cycleCounter) % 256)) / 256);

		if (cycleCounter == 255)
		{
			isAscCounter = false;
		}

		else if (cycleCounter == 0)
		{
			isAscCounter = true;
		}

		if (!isAscCounter)
		{
			--cycleCounter;
		}

		else
		{
			++cycleCounter;
		}

		//print(cycle);
		color.a = cycle;
		//StaticColor();
		scoreText.color = color;
		scoreText.text = score;
	}

	void StaticColor(bool isDefault)
	{
		if (isDefault)
		{
			color = itemColor;
		}

		else
		{
			color.r = (float)r / 256;
			color.g = (float)g / 256;
			color.b = (float)b / 256;
			scoreText.color = color;
			scoreText.text = score;
		}
	}
}