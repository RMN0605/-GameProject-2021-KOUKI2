using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRange : MonoBehaviour
{
	[Header("明るさ")]
	public float brightness = 5f;
	[SerializeField, Header("明るくなる速さ")]
	private float brightnessSpeed = 0.3f;
	private float nowBrightness = 1;
	private SpriteRenderer spriteRenderer;
	private bool isAlpha;   //アルファの値が最大になっているか最小になっているかを見るやつ
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		isAlpha = false;
	}
	private void Update()
	{
		if (nowBrightness <= brightness)
		{
			if (!isAlpha)
				spriteRenderer.color += new Color(0, 0, 0, brightnessSpeed * Time.deltaTime);
			if (isAlpha)
				spriteRenderer.color -= new Color(0, 0, 0, brightnessSpeed * Time.deltaTime);
			nowBrightness += nowBrightness * Time.deltaTime;
		}
		else
		{
			nowBrightness = 1;
			if (isAlpha)
				isAlpha = false;
			else
				isAlpha = true;
		}
	}

}
