using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A switch that requires all the switchs to be active for this object to be set active.
/// </summary>
public class CompositeSwitch : Switch, IEnumerable<Switch>
{

    [SerializeField]
    private List<Switch> switches = new List<Switch>();

    private void Awake()
    {
        foreach (Switch switchObject in this)
        {
            switchObject.OnActivateStateChange += OnSubswitchActivatedChange;
        }
    }

    private void OnDestroy()
    {
        foreach (Switch switchObject in this)
        {
            switchObject.OnActivateStateChange -= OnSubswitchActivatedChange;
        }
    }

    private void OnSubswitchActivatedChange(bool subswitchStateChange)
    {
        SetActivated(switches.All(s => s.Activated));
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        switches.ForEach(s => GizmosSwitchDrawKeyConnection(s));
    }

    // IEnumerable interface

    public IEnumerator<Switch> GetEnumerator()
    {
        return switches.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return switches.GetEnumerator();
    }

}
