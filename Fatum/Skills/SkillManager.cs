using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public SkillsLogic _skillsLogic;
    public void UseHermitSkill(){
        _skillsLogic.HermitSkill();
    }
    public void UsePrimarySkill(string skillName){
        switch (skillName){
            case "Hermit":
                _skillsLogic.HermitSkill();
                break;
            default:
            UnityEngine.Debug.Log(skillName);
            break;
        }
    }
    public void UseMoveSkill(string skillName){
        
    }
    public void UsePassiveSkill(string skillName){
        switch (skillName){
            case "Lovers":
                _skillsLogic.UseLoversSkill();
                break;
            default:
            UnityEngine.Debug.Log(skillName);
            break;
        }
    }
    public Skill GetSkill(string skillName){
        Skill skill = SkillDB.GetSkillByName(skillName);
        return skill;
    }
}
