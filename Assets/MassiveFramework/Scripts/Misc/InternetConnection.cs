using UnityEngine;

namespace MassiveCore.Framework
{
    public class InternetConnection
    {
        public bool Available => Application.internetReachability != NetworkReachability.NotReachable;
    }
}
