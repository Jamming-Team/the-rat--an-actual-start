using UnityEngine;

public class test : MonoBehaviour
{

    [SerializeField] private string a = "a";
    [SerializeField] private string b = "b";
    
    [SerializeField] private GameInputManager manager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager.Init();
        manager.player.interact += Interact;
        manager.vehicle.interact += InteractV;
    }
    

    private void InteractV()
    {
        Debug.Log($"Interact Vehicle: ");
    }

    private void Interact()
    {
        Debug.Log($"Interact Player: ");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            manager.ChangeState(GC.States.InputMaps.None);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            manager.ChangeState(GC.States.InputMaps.Player);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            manager.ChangeState(GC.States.InputMaps.Vehicle);
        }
    }
}
