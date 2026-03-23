using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance { get; private set; }
    public PlayerKeyBinds.StandardActions input;

    private void Awake()
    {
        Instance = this;
        var inputActions = new PlayerKeyBinds();
        inputActions.Enable();
        input = inputActions.Standard;
        input.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
