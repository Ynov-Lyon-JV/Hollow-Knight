using System;
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
        {"ANIM_PLAYER_DASH", "PlayerD_dash"},
        {"ANIM_PLAYER_DASH_EFFECT", "Dash"},
        {"ANIM_TRANSITION_FADEIN", "FadeIn"},
        {"ANIM_TRANSITION_FADEOUT", "FadeOut"},
        //BUTTON
        {"BUTTON_JUMP", "Button_Jump"},
        {"BUTTON_ATTACK", "Button_Attack"},
        {"BUTTON_DASH", "Button_Dash"},
        {"BUTTON_FIRE", "Button_Fire"},
        //DIRECTION Entrées
        {"N1","S1"},
        {"N2","S2"},
        {"N3","S3"},
        {"S1","N1"},
        {"S2","N2"},
        {"S3","N3"},
        {"O1","E1"},
        {"O2","E2"},
        {"O3","E3"},
        {"E1","O1"},
        {"E2","O2"},
        {"E3","O3"},
        //SOUND
        {"SOUND_PLAYER_JUMP", "hornet_jump"},
        {"SOUND_PLAYER_LANDING", "hornet_ground_land"},
        {"SOUND_PLAYER_DASH", "hornet_dash"},
        {"SOUND_PLAYER_SWORD", "hornet_sword"},
        {"SOUND_PLAYER_DOMAGE", "hero_damage"},
        {"SOUND_ENEMY_DOMAGE", "enemy_damage"},
        {"SOUND_ENEMY_DEATH", "enemy_death_sword"},
    };

    public static string Get(string word)
    {
        if (Dictionary.ContainsKey(word))
            return Dictionary[word];
        return "";
    }


    public static float CalculeDirection(Transform target, Transform entity)
    {
        return Math.Sign(target.position.x - entity.position.x);
    }
}
