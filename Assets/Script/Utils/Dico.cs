using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dico
{
    //Essayer de ne jamais rename la premiere colone
    private static Dictionary<string, string> Dictionary = new Dictionary<string, string> {
        //ANIM
        {"ANIM_PLAYER_IDLE", "willo_idle"},
        {"ANIM_PLAYER_RUN", "willo_run"},
        {"ANIM_PLAYER_JUMP", "willo_jump"},
        {"ANIM_PLAYER_LAND", "willo_fall"},
        {"ANIM_PLAYER_ATTACK", "PlayerD_attack"},
        {"ANIM_PLAYER_DASH", "willo_dash"},
        {"ANIM_PLAYER_DASH_EFFECT", "Dash"},
        {"ANIM_TRANSITION_FADEIN", "FadeIn"},
        {"ANIM_TRANSITION_FADEOUT", "FadeOut"},
        //BUTTON
        {"BUTTON_JUMP", "Button_Jump"},
        {"BUTTON_ATTACK", "Button_Attack"},
        {"BUTTON_DASH", "Button_Dash"},
        {"BUTTON_FIRE", "Button_Fire"},
        {"BUTTON_BOMB", "Button_Bomb"},
        {"BUTTON_INTERACT", "Button_Interact"},
        {"BUTTON_SPELL", "Button_Spell"},
        {"BUTTON_SPELLMENU", "Button_SpellMenu"},
        {"BUTTON_CANCEL", "Button_Cancel"},
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
        //SPELL
        {"SPELL_SLIME", "SPELL_SLIME_SHIELD"},
        {"SPELL_TREE", "SPELL_TREE"},
        {"SPELL_MUSHROOM", "SPELL_MUSHROOM"},
        {"SPELL_BAT", "SPELL_BAT"},
    };

    public static string Get(string word)
    {
        if (Dictionary.ContainsKey(word))
            return Dictionary[word];
        return "";
    }


    public static float CalculeDirection(Transform entity, Transform target)
    {
        return Math.Sign(target.position.x - entity.position.x);
    }

}
