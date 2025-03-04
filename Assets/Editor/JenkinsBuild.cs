using UnityEditor;
using System;
using System.Collections.Generic;

class JenkinsBuild
{
    static string[] SCENES = FindEnabledEditorScenes();

    static string APP_NAME = "Freestone";
    static string TARGET_DIR = "build";

    static void PerformAllBuilds()
    {
        PerformWindowsBuild();
    }

    static void PerformWindowsBuild()
    {
        string target_dir = APP_NAME + ".exe";
        GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows, BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_dir, BuildTargetGroup build_target_group, BuildTarget build_target, BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(build_target_group, build_target);
        UnityEditor.Build.Reporting.BuildReport report = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);

        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Failed)
        {
            throw new Exception("BuildPlayer failure: " + report.summary);
        }
    }
}
