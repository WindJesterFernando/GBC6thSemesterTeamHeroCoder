﻿using System;
using System.IO;
using System.Collections.Generic;
using TeamHeroCoderLibrary;
//using static TeamHeroCoderLibrary.TeamHeroCoder;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
        PartyAIManager partyAIManager = new MyAI();
        partyAIManager.SetExchangePath("/Users/fernandorestituto/Library/Application Support/Wind Jester Games/Team Hero Coder");
        partyAIManager.StartReadingAndProcessingInfiniteLoop();
    }
}

public class MyAI : PartyAIManager
{


    //Brave
    //Slow
    //Ehter
    //CHeck if hit will KO and do so if so,
    //int amount = TeamHeroCoder.CalculateDamageAmount(TeamHeroCoder.BattleState.heroWithInitiative, TeamHeroCoder.BattleActionParams.MagicMissileDamageMod, target, false);



    public override void ProcessAI()
    {
        Console.WriteLine("Processing AI!");

        #region SampleCode

        if (TeamHeroCoder.BattleState.heroWithInitiative.characterClassID == TeamHeroCoder.HeroClassID.Fighter)
        {
            Console.WriteLine("this is a fighter");



            foreach (Hero h in TeamHeroCoder.BattleState.playerHeroes)
            {
                if (h.characterClassID == TeamHeroCoder.HeroClassID.Cleric)
                {
                    float healthPercent = (float)h.health / (float)h.maxHealth;

                    if (healthPercent < 0.6f && h.health > 0)
                    {
                        TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.CureSerious, h);
                        return;
                    }

                }
            }

            foreach (Hero h in TeamHeroCoder.BattleState.playerHeroes)
            {
                if (h.health == 0 && h.characterClassID == TeamHeroCoder.HeroClassID.Cleric)
                {
                    Console.WriteLine("KO cleric found");
                    TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.Resurrection, h);
                    return;
                }
            }

