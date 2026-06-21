using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

// Command-line WebGL build entry point.
// Invoke with:
//   Unity -quit -batchmode -projectPath . -buildTarget WebGL -executeMethod WebGLBuilder.Build
public static class WebGLBuilder
{
    public static void Build()
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => s.path)
            .ToArray();

        // Host-agnostic output: disabling compression lets the build run on any
        // static host (including GitHub Pages, which can't set Content-Encoding
        // headers). For Vercel/Netlify you can switch to Gzip/Brotli later.
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
        PlayerSettings.WebGL.decompressionFallback = false;
        PlayerSettings.WebGL.template = "APPLICATION:Default";

        const string outDir = "Build/WebGL";

        var options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = outDir,
            target = BuildTarget.WebGL,
            options = BuildOptions.None,
        };

        Debug.Log($"WEBGL BUILD STARTING. Scenes ({scenes.Length}): {string.Join(", ", scenes)}");

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"WEBGL BUILD SUCCEEDED: {summary.totalSize} bytes -> {outDir}");
            EditorApplication.Exit(0);
        }
        else
        {
            Debug.LogError($"WEBGL BUILD FAILED: result={summary.result}, errors={summary.totalErrors}");
            EditorApplication.Exit(1);
        }
    }
}
