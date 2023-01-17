using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework.Runtime
{
    public class Cameras : ICameras
    {
        private readonly IEnumerable<Camera> _cameras;

        public Cameras(IEnumerable<Camera> cameras)
        {
            _cameras = cameras;
        }

        public Camera CameraBy(string name)
        {
            return _cameras.First(camera => camera.name == name);
        }
    }
}
