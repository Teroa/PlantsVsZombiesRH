using TMPro;
using UnityEngine;

public class UIZombieNum : MonoBehaviour
{
	private TextMeshProUGUI t;

	private void Start()
	{
		t = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		t.text = $"场上敌人数量：{Board.Instance.theCurrentNumOfZombieUncontroled}";
	}
}
