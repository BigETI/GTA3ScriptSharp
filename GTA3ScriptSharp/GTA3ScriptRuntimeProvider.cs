/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA2Script runtime provider
    /// </summary>
    public static class GTA3ScriptRuntimeProvider
    {
        /// <summary>
        /// Create GTA3Script runtime
        /// </summary>
        /// <param name="game">Game</param>
        /// <returns>GTA3Script runtime if successful, otherwise "null"</returns>
        public static AGTA3ScriptRuntime CreateRuntime(EGame game)
        {
            AGTA3ScriptRuntime ret = null;
            switch (game)
            {
                case EGame.GTAIII:
                    ret = new GTA3ScriptGTAIIIRuntime();
                    break;
                case EGame.GTAViceCity:
                    ret = new GTA3ScriptGTAViceCityRuntime();
                    break;
                case EGame.GTASanAndreas:
                    ret = new GTA3ScriptGTASanAndreasRuntime();
                    break;
                case EGame.GTALibertyCityStories:
                    ret = new GTA3ScriptGTALibertyCityStoriesRuntime();
                    break;
                case EGame.GTAViceCityStories:
                    ret = new GTA3ScriptGTAViceCityStoriesRuntime();
                    break;
            }
            return ret;
        }
    }
}
