using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dico
{
    //Essayer de ne jamais rename la premiere colone
    private static Dictionary<string, string> Dictionary = new Dictionary<string, string> {
        //ANIM
        {"ANIM_PLAYER_IDLE", "PlayerD_idle"},
        {"ANIM_PLAYER_RUN", "PlayerD_run"},
        {"ANIM_PLAYER_JUMP", "PlayerD_jump"},
        {"ANIM_PLAYER_LAND", "PlayerD_land"},
        {"ANIM_PLAYER_ATTACK", "PlayerD_attack"},
        //BUTTON
        {"BUTTON_JUMP", "Button_Jump"},
        {"BUTTON_ATTACK", "Button_Attack"},
        //SOUND
        {"SOUND_PLAYER_JUMP", "Audio/SoundEffects/SoundEffect_Jump"},
    };

    public static string Get(string word)
    {
        return Dictionary[word];
    }
}
