using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A switch that requires all the switchs to be active for this object to be set active.
/// </summary>
public class CompositeSwitch : Activable, IEnumerable<Activable>
{

    protected override bool CanChangeState => Activated ^ ConnectedSwitches.All(s => s.Activated);


    // IEnumerable interface

    public IEnumerator<Activable> GetEnumerator() => ConnectedSwitches.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

}
