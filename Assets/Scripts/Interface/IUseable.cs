using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseable 
{
   string Name { get; }
   string Description { get; }
   void Interact();
}
