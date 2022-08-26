using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Squares
{
    public class PostBuild
    {
#if UNITY_IOS
        [PostProcessBuild(1000)]
        public static void ModifyPBXProject(BuildTarget target, string path)
        {
            var project = new PBXProject();
            var projectPath = PBXProject.GetPBXProjectPath(path);
            project.ReadFromFile(projectPath);

            var targetName = PBXProject.GetUnityTargetName();
            var targetGuid = project.TargetGuidByName(targetName);

            project.SetBuildProperty(targetGuid, "SWIFT_VERSION", "5.1");

            File.WriteAllText(projectPath, project.WriteToString());
        }
#endif
    }
}
