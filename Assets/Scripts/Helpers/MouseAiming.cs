using UnityEngine;

public static class MouseAiming
{
    private static readonly Camera _mainCamera;

    static MouseAiming()
    {
        _mainCamera = Camera.main;
    }

    public static (bool success, Vector3 position) GetMousePosition()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }
}
