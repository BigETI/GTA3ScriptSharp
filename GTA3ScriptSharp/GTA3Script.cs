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
        /// Open script directory
        /// </summary>
        /// <param name="directory">Script directory</param>
        /// <param name="game">Game</param>
        /// <param name="filesMode">Script files mode</param>
        /// <returns>Script files if successful, otherwise "null"</returns>
        public static GTA3ScriptFiles OpenScriptDirectory(string directory, EGame game, EGTA3ScriptFilesMode filesMode)
        {
            GTA3ScriptFiles ret = null;
            if (Directory.Exists(directory))
            {
                string main_scm_path = Path.Combine(directory, "main.scm");
                string script_img_path = Path.Combine(directory, "script.img");
                if (File.Exists(main_scm_path) && File.Exists(script_img_path))
                {
                    List<Stream> streams = new List<Stream>();
                    try
                    {
                        streams.Add(File.Open(main_scm_path, FileMode.Open, FileAccess.Read));
                        using (IMGArchive img_archive = IMGFile.OpenRead(script_img_path))
                        {
                            IMGArchiveEntry[] entries = img_archive.Entries;
                            foreach (IMGArchiveEntry entry in entries)
                            {
                                if (entry != null)
                                {
                                    Stream stream = entry.Open();
                                    if (stream != null)
                                    {
                                        streams.Add(stream);
                                    }
                                }
                            }
                        }
                        AGTA3Script[] scripts = new AGTA3Script[streams.Count];
                        for (int i = 0; i < scripts.Length; i++)
                        {
                            scripts[i] = new SCM(game, streams[i]);
                        }
                        ret = new GTA3ScriptFiles(game, scripts);
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e);
                        foreach (Stream stream in streams)
                        {
                            stream.Dispose();
                        }
                    }
                    streams.Clear();
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
                    ret = new IR2(game, File.Open(path, FileMode.Open, FileAccess.Read));
                }
            }
            return ret;
        }
    }
}
