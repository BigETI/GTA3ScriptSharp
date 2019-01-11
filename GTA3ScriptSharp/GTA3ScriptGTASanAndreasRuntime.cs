using System;
/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script GTA San Andreas runtime class
    /// </summary>
    public class GTA3ScriptGTASanAndreasRuntime : AGTA3ScriptRuntime
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GTA3ScriptGTASanAndreasRuntime()
        {
            Patch(0x1, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "WAIT");

            Patch(0x2, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "GOTO", "JUMP");

            Patch(0x3, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SHAKE_CAMERA");

            Patch(0x4D, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "ELSE_JUMP", "ELSE_GOTO", "JF");

            Patch(0x4E, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "END_THREAD");

            Patch(0x4F, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CREATE_THREAD");

            Patch(0x50, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "GOSUB");

            Patch(0x51, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "RETURN");

            Patch(0x53, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CREATE_PLAYER");

            Patch(0x9A, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CREATE_CHAR");

            Patch(0x9B, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "DESTROY_ACTOR");

            Patch(0xBE, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "TEXT_CLEAR_ALL");

            Patch(0xC0, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_CURRENT_TIME");

            Patch(0xC2, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_SPHERE_ONSCREEN");

            Patch(0xD6, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IF");

            Patch(0xD7, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CREATE_THREAD_WB", "CREATE_THREAD_NO_PARAMS");

            Patch(0xD8, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "MISSION_CLEANUP");

            Patch(0xDB, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_ACTOR_IN_CAR");

            Patch(0xDD, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_ACTOR_DRIVING_CAR_WITH_MODEL");

            Patch(0xDF, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_ACTOR_DRIVING");

            Patch(0x107, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CREATE_OBJECT");

            Patch(0x108, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "DESTROY_OBJECT");

            Patch(0x10D, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_PLAYER_WANTED_LEVEL");

            Patch(0x10E, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_PLAYER_MINIMUM_WANTED_LEVEL");

            Patch(0x111, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_WB_CHECK_TO");

            Patch(0x112, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "WASTED_OR_BUSTED");

            Patch(0x114, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "GIVE_ACTOR_WEAPON_AMMO");

            Patch(0x117, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_PLAYER_WASTED");

            Patch(0x118, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_ACTOR_DEAD");

            Patch(0x119, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_CAR_WRECKED");

            Patch(0x122, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_PLAYER_PRESSING_HORN");

            Patch(0x129, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CREATE_ACTOR_INSIDE_CAR");

            Patch(0x137, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_CAR_MODEL");

            Patch(0x14B, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CREATE_CAR_GENERATOR");

            Patch(0x14C, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_CAR_GENERATOR_CARS_TO_GENERATE");

            Patch(0x14E, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "START_TIMER_AT");

            Patch(0x14F, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "STOP_TIMER");

            Patch(0x151, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "REMOVE_STATUS_TEXT");

            Patch(0x154, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_ACTOR_IN_ZONE");

            Patch(0x158, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CAMERA_ON_VEHICLE");

            Patch(0x159, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CAMERA_ON_PED");

            Patch(0x15A, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "RESTORE_CAMERA");

            Patch(0x16A, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "FADE");

            Patch(0x16B, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "FADING");

            Patch(0x16C, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "RESTART_IF_WASTED_AT");

            Patch(0x16D, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "RESTART_IF_BUSTED_AT");

            Patch(0x173, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_ACTOR_Z_ANGLE");

            Patch(0x177, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_OBJECT_Z_ANGLE");

            Patch(0x1B4, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_PLAYER_CAN_MOVE");

            Patch(0x1B6, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_WEATHER");

            Patch(0x1B9, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_ACTOR_ARMED_WEAPON");

            Patch(0x1C2, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "REMOVE_REFERENCES_TO_ACTOR");

            Patch(0x1C3, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "REMOVE_REFERENCES_TO_CAR");

            Patch(0x1C4, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "REMOVE_REFERENCES_TO_OBJECT");

            Patch(0x1F0, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_MAX_WANTED_LEVEL_TO");

            Patch(0x1F5, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "GET_PLAYER_ACTOR");

            Patch(0x23C, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "LOAD_SPECIAL_ACTOR");

            Patch(0x247, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "LOAD_MODEL");

            Patch(0x248, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_MODEL_AVAILABLE");

            Patch(0x249, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "RELEASE_MODEL");

            Patch(0x256, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_PLAYER_DEFINED");

            Patch(0x2A3, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "ENABLE_WIDESCREEN");

            Patch(0x2AC, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_CAR_IMMUNITIES");

            Patch(0x2EB, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "RESTORE_CAMERA_WITH_JUMPCUT");

            Patch(0x317, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "INCREMENT_MISSION_ATTEMPTS");

            Patch(0x34F, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "DESTROY_ACTOR_WITH_FADE");

            Patch(0x35F, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "GIVE_ARMOUR_TO_ACTOR");

            Patch(0x38B, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "LOAD_REQUESTED_MODELS");

            Patch(0x394, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "PLAY_MUSIC");

            Patch(0x39E, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_ACTOR_CANT_BE_DRAGGED_OUT_OF_CAR");

            Patch(0x3A3, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_ACTOR_MALE");

            Patch(0x3A4, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "THREAD");

            Patch(0x3CF, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "LOAD_WAV");

            Patch(0x3D0, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "HAS_WAV_LOADED");

            Patch(0x3D1, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "PLAY_WAV");

            Patch(0x3D7, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_WAV_LOCATION");

            Patch(0x3DE, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_PED_DENSITY");

            Patch(0x3E4, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_TEXT_DRAW_ALIGN_RIGHT");

            Patch(0x3E6, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "REMOVE_TEXT_BOX");

            Patch(0x3EE, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_PLAYER_CONTROLLABLE");

            Patch(0x3F0, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "ENABLE_TEXT_DRAW");

            Patch(0x3FE, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_ACTOR_MONEY");

            Patch(0x40D, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "UNLOAD_WAV");

            Patch(0x417, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "START_MISSION");

            Patch(0x41D, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_CAMERA_NEAR_CLIP");

            Patch(0x459, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "END_THREAD_NAMED");

            Patch(0x4BB, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SELECT_INTERIOR");

            Patch(0x4ED, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "LOAD_ANIMATION");

            Patch(0x56D, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_ACTOR_DEFINED");

            Patch(0x56E, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_CAR_DEFINED");

            Patch(0x580, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_CHAR_OBJ_BUY_ICE_CREAM");

            Patch(0x0750, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_OBJECT_VISIBILITY");

            Patch(0x08A8, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_MARKERS_TO_LONG_DISTANCE");

            Patch(0x0A92, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CREATE_CUSTOM_THREAD");

            Patch(0x0A93, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "END_CUSTOM_THREAD");

            Patch(0x0A94, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "START_CUSTOM_MISSION");

            Patch(0x0A95, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "ENABLE_THREAD_SAVING");

            Patch(0x0A99, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CHDIR");

            Patch(0x0AA0, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "GOSUB_IF_FALSE");

            Patch(0x0AA1, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "RETURN_IF_FALSE");

            Patch(0x0AA9, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_GAME_VERSION_ORIGINAL");

            Patch(0x0AAB, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "FILE_EXISTS");

            Patch(0x0AB0, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "KEY_PRESSED");

            Patch(0x0AB1, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CALL_SCM_FUNC");

            Patch(0x0AB2, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "RET");

            Patch(0x0ABA, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "END_CUSTOM_THREAD_NAMED");

            Patch(0x0ADC, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "TEST_CHEAT");

            Patch(0x0ADF, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "ADD_DYNAMIC_GXT_ENTRY");

            Patch(0x0AE0, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "REMOVE_DYNAMIC_GXT_ENTRY");

            Patch(0x0AE4, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "DIRECTORY_EXISTS");

            Patch(0x0AE5, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CREATE_DIRECTORY");

            Patch(0x0AE9, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "POP_FLOAT");
            

            // TODO
        }
    }
}
