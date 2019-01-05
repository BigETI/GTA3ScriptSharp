using IMGSharp;
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script class
    /// </summary>
    public static class GTA3Script
    {
        /// <summary>
        /// Open SCM directory
        /// </summary>
        /// <param name="directory">SCM directory</param>
        /// <param name="game">Game</param>
        /// <param name="filesMode">SCM files mode</param>
        /// <returns>SCM files if successful, otherwise "null"</returns>
        public static SCMFiles OpenSCMDirectory(string directory, EGame game, EGTA3ScriptFilesMode filesMode)
        {
            SCMFiles ret = null;
            if (Directory.Exists(directory))
            {
                string main_scm_path = Path.Combine(directory, "main.scm");
                string script_img_path = Path.Combine(directory, "script.img");
                if (File.Exists(main_scm_path) && File.Exists(script_img_path))
                {
                    using (IMGArchive img_file = IMGFile.OpenRead(script_img_path))
                    {
                        // TODO
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Open IR2 file
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="game">Game</param>
        /// <returns>IR2 if successful, otherwise "null"</returns>
        public static IR2 OpenIR2(string path, EGame game)
        {
            IR2 ret = null;
            if (path != null)
            {
                if (File.Exists(path))
                {
                    List<string> lines = new List<string>();
                    try
                    {
                        using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    // TODO
                                    // Validate

                                    lines.Add(line);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e);
                    }

                    // Instantiate
                    ret = new IR2(game, lines.ToArray());
                    lines.Clear();
                }
            }
            return ret;
        }
    }
}
