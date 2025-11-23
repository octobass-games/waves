using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ControlsImageMap", menuName = "Scriptable Objects/ControlsImageMap")]
public class ControlsImageMap : ScriptableObject
{
    public List<ControlsImage> Images;
}
