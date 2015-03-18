using UnityEngine.UI;

public class InputAxisScrollbar : Scrollbar
{
    public string axis;

    protected override void OnEnable()
    {
        base.OnEnable();
        onValueChanged.AddListener(HandleInput);
    }


    private void HandleInput(float arg0)
    {
        CrossPlatformInputManager.SetAxis(axis, (value * 2f) - 1f);
    }
}
