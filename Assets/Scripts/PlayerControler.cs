using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;
    
    [Header("Player Configurations")]
    [SerializeField] private float movementSpeed;
    
    private Vector3 direction;
    private bool isWalk;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("fire"))
        {
            anim.SetTrigger("Attack");
        }
        
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
        
        controller.Move(direction * movementSpeed * Time.deltaTime);
        anim.SetBool("isWalk", isWalk);
    }
}
