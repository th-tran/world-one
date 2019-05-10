using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	public GameObject[] number;
	public Transform[] field;
	GameObject[] activeObj;
	int score = 0;

	void Start () {
		activeObj = new GameObject[field.Length];
		SetValue (score);
	}
	
	public void Inc () {
		Clear();
		if (score < 9999)
		{
			score++;
		}
		SetValue(score);
	}

	public void Dec () {
		Clear();
		if (score > 0)
		{
			score--;
		}
		SetValue(score);
	}

	void Clear () {
		for (int i = 0; i < field.Length; i++)
		{
			Destroy (activeObj[i]);
		}
	}

	void SetValue (int score) {
		int Convert = 1;
		for (int i = 0; i < field.Length; i++) 
		{
			int scoreConvert = (score / Convert) % 10;
			Print (i, scoreConvert, i);
			Convert *= 10;
		}
	}

	void Print (int activeObj, int score, int field) {
		this.activeObj[activeObj] = Instantiate (this.number[score], this.field[field].position, this.field[field].rotation);
		this.activeObj[activeObj].name = this.field[field].name;
		this.activeObj[activeObj].transform.parent = this.field[field];
	}
}
