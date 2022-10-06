#define LOG_SHADERS_COMPILATION
//#define SKIP_SHADERS_COMPILATION

using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

namespace MassiveCore.Framework
{
	public class PreprocessShadersStripping : IPreprocessShaders
	{
		private const string LogFilePath = "Library/shaders_compilation_result.txt";

		private static readonly string[] StrippedNames =
		{
			"Hidden/Internal-DeferredShading",
			"Hidden/Internal-DeferredReflections",
			"Hidden/Internal-PrePassLighting",
			"Hidden/Internal-MotionVectors",
			"Hidden/Internal-Flare",
			"Hidden/Internal-Halo",
			"Hidden/CubeBlur",
			"Hidden/CubeCopy",
			"Hidden/CubeBlend",
			"Hidden/VR",
			"Hidden/Internal-ODSWorldTexture"
		};

		private static readonly PassType[] StrippedPassTypes =
		{
			PassType.Deferred,
			PassType.LightPrePassBase,
			PassType.LightPrePassFinal,
			PassType.ScriptableRenderPipeline,
			PassType.ScriptableRenderPipelineDefaultUnlit,
			PassType.MotionVectors,
			PassType.Meta
		};

		private static readonly ShaderKeyword[] StrippedShaderKeywords =
		{
			new("POINT"),
			new("SPOT"),
			new("DIRECTIONAL_COOKIE"),
			new("POINT_COOKIE"),
			new("LIGHTPROBE_SH"),
			new("SHADOWS_CUBE")
		};

		public int callbackOrder => 0;

		public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> data)
		{
#if LOG_SHADERS_COMPILATION
			System.IO.File.AppendAllText(LogFilePath, $"\n\n-----> {shader.name} {snippet.passName} {snippet.passType} {snippet.shaderType}\n");
#endif

			var shaderName = shader.name;
			if (StrippedNames.Any(strippedName => shaderName.StartsWith(strippedName)))
			{
#if LOG_SHADERS_COMPILATION
				System.IO.File.AppendAllText(LogFilePath, "Stripped by name.\n" );
#endif
				data.Clear();
				return;
			}

			var passType = snippet.passType;
			if (StrippedPassTypes.Any(skippedPassType => skippedPassType == passType))
			{
#if LOG_SHADERS_COMPILATION
				System.IO.File.AppendAllText(LogFilePath, "Stripped by pass type.\n" );
#endif
				data.Clear();
				return;
			}

			for (var i = data.Count - 1; i >= 0; --i)
			{
				var shaderKeywordSet = data[i].shaderKeywordSet;
				if (StrippedShaderKeywords.Any(skippedKeyword => shaderKeywordSet.IsEnabled(skippedKeyword)))
				{
					data.RemoveAt(i);
					continue;
				}

#if LOG_SHADERS_COMPILATION
				var keywords = new StringBuilder("- ");
				var shaderKeywords = shaderKeywordSet.GetShaderKeywords();
				if (shaderKeywords.Length == 0)
				{
					keywords.Append("Keywords are empty");
				}
				else
				{
					foreach (var keyword in shaderKeywords)
					{
						var keywordName = keyword.name;
						keywords.Append($"{keywordName} ");
					}
				}
				keywords.Append("\n");
				System.IO.File.AppendAllText(LogFilePath, keywords.ToString());
#endif
			}

#if SKIP_SHADERS_COMPILATION
			for (var i = data.Count - 1; i >= 0; --i)
			{
				data.Clear();
			}
#endif
		}
	}
}
