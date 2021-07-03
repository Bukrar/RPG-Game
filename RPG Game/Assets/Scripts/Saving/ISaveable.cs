using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public interface ISaveable
    {
        object CaputureState();
        void RestoreState(object state);
    }
}

