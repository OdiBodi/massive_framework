using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace MassiveCore.Framework.Editor
{
	public class PreProcessBuild : IPreprocessBuildWithReport
	{
		public int callbackOrder => 0;

		public void OnPreprocessBuild(BuildReport report)
		{
			try
			{
				System.IO.File.Delete(PreProcessShadersStripping.LogFilePath);
			}
			catch
			{
			}
		}
	}
}
