using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// An a activable that can be activated publicly from anywhere, used on the switchs
/// </summary>
public abstract class ActivableActuator : Activable
{

    public IEnumerable<Switch> ConnectedSwitches => FindObjectsOfType<Switch>().Where(s => s.ContainsActuator(this));

    public void Activate(bool active)
    {
        SetActivated(active);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        foreach (Switch @switch in ConnectedSwitches)
        {
            GizmosDrawSwitchActuatorConnection(@switch, this);
        }
    }

}
