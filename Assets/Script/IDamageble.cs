using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble 
{
    int Health { get; set;}
    void Damage();
}
