using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fluent;


public class Cackle : MyFluentDialogue
{
    public int EnemyHealth = 10;
    bool EnemystillFighting = true;
    public int PlayerHealth = 10;
    public bool canUseSpells = false;
    //private Fight1 fight1;

    public override void OnStart()
    {
        //fight1 = GameObject.FindObjectOfType<Fight1>();
        EnemyHealth = 10;
        PlayerHealth = 10;
        EnemystillFighting = true;
        base.OnStart();
    }
 
    void DoDamage(int damage)
    {
        EnemyHealth -= damage;
        PlayerHealth= PlayerHealth - 3;
        if (EnemyHealth <= 0){
            canUseSpells = true;
            EnemystillFighting = false;
            //fight1.SpellList (canUseSpells);
        }
        if(PlayerHealth <= 0){
            EnemystillFighting = false;
        }
    }
 
    FluentNode AttackOption(string itemName, string yell, int damage)
    {
        return
            Option(itemName) *
            Hide() *
            Yell("You delt " + damage + " damage") *
            Yell("You took 1 damage")*
            Do(() => DoDamage(damage)) *
            Yell(Eval(() => EnemyHealth + " Wizard hp left")) *
            Yell(Eval(() => PlayerHealth + " Player hp left")) *
            Show() *
            If(() => EnemyHealth <= 0,
                Hide() *
                Yell("I died!") *
                Yell("You gain spells")
            ) *
            If(() => PlayerHealth <= 0,
                Hide() *
                Yell("you loose") *
                Yell("you can try again later")
            ) *
            End();
    }   
 
    FluentNode SpellList()
    {
      if(canUseSpells == true){
        return
            Options
            (   
                
                AttackOption("Magic blot", "Zap", 4) *
                AttackOption("Fire ball", "Boom!", 2) *
                Option("Back") *
                    Back()
            );
      }
      else{
        return
            Options
            (   
                Write(0, "You dont have any spells")*
                AttackOption("Magic blot", "Zap", 0) *
                AttackOption("Fire ball", "Boom!", 0) *
                Option("Back") *
                    Back()
            );
      }
    }
 
    public override FluentNode Create()
    {
        return
            Show() *
            While(() => EnemystillFighting,
                Show() * 
                Options
                (
                    If(() => EnemyHealth >= 10, Write(0, "You cant die your brother is going to die")) *
                    If(() => EnemyHealth >= 6 && EnemyHealth < 10, Write(0, "*Cackle*")) *
                    If(() => EnemyHealth >= 2 && EnemyHealth < 6, Write(0, "wait i can give you riches")) *

                    Option("Spells") *
                        Write(0, "Choose a spell") *
                        SpellList() *
 
                    AttackOption("Melee", "Hand 2 Hand!", 2) *                        
 
                    Option("Flee") *
                        Hide() *
                        Yell("*cackle* idiot") *
                        Do(() => EnemystillFighting = false) *
                        End()
                )
            );
    }
}

