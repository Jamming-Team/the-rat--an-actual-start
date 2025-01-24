using UnityEngine;
using Rat;
using PrimeTween;


public class test : MonoBehaviour
{

    [SerializeField] private SoundData soundData;
    
    [SerializeField] private string a = "a";
    [SerializeField] private string b = "b";
    
    [SerializeField] private GameInputManager manager;
    private Sequence _sequenceCreateSound;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager.Init();
        manager.player.interact += Interact;
        manager.vehicle.interact += InteractV;
        // Sequence.Create(1000)
        //     .ChainCallback(() =>
        //     {
        //         SoundManager.Instance.CreateSoundBuilder()
        //             .WithPosition(this.transform.position)
        //             .WithRandomPitch()
        //             .Play(soundData);
        //     })
        //     .ChainDelay(0.01f);
    }
    

    private void InteractV()
    {
        SoundManager.Instance.CreateSoundBuilder()
            .WithPosition(this.transform.position)
            .WithRandomPitch()
            .Play(soundData);
        Debug.Log($"Interact Vehicle: ");
    }

    private void Interact()
    {
        MusicManager.Instance.PlayNextTrack();
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
