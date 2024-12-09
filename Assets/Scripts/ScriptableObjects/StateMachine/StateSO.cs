using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public abstract class StateSO : ScriptableObject
{
    public List<StateSO> StatesToGo;
    public abstract void OnStateEnter(Enemy ec);
    public abstract void OnStateUpdate(Enemy ec);
    public abstract void OnStateExit(Enemy ec);
}
