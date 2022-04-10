using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTM.Interfaces
{
    public interface ILockable
    {
        public void Unlock();
        public bool GetUnlocked();
    }
}
