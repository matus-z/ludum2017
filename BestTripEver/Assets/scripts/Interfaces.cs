using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ----------------------------------------------------------------
public enum ELayer
{
    Base,
    Red,
    Green,
    Blue,
    Yellow,
};

// ----------------------------------------------------------------
interface IInteractable
{
    void InteractWithPlayer(PlayerController player);
}
