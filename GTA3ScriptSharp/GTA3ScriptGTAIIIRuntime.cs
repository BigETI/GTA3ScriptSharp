using System;

/// <summary>
/// GTA3Script sharp namespace
/// </summary>
namespace GTA3ScriptSharp
{
    /// <summary>
    /// GTA3Script GTA III runtime class
    /// </summary>
    public class GTA3ScriptGTAIIIRuntime : AGTA3ScriptRuntime
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GTA3ScriptGTAIIIRuntime() : base()
        {
            /*// Go to the label if the condition result is true.
            Patch(0x4C, null, typeof(void), (runtime, arguments) =>
            {
                // TODO
                throw new NotImplementedException();
            }, typeof(GTA3ScriptReferenceValue), typeof(GTA3ScriptReferenceValue));

            AddKeyword(0x4D, "ELSE_GOTO");

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

            Patch(0x54, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "GET_PLAYER_COORDINATES");

            Patch(0x55, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_PLAYER_COORDINATES");

            Patch(0x56, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_PLAYER_IN_AREA_2D");

            Patch(0x57, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_PLAYER_IN_AREA_3D");

            Patch(0x9A, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CREATE_CHAR");

            Patch(0x9B, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "DESTROY_ACTOR");

            Patch(0x9C, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "ACTOR_WANDER_DIR");

            Patch(0x9F, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "ACTOR_SET_IDLE");

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

            Patch(0xC3, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "ENTER_DEBUGMODE");

            Patch(0xC4, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "EXIT_DEBUGMODE");

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

            Patch(0xDC, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_PLAYER_DRIVING");

            Patch(0xDD, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_ACTOR_DRIVING_VEHICLE_TYPE");

            Patch(0xDE, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_PLAYER_DRIVING_VEHICLE_TYPE");

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

            Patch(0x11A, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_ACTOR_THREAT_SEARCH");

            Patch(0x11C, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_ACTOR_OBJ_NO_OBJ");

            Patch(0x121, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_PLAYER_IN_ZONE");

            Patch(0x122, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_PLAYER_PRESSING_HORN");

            Patch(0x123, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "HAS_ACTOR_SPOTTED_PLAYER");

            Patch(0x129, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CREATE_ACTOR_INSIDE_CAR");

            Patch(0x130, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_PLAYER_BUSTED");

            Patch(0x135, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_CAR_DOOR_LOCK");

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

            Patch(0x14C, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "TEXT_PAGER");

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

            Patch(0x152, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_ZONE_CAR_INFO");

            Patch(0x154, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "IS_ACTOR_IN_ZONE");

            Patch(0x157, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CAMERA_ON_PLAYER");

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
            }, null, "SET_PLAYER_CONTROL");

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

            Patch(0x23C, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "LOAD_SPECIAL_ACTOR");

            Patch(0x247, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "REQUEST_MODEL");

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
            }, null, "TOGGLE_WIDESCREEN");

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

            Patch(0x40D, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "CLEAR_MISSION_AUDIO");

            Patch(0x417, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "START_MISSION");

            Patch(0x41D, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_CAMERA_NEAR_CLIP");

            Patch(0x580, (runtime, args) =>
            {
                throw new NotImplementedException();
            }, null, "SET_CHAR_OBJ_BUY_ICE_CREAM");*/


            // TODO
        }
    }
}
