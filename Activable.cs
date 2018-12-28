using UnityEngine;


public abstract class Activable : MonoBehaviour
{

    // Leave this instead of using auto-property to be able to see in the inspector in debug mode
    private bool activated = false;
    /// <summary>
    /// The actual state of the this <see cref="Activable"/>, if set, it doesn't call the virtual method
    /// <see cref="OnActivatedStateChange(bool)"/>.
    /// </summary>
    public bool Activated { get { return activated; } protected set { activated = value; } }

    /// <summary>
    /// Sets the actual state of this <see cref="Activable"/> with and calls the 
    /// <see cref="OnActivatedStateChange(bool)"/> virtual method if the active state changes.
    /// </summary>
    /// <param name="active">The state to set.</param>
    protected void SetActivated(bool active)
    {
        if (activated == active) return;

        activated = active;
        OnActivatedStateChange(active);
    }

    protected void ToggleState()
    {
        SetActivated(!activated);
    }

    protected abstract void OnActivatedStateChange(bool active);

    public static void GizmosDrawSwitchActuatorConnection(Switch @switch, ActivableActuator actuator)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(@switch.transform.position, actuator.transform.position);
    }

}
