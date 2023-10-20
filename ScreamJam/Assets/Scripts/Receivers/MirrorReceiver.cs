using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorReceiver : PieceReceiver
{
    public override void Action(GameObject go)
    {
        go.transform.SetParent(transform);
        go.transform.position = transform.position;
    }
}
