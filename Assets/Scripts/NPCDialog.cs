using UnityEngine;

public class NPCDialog : MonoBehaviour
{

    public GameObject popupPrefab; // assign your UI prefab here
    public Vector2 offset; // offset above NPC
    public Vector2 uiPos;
    public RectTransform canvasRect;
    public RectTransform popupRect;
    private GameObject popupInstance;
    public float popupTime;
    private float popupTimer = 0f;
    private bool popupActive = false;
    void Start()
    {
        if (popupPrefab != null)
        {
            popupInstance = Instantiate(popupPrefab, new Vector2(0, 0), Quaternion.identity);
            popupInstance.transform.SetParent(GameObject.Find("NPC TextBox Canvas").transform, false);
            popupInstance.SetActive(false); // hide until needed
            canvasRect = popupInstance.transform.parent.GetComponent<RectTransform>();
            popupRect = popupInstance.GetComponent<RectTransform>();
        }
    }

    void Update()
    {

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

        if (popupActive)
        {
            popupTimer += Time.deltaTime;
            if (popupTimer >= popupTime)
            {
                HidePopup();
            }
            //popupRect.anchoredPosition = WorldObject_ScreenPosition + offset;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, screenPos, null, out localPos); // null since you're using Overlay
            popupRect.anchoredPosition = localPos + offset;
        }

        
    }

    public void ShowPopup(string message)
    {
        if (popupInstance != null)
        {
            popupInstance.SetActive(true);
            popupActive = true;

            popupInstance.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = message;
        }
    }

    public void HidePopup()
    {
        if (popupInstance != null)
        {
            popupInstance.SetActive(false);
            popupActive = false;
            popupTimer = 0f;
        }
    }
}
