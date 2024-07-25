using UnityEngine;
using UnityEngine.UI;


/// �÷��̾ Ư�� ������Ʈ�� ��ȣ�ۿ��� �� ������ ǥ���մϴ�.
/// ��ȣ�ۿ� ���� ���� �÷��̾ ������ �� ���� �г� Ȱ��ȭ

public class InfoInteract : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Image interactionImage; // �浹 �� ǥ���� �̹���
    private bool isPlayerInRange = false;

    private void Start()
    {
        if (infoPanel == null)
        {
            Debug.LogError("InfoPanel is not assigned.");
        }

        if (interactionImage != null)
        {
            interactionImage.gameObject.SetActive(false); // �ʱ⿡�� �̹����� ��Ȱ��ȭ
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed and player is in range.");
            if (infoPanel != null)
            {
                infoPanel.SetActive(true);
            }
            else
            {
                Debug.LogError("InfoPanel is null.");
            }
        }

        // �̹��� ��ġ�� ������Ʈ ��ܿ� ����
        if (isPlayerInRange && interactionImage != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            interactionImage.transform.position = screenPosition + new Vector3(0, 80, 0); // ������Ʈ ��ܿ� ��ġ ����
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered interaction range.");
            isPlayerInRange = true;
            if (interactionImage != null)
            {
                interactionImage.gameObject.SetActive(true); // �̹��� Ȱ��ȭ
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited interaction range.");
            isPlayerInRange = false;
            if (interactionImage != null)
            {
                interactionImage.gameObject.SetActive(false); // �̹��� ��Ȱ��ȭ
            }

            if (infoPanel != null)
            {
                infoPanel.SetActive(false); // �÷��̾ ������ ����� ���� �г� ��Ȱ��ȭ
            }
        }
    }
}
