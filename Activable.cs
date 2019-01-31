using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Activable : MonoBehaviour
{

    [System.Serializable]
    public class OnSwitchStateChangeEvent : UnityEvent<bool> { }

    // Leave this instead of using auto-property to be able to see it in the inspector in debug mode
    private bool activated = false;
    /// <summary>
    /// The actual state of the this <see cref="Activable"/>, if set, it doesn't call the virtual method
    /// <see cref="OnActivatedStateChange(bool)"/>.
    /// </summary>
    public bool Activated { get => activated; protected set => activated = value; }


    [SerializeField]
    private List<Activable> connectedActuators = new List<Activable>();
    public ReadOnlyCollection<Activable> ConnectedActuators => connectedActuators.AsReadOnly();

    protected virtual bool CanChangeState => true;

    public IEnumerable<Activable> ConnectedSwitches => FindObjectsOfType<Activable>().Where(s => s.ContainsActuator(this));


    [SerializeField]
    private OnSwitchStateChangeEvent onActiveStateChange = default;
    public event UnityAction<bool> OnActivateStateChange
    {
        add => onActiveStateChange.AddListener(value);
        remove => onActiveStateChange.RemoveListener(value);
    }


    public bool ContainsActuator(Activable actuator) => connectedActuators.Contains(actuator);


    /// <summary>
    /// Sets the actual state of this <see cref="Activable"/> with and calls the 
    /// <see cref="OnActivatedStateChange(bool)"/> virtual method if the active state changes.
    /// </summary>
    /// <param name="active">The state to set.</param>
    public void SetActivated(bool active)
    {
        if (Activated == active || !CanChangeState) return;

        Activated = active;
        OnActivatedStateChange(active);

        foreach (Activable actuator in connectedActuators)
        {
            actuator.SetActivated(active);
        }
    }

    public void ToggleState() => SetActivated(!activated);

    protected virtual void OnActivatedStateChange(bool active) { }


    public static void GizmosDrawSwitchActuatorConnection(Activable @switch, Activable actuator)
    {
        if (@switch == null || actuator == null) return;

        Gizmos.DrawLine(@switch.transform.position, actuator.transform.position);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (Activable actuator in connectedActuators)
        {
            GizmosDrawSwitchActuatorConnection(this, actuator);
        }

        Gizmos.color = Color.yellow;
        foreach (Activable @switch in ConnectedSwitches)
        {
            GizmosDrawSwitchActuatorConnection(@switch, this);
        }
    }

}
