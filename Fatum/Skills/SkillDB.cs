using System.Dynamic;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SkillDB
{
    public static Sprite[] s_skillSprites = new Sprite[23];

    public static Skill NoSkill = new Skill(
        "NoSkill",
        "None", 0, 0, 0,
        Skill.SkillType.None, "noItem",
        "Not unlocked yet:Not unlocked yet"

    );
#region MainSkills
    public static Skill Hermit = new Skill(
        "Hermit",
        "UseHermitSkill", 1, 30f, 1f,
        Skill.SkillType.Main, "the hermit",
        "The Hermit walks through every plane, wandering, but never lost on it's way:Press left click to leave your body and wander on the spiritual plane. Beware, your body can still be damaged."

    );
    public static Skill Empress = new Skill(
        "Empress",
        "None", 0, 0, 0,
        Skill.SkillType.Main, "the empress",
        "The Empress:Empress descript"

    );
    public static Skill Devil = new Skill(
        "Devil",
        "None", 0, 0, 0,
        Skill.SkillType.Main, "the devil",
        "The Devil:Devil descript"

    );
    public static Skill Moon = new Skill(
        "Moon",
        "None", 0, 0, 0,
        Skill.SkillType.Main, "the moon",
        "The Moon:Moon descript"

    );
    public static Skill Sun = new Skill(
        "Sun",
        "None", 0, 0, 0,
        Skill.SkillType.Main, "the sun",
        "The Sun:Sun descript"

    );
    public static Skill Judgement = new Skill(
        "Judgement",
        "None", 0, 0, 0,
        Skill.SkillType.Main, "judgement",
        "Judgement:Judgement descript"

    );
#endregion
#region Passive
    public static Skill Lovers = new Skill(
        "Lovers",
        "None", 0, 0, 0,
        Skill.SkillType.Pasive, "the lovers",
        "What would you do for your loved one? What would you sacrifice?:This amulet restores health over time"

    );
    public static Skill Hierophant = new Skill(
        "Hierophant",
        "None", 0, 0, 0,
        Skill.SkillType.Pasive, "the hierophant",
        "The Hierophant:Hierophant descript"

    );
    public static Skill Strenght = new Skill(
        "Strenght",
        "None", 0, 0, 0,
        Skill.SkillType.Pasive, "strenght",
        "Strenght:Strenght descript"

    );
    public static Skill Wheel = new Skill(
        "Wheel",
        "None", 0, 0, 0,
        Skill.SkillType.Pasive, "wheel of fortune",
        "The wheel of fortune:Wheel descript"

    );
    public static Skill Temperance = new Skill(
        "Temperance",
        "None", 0, 0, 0,
        Skill.SkillType.Pasive, "temperance",
        "Temperance:Temperance descript"

    );
    public static Skill Tower = new Skill(
        "Tower",
        "None", 0, 0, 0,
        Skill.SkillType.Pasive, "the tower",
        "The Tower:Tower descript"

    );
    public static Skill Hanged = new Skill(
        "Hanged",
        "None", 0, 0, 0,
        Skill.SkillType.Pasive, "the hanged man",
        "The Hanged Man:XD"

    );
#endregion
#region Movement
public static Skill Magician = new Skill(
        "Magician",
        "None", 0, 0, 0,
        Skill.SkillType.Movement, "the magician",
        "The Magician:Press right click to teleport a shot distance"

    );
    public static Skill Chariot = new Skill(
        "Chariot",
        "None", 0, 0, 0,
        Skill.SkillType.Movement, "the chariot",
        "The Chariot:Carro"

    );
    
#endregion
#region Mask
public static Skill Fool = new Skill(
        "Fool",
        "None", 0, 0, 0,
        Skill.SkillType.Mask, "the fool",
        "Everything is conected somehow. Even a lost wanderer like you.:This mask grants 100 health points and 100 flux points"

    );public static Skill Priestess = new Skill(
        "Priestess",
        "None", 0, 0, 0,
        Skill.SkillType.Mask, "the high priestess",
        "The High Priestess:This mask grants 100 health points and 100 flux points"

    );public static Skill Emperor = new Skill(
        "Emperor",
        "None", 0, 0, 0,
        Skill.SkillType.Mask, "the emperor",
        "The Emperor:This mask grants 100 health points and 100 flux points"

    );public static Skill Justice = new Skill(
        "Justice",
        "None", 0, 0, 0,
        Skill.SkillType.Mask, "justice",
        "Justice:This mask grants 100 health points and 100 flux points"

    );public static Skill Death = new Skill(
        "Death",
        "None", 0, 0, 0,
        Skill.SkillType.Mask, "death",
        "Death doesn't make distinctions between the wealthy and the poor:This mask grants 100 health points and 100 flux points"
    );
    public static Skill Star = new Skill(
        "Star",
        "None", 0, 0, 0,
        Skill.SkillType.Mask, "the star",
        "The Star:This mask grants 100 health points and 100 flux points"
    );
    public static Skill World = new Skill(
        "World",
        "None", 0, 0, 0,
        Skill.SkillType.Mask, "the world",
        "ZA WARUDO:KONO DIO DA"
    );
#endregion
    
public static Skill GetSkillByName(string s){
    Skill get;
    switch(s){
        case "Hermit":
            get = Hermit;
            break;
        case "Fool":
            get = Fool;
            break;
        case "Wheel":
            get = Wheel;
            break;
        case "Magician":
            get = Magician;
            break;
        case "Priestess":
            get = Priestess;
            break;
        case "Hierophant":
            get = Hierophant;
            break;
        case "Sun":
            get = Sun;
            break;
        case "Moon":
            get = Moon;
            break;
        case "Star":
            get = Star;
            break;
        case "World":
            get = World;
            break;
        case "Strenght":
            get = Strenght;
            break;
        case "Temperance":
            get = Temperance;
            break;
        case "Justice":
            get = Justice;
            break;
        case "Judgement":
            get = Judgement;
            break;
        case "Empress":
            get = Empress;
            break;
        case "Emperor":
            get = Emperor;
            break;
        case "Death":
            get = Death;
            break;
        case "Devil":
            get = Devil;
            break;
        case "Hanged":
            get = Hanged;
            break;
        case "Tower":
            get = Tower;
            break;
        case "Chariot":
            get = Chariot;
            break;
        case "Lovers":
            get = Lovers;
            break;
        default:
            get = NoSkill;
            break;
    }

    Sprite skillSprite =(Sprite)Resources.Load(get.SpritePath, typeof(Sprite));
    get.SetSprite(skillSprite);
    s_skillSprites[get.ID] = skillSprite;

    return get;
}
}
