using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [SerializeField] private GameObject camB;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "CamTrigger":
                camB.SetActive(true);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "CamTrigger":
                camB.SetActive(false);
                break;
        }
    }
}
