using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Switch : Activable
{

    [System.Serializable]
    public class OnSwitchStateChangeEvent : UnityEvent<bool> { }

    [SerializeField]
    private List<ActivableActuator> connectedActuators = new List<ActivableActuator>();
    public System.Collections.ObjectModel.ReadOnlyCollection<ActivableActuator> ConnectedActuators
    {
        get { return connectedActuators.AsReadOnly(); }
    }

    [SerializeField]
    private OnSwitchStateChangeEvent onActiveStateChange;
    public event UnityAction<bool> OnActivateStateChange
    {
        add { onActiveStateChange.AddListener(value); }
        remove { onActiveStateChange.RemoveListener(value); }
    }

    public bool ContainsActuator(ActivableActuator actuator)
    {
        return connectedActuators.Contains(actuator);
    }

    protected sealed override void OnActivatedStateChange(bool active)
    {
        connectedActuators.ForEach(a => a.Activate(active));

        OnSwitchStateUpdate(active);
        onActiveStateChange.Invoke(active);
    }

    protected virtual void OnSwitchStateUpdate(bool state) { }


    protected virtual void OnDrawGizmosSelected()
    {
        connectedActuators.ForEach(a => GizmosDrawSwitchActuatorConnection(this, a));
    }


    // To draw yellow lines from this switch to a certain point, indicating this position is important to the switch
    // Used for CompositeSwitch for all the switches that are linked to the composite one

    protected void GizmosSwitchDrawKeyConnection(Component toComponent)
    {
        GizmosSwitchDrawKeyConnection(toComponent.transform);
    }

    protected void GizmosSwitchDrawKeyConnection(Transform toTransform)
    {
        GizmosSwitchDrawKeyConnection(toTransform.position);
    }

    protected void GizmosSwitchDrawKeyConnection(Vector3 toWorldPosition)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(this.transform.position, toWorldPosition);
    }

}
