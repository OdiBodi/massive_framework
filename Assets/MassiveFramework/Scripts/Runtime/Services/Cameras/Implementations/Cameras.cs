using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MassiveCore.Framework
{
    public class Cameras : ICameras
    {
        private readonly IEnumerable<Camera> cameras;

        public Cameras(IEnumerable<Camera> cameras)
        {
            this.cameras = cameras;
        }

        public Camera CameraBy(string name)
        {
            return cameras.First(camera => camera.name == name);
        }
    }
}
