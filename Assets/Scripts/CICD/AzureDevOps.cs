using System;
using UnityEditor;

namespace Game.CICD
{
    public class AzureDevOps
    {
        private static readonly string[] _scenes =
        {
            "Assets/Scenes/SampleScene.unity"
        };

        [MenuItem("CICD/Build Game")]
        public static void Build()
        {
            PlayerSettings.bundleVersion = GetCommandLineArgumentValue("Verson");
                        
            BuildOptions buildOptions = GetBuildOptions();

            BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes              = _scenes,
                locationPathName    = GetCommandLineArgumentValue("BuildOutputDirectory"),
                target              = BuildTarget.WebGL,
                options             = buildOptions
            });
        }

        private static string GetCommandLineArgumentValue(string argumentName)
        {
            string[] allArguments = Environment.GetCommandLineArgs();

            if (!argumentName.StartsWith("--"))
            {
                argumentName = ("--" + argumentName);
            }

            for (int i = 0; i < allArguments.Length; i++)
            {
                if (allArguments[i] == argumentName)
                {
                    return allArguments[i + 1];
                }
            }

            throw new Exception("The argument '" + argumentName + "' could not be found");
        }

        private static bool CheckCommandLineArgumentExists(string argumentName)
        {
            string[] allArguments = Environment.GetCommandLineArgs();

            if (!argumentName.StartsWith("--"))
            {
                argumentName = ("--" + argumentName);
            }

            for (int i = 0; i < allArguments.Length; i++)
            {
                if (allArguments[i] == argumentName)
                {
                    return true;
                }
            }

            return false;
        }

        private static BuildOptions GetBuildOptions()
        {
            bool developmentBuild = CheckCommandLineArgumentExists("DevelopmentBuild");

            if (developmentBuild)
            {
                return BuildOptions.StrictMode | BuildOptions.Development;
            }
            else
            {
                return BuildOptions.StrictMode;
            }
        }
    }
}