using UnityEngine;

public class HpBarFollow : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject canvas;

    private RectTransform hpBar; // HP 바 RectTransform 컴포넌트

    public float height = 1.7f;

    private void Start()
    {
        canvas = GameObject.FindWithTag("Canvas"); // Canvas를 태그로 찾아서 할당합니다.
        if (canvas == null)
        {
            Debug.LogError("Canvas not found! Make sure to assign a canvas GameObject or add the 'Canvas' tag to the canvas GameObject.");
            return;
        }

        // HP 바를 생성하고 canvas의 자식으로 만듭니다.
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
    }

    // HP 바를 업데이트하는 메서드
    public void UpdateHpBar(int currentHealth, int maxHealth)
    {
        // 현재 체력과 최대 체력을 기반으로 HP 바의 크기를 조정합니다.
        float fillAmount = (float)currentHealth / maxHealth;
        hpBar.localScale = new Vector3(fillAmount, 1f, 1f);
    }

    // HP 바를 파괴하는 메서드
    public void DestroyHpBar()
    {
        // HP 바를 파괴합니다.
        Destroy(hpBar.gameObject);
    }

    private void Update()
    {
        // 적 오브젝트의 위치를 기준으로 HP 바의 위치를 업데이트합니다.
        Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = hpBarPos;
    }
}
