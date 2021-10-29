using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    
    [SerializeField] private Camera camera;

    private Vector3 prevPosition;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameMaster").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameRunning)
        {
            Vector3 worldMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            bool mouseButttonPushed = Input.GetMouseButton(0);
            if (mouseButttonPushed)
            {
                transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, 0);
            }
        }
    }
}
