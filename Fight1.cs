using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fluent;


public class Fight1 : MyFluentDialogue
{
    public int EnemyHealth = 10;
    bool EnemystillFighting = true;
    public int PlayerHealth = 10;
    public bool canUseSpellsGuard = false;
    public override void OnStart()
    {
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
            EnemystillFighting = false;
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
            Do(() => DoDamage(damage)) *
            Yell(Eval(() => EnemyHealth + " hp left")) *
            Yell(Eval(() => PlayerHealth + " Player hp left")) *
            Show() *
            If(() => EnemyHealth <= 0,
                Hide() *
                Yell("you win") *
                Yell("your brother is free to go")
            ) *
            If(() => PlayerHealth <= 0,
                Hide() *
                Yell("you loose") *
                Yell("you can try again later")
            ) *
            End();
    }   
 
    public FluentNode SpellList()
    {
        //canUseSpellsGuard = canUseSpells;
        if(canUseSpellsGuard == true){
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
                    If(() => EnemyHealth >= 10, Write(0, "If you can beat me in combat ill let your brother go")) *
                    If(() => EnemyHealth >= 6 && EnemyHealth < 10, Write(0, "Almost good enough")) *
                    If(() => EnemyHealth >= 2 && EnemyHealth < 6, Write(0, "Not today")) *

                    Option("Spells") *
                        Write(0, "Choose a spell") *
                        SpellList() *
 
                    AttackOption("Melee", "Hand 2 Hand!", 2) *                        
 
                    Option("Flee") *
                        Hide() *
                        Yell("Coward!") *
                        Do(() => EnemystillFighting = false) *
                        End()
                )
            );
    }
}