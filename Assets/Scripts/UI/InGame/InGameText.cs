using TMPro;
using UnityEngine;

public class InGameText : MonoBehaviour
{
	private float existTime;

	private TextMeshProUGUI t;

	public static InGameText Instance;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		t = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		if (existTime > 0f)
		{
			existTime -= Time.deltaTime;
			if (existTime <= 0f)
			{
				t.enabled = false;
				base.transform.GetChild(0).gameObject.SetActive(value: false);
				existTime = 0f;
			}
		}
		if (GameAPP.theGameStatus == 1)
		{
			t.enabled = false;
			base.transform.GetChild(0).gameObject.SetActive(value: false);
			existTime = 0f;
		}
	}

	public void EnableText(string text, float time)
	{
		t.enabled = true;
		base.transform.GetChild(0).gameObject.SetActive(value: true);
		t.text = text;
		existTime = time;
	}
}
