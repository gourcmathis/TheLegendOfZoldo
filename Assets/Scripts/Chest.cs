using UnityEngine.UI;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Text interactUI;
    private bool isInRange;

    public Animator animator;
    public int gemsToAdd;

    public AudioClip openChestSound;

    void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInRange && Inventory.instance.keysCount >= 1)
        {
            OpenChest();
            Inventory.instance.UseKeys();
        }
    }

    void OpenChest()
    {
        animator.SetTrigger("OpenChest");
        Inventory.instance.AddGems(gemsToAdd);
        GetComponent<BoxCollider2D>().enabled = false;
        interactUI.enabled = false;
        AudioManager.instance.PlayClipAt(openChestSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Player"))
        {
            interactUI.enabled = false;
            isInRange = false;
        }
    }
}
