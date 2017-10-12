using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatechart : ScriptableObject {
    [SerializeField] public string Name;

#if UNITY_EDITOR
    [SerializeField] public string eDescription;
    [SerializeField] Dictionary<UIStatechart, Vector2> eChildStatePositions;
#endif

    [SerializeField] public Dictionary<UIStateSegue, UIStatechart> SegueTargets;

    [SerializeField] public List<UIStateSegue> Segues;

}

public class UIStateSegue
{
    public enum StateStackTypes { Push, Pop, Next, Back, Exit };

    [SerializeField] public StateStackTypes StateStackType;
}