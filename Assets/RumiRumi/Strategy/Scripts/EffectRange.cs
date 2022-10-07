using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRange : MonoBehaviour
{
	[Header("���邳")]
	public float brightness = 5f;
	[SerializeField, Header("���邭�Ȃ鑬��")]
	private float brightnessSpeed = 0.3f;
	private float nowBrightness = 1;
	private SpriteRenderer spriteRenderer;
	private bool isAlpha;   //�A���t�@�̒l���ő�ɂȂ��Ă��邩�ŏ��ɂȂ��Ă��邩��������
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
