using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlinkingScript : MonoBehaviour
{
    private StarterAssetsInputs _input;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {

    }
}
