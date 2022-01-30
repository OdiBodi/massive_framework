using System.Linq;
using Zenject;

namespace MassiveCore.Framework
{
    public class VfxInstaller : MonoInstaller
    {
        [Inject]
        private readonly GameConfig gameConfig;

        public override void InstallBindings()
        {
            Container.BindFactory<string, Vfx, Vfx.Factory>().FromMethod
            (
                (c, id) =>
                {
                    var configs = gameConfig.VfxConfigs.Configs;
                    var prefab = configs.First(x => x.Id == id).Vfx;
                    var vfx = c.InstantiatePrefabForComponent<Vfx>(prefab);
                    vfx.name = id;
                    return vfx;
                }
            );
        }
    }
}
