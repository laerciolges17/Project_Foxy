using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private CharacterController controller;
    
    [Header("Player Configurations")]
    
    [SerializeField] private float movementSpeed;
    
    private Vector3 direction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        controller.Move(direction * movementSpeed * Time.deltaTime);
        
    }
}
