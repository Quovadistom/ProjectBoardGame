using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTM.Containers
{ 
    public class Chest : Container
    {
        public Rigidbody RigidBody;

        public override void Unlock()
        {
            base.Unlock();

            RigidBody.constraints = RigidbodyConstraints.None;
        }
    }
}

