using UnityEngine;

public class test : MonoBehaviour
{

    [SerializeField] private string a = "a";
    [SerializeField] private string b = "b";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("b");
        Debug.Log("a");
    }
}