            foreach (Hero h in TeamHeroCoder.BattleState.playerHeroes)
            {
                if (h.health == 0)
                {
                    Console.WriteLine("KO cleric found");
                    TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.Resurrection, h);
                    return;
                }
            }



            //Find the foe that is not KO and has the lowest health
            Hero target = null;

            foreach (Hero hero in TeamHeroCoder.BattleState.foeHeroes)
            {
                if (hero.health > 0)
                {
                    if (target == null)
                        target = hero;
                    else if (hero.health < target.health)
                        target = hero;
                }
            }

            //This is the line of code that tells Team Hero Coder that we want to perform the attack action targeting the foe with the lowest HP
            TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.Attack, target);
        }
        else if (TeamHeroCoder.BattleState.heroWithInitiative.characterClassID == TeamHeroCoder.HeroClassID.Cleric)
        {
            Console.WriteLine("this is a cleric");



            int heroBelow70Percent = 0;

            foreach (Hero h in TeamHeroCoder.BattleState.playerHeroes)
            {
                float healthPercent = (float)h.health / (float)h.maxHealth;

                if (healthPercent < 0.7f && h.health > 0)
                {
                    heroBelow70Percent++;
                }
            }

            if (heroBelow70Percent >= 2)
            {
                TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.MassHeal, TeamHeroCoder.BattleState.playerHeroes[0]);
                return;
            }




            foreach (Hero h in TeamHeroCoder.BattleState.playerHeroes)
            {
                float healthPercent = (float)h.health / (float)h.maxHealth;

                if (healthPercent < 0.7f && h.health > 0)
                {
                    TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.CureLight, h);
                    return;
                }

            }



            foreach (Hero h in TeamHeroCoder.BattleState.playerHeroes)
            {
                if (h.characterClassID == TeamHeroCoder.HeroClassID.Wizard)
                {
                    bool doesHaveFaith = false;

                    foreach (StatusEffect se in h.statusEffects)
                    {
                        if (se.id == TeamHeroCoder.StatusEffectID.Faith)
                            doesHaveFaith = true;
                    }

                    if (!doesHaveFaith)
                    {
                        TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.Faith, h);
                    }
                }

            }




            bool clericDoesHaveHaste = false;
            Hero cleric = TeamHeroCoder.BattleState.heroWithInitiative;

            foreach (StatusEffect se in cleric.statusEffects)
            {
                if (se.id == TeamHeroCoder.StatusEffectID.Haste)
                    clericDoesHaveHaste = true;
            }

            if (!clericDoesHaveHaste)
            {
                TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.Haste, cleric);
            }



            Hero target = null;

            foreach (Hero hero in TeamHeroCoder.BattleState.foeHeroes)
            {
                if (hero.health > 0)
                {
                    if (target == null)
                        target = hero;
                    else if (hero.health < target.health)
                        target = hero;
                }
            }

            //This is the line of code that tells Team Hero Coder that we want to perform the attack action targeting the foe with the lowest HP
            TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.Attack, target);






        }
        else if (TeamHeroCoder.BattleState.heroWithInitiative.characterClassID == TeamHeroCoder.HeroClassID.Wizard)
        {
            Console.WriteLine("this is a wizard");


            if (TeamHeroCoder.AreAbilityAndTargetLegal(TeamHeroCoder.AbilityID.Meteor, null, false))
            {
                Console.WriteLine("attempting to cast meteor");
                TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.Meteor, TeamHeroCoder.BattleState.foeHeroes[0]);
            }
            else
            {
                Console.WriteLine("not attempting to cast meteor");
                //Find the foe that is not KO and has the lowest health
                Hero target = null;

                foreach (Hero hero in TeamHeroCoder.BattleState.foeHeroes)
                {
                    if (hero.health > 0)
                    {
                        if (target == null)
                            target = hero;
                        else if (hero.health < target.health)
                            target = hero;
                    }
                }

                Console.WriteLine(TeamHeroCoder.HeroClassID.lookUp[target.characterClassID] + " has been select as lowest health target.");

                if (TeamHeroCoder.AreAbilityAndTargetLegal(TeamHeroCoder.AbilityID.MagicMissile, target, true))
                {
                    int amount = TeamHeroCoder.CalculateDamageAmount(TeamHeroCoder.BattleState.heroWithInitiative, TeamHeroCoder.BattleActionParams.MagicMissileDamageMod, target, false);
                    Console.WriteLine("Magic Missile Damage == " + amount);
                    TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.MagicMissile, target);
                }
                else
                {
                    TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.Attack, target);
                }
            }

        }




        #endregion


        // foreach (InventoryItem ii in TeamHeroCoder.BattleState.playerInventory)
        // {
        //     if(ii.id == TeamHeroCoder.ItemID.Potion)
        //     {
        //         Console.WriteLine("Player has " + ii.count + " potion(s), total gold cost of player potions == " + ii.count * TeamHeroCoder.ItemCost.Potion);
        //     }
        // }

        // foreach(Hero h in TeamHeroCoder.BattleState.foeHeroes)
        // {
        //     Console.WriteLine(TeamHeroCoder.HeroClassID.lookUp[h.characterClassID]);
        // }


        // //Searching for a poisoned hero 
        // foreach (Hero hero in TeamHeroCoder.BattleState.playerHeroes)
        // {
        //     foreach (StatusEffect se in hero.statusEffects)
        //     {
        //         if (se.id == TeamHeroCoder.StatusEffectID.Poison)
        //         {
        //             Console.WriteLine(TeamHeroCoder.HeroClassID.lookUp[hero.characterClassID] + " is poisoned");
        //         }
        //     }
        // }

        // //Find the foe that is not KO and has the lowest health
        // Hero target = null;

        // foreach (Hero hero in TeamHeroCoder.BattleState.foeHeroes)
        // {
        //     if (hero.health > 0)
        //     {
        //         if (target == null)
        //             target = hero;
        //         else if (hero.health < target.health)
        //             target = hero;
        //     }
        // }

        // Console.WriteLine(TeamHeroCoder.HeroClassID.lookUp[target.characterClassID] + " has been select as lowest health target.");

        // //This is the line of code that tells Team Hero Coder that we want to perform the attack action targeting the foe with the lowest HP
        // TeamHeroCoder.PerformHeroAbility(TeamHeroCoder.AbilityID.Attack, target);




        #region Extended Sample Code Readings

        // int dmgAmount = TeamHeroCoder.CalculateDamageAmount(TeamHeroCoder.BattleState.heroWithInitiative, TeamHeroCoder.BattleActionParams.AttackDamageMod, target, true);
        // Console.WriteLine("Unless intercepted by cover, the amount of damage that will be done == " + dmgAmount);


        // if (TeamHeroCoder.AreAbilityAndTargetLegal(TeamHeroCoder.AbilityID.MagicMissile, target, false))
        //     Console.WriteLine("The hero with initiative is capable of casting Magic Missile.");
        // else
        //     Console.WriteLine("The hero with initiative is not capable of casting Magic Missile.");


        // if (TeamHeroCoder.AreAbilityAndTargetLegal(TeamHeroCoder.AbilityID.Meteor, null, false))
        //     Console.WriteLine("The hero with initiative is capable of casting Meteor.");
        // else
        //     Console.WriteLine("The hero with initiative is not capable of casting Meteor.");


        // List<Hero> playerHeroesThatAreNotKOAndHaveHealthLessThanPercentAmount = new List<Hero>();
        // foreach (Hero hero in TeamHeroCoder.BattleState.playerHeroes)
        // {
        //     if (hero.health <= 0)
        //         continue;

        //     float p = (float)hero.health / (float)hero.maxHealth;
        //     if (p < 0.4f)
        //         playerHeroesThatAreNotKOAndHaveHealthLessThanPercentAmount.Add(hero);
        // }


        // List<Hero> playerHeroesThatAreNotKOAndHaveManaLessThanPercentAmount = new List<Hero>();
        // foreach (Hero hero in TeamHeroCoder.BattleState.playerHeroes)
        // {
        //     if (hero.health <= 0)
        //         continue;

        //     float p = (float)hero.mana / (float)hero.maxMana;
        //     if (p < 0.4f)
        //         playerHeroesThatAreNotKOAndHaveManaLessThanPercentAmount.Add(hero);
        // }


        // List<Hero> foeHeroesWithRogueHeroClass = new List<Hero>();
        // foreach (Hero hero in TeamHeroCoder.BattleState.foeHeroes)
        // {
        //     if (hero.characterClassID == TeamHeroCoder.HeroClassID.Rogue)
        //         foeHeroesWithRogueHeroClass.Add(hero);
        // }


        // List<Hero> foeHeroesWithWizardEvokerPerk = new List<Hero>();
        // foreach (Hero h in TeamHeroCoder.BattleState.foeHeroes)
        // {
        //     foreach (int p in h.perkIDs)
        //     {
        //         if (p == TeamHeroCoder.PerkID.WizardEvoker)
        //             foeHeroesWithWizardEvokerPerk.Add(h);
        //     }
        // }


        // List<Hero> foeHeroesWithCoverPassiveAbility = new List<Hero>();
        // foreach (Hero h in TeamHeroCoder.BattleState.foeHeroes)
        // {
        //     foreach (int p in h.passiveAbilityIDs)
        //     {
        //         if (p == TeamHeroCoder.PassiveAbilityID.Cover)
        //             foeHeroesWithCoverPassiveAbility.Add(h);
        //     }
        // }


        #endregion

    }
}
