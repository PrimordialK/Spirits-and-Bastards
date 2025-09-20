using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float moveDistance = 5f; // How far the elevator moves up/down
    [SerializeField] private float moveSpeed = 2f;    // How fast the elevator moves
    [SerializeField] private KeyCode activateKey = KeyCode.E;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingUp = true;
    private bool isPlayerOn = false;
    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + Vector3.down * moveDistance; // Move down instead of up
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOn && !isMoving && Input.GetKeyDown(activateKey))
        {
            Debug.Log("E pressed while on elevator!");
            isMoving = true;
        }

        if (isMoving)
        {
            Vector3 target = movingUp ? endPos : startPos;
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.01f)
            {
                transform.position = target;
                movingUp = !movingUp;
                isMoving = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOn = true;
            Debug.Log("Player entered elevator.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerOn = false;
            Debug.Log("Player exited elevator.");
        }
    }
}
