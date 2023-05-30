using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill
{
    public enum SkillType{
        Main, Pasive, Movement, None, Mask
    }
    public string Name;
    public string SkillAction;
    public int ID;
    public float Cost;
    public float Cooldown;
    public SkillType Type;
    public string SpritePath;
    public string Descritption;
    public Sprite skillSprite;

    public Skill(string name, string action, int id, float cost, float cd, SkillType type, string path, string description){
        Name = name;
        SkillAction = action;
        ID = id;
        Cost = cost;
        Cooldown = cd;
        Type = type;
        SpritePath = path;
        Descritption = description;

    }
    public void SetSprite(Sprite s)
    {
        skillSprite = s;
    }
    
}
