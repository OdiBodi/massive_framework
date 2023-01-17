using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class InternetConnection
    {
        public bool Available => Application.internetReachability != NetworkReachability.NotReachable;
    }
}
