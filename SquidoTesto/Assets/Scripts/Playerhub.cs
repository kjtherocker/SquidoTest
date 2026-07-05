using UnityEngine;

public class Playerhub : MonoBehaviour
{
    private BasketBallInput     controls;
    public InteractionHandler interactionHandler { get; private set; }
    public PlayerMovementController playerMovementController { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize()
    {
        controls                 = new BasketBallInput();
        
        interactionHandler       = GetComponent<InteractionHandler>();
        playerMovementController = GetComponent<PlayerMovementController>();

        interactionHandler.Initialize(controls);
        playerMovementController.Initialize(controls);
        
        controls.Player.Escape.performed += ctx => OnEscape();   
        
        controls.Player.Enable();
    }

    void OnEscape()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
