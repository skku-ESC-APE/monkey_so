using UnityEngine;

public class InfoPanelController : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;

    private void Start()
    {
        if (infoPanel == null)
        {
            Debug.LogError("InfoPanel is not assigned.");
        }
        else
        {
            infoPanel.SetActive(false); // ���� ���� �� InfoPanel�� ��Ȱ��ȭ
        }
    }

    private void Update()
    {
        if (infoPanel != null && infoPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            infoPanel.SetActive(false);
        }
    }
}
