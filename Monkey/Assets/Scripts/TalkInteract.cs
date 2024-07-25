using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TalkInteract Ŭ������ �÷��̾ Ư�� ������Ʈ�� ��ȣ�ۿ��� �� ��ȭ�� �����մϴ�.
/// �� Ŭ������ DialogueManager�� DialogueComponent�� ��ȣ�ۿ��Ͽ� ��ȭ�� �����մϴ�.
/// </summary>
public class TalkInteract : MonoBehaviour
{
    [SerializeField] private Image interactionImage; // �浹 �� ǥ���� �̹���
    [SerializeField] private DialogueComponent dialogueComponent; // DialogueComponent�� ����Ͽ� ��ȭ �����̳� ����
    private DialogueManager dialogueManager;
    private bool isPlayerInRange = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found in the scene.");
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
            if (dialogueManager != null && dialogueComponent != null)
            {
                // �÷��̾��� ��������Ʈ�� �����ͼ� ��ȭ ����
                Sprite playerSprite = GetPlayerSprite();
                dialogueManager.StartDialogue(playerSprite, dialogueComponent);
            }
            else
            {
                Debug.LogError("DialogueManager or DialogueComponent is null.");
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
        }
    }

    private Sprite GetPlayerSprite()
    {
        // Player ������Ʈ���� SpriteRenderer ������Ʈ�� ã�� ��������Ʈ�� ��ȯ�մϴ�.
        // ���� ���������� Player ������Ʈ�� ��ġ�� ���� ����� ������� �����ؾ� �մϴ�.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                return spriteRenderer.sprite;
            }
        }
        return null;
    }
}
