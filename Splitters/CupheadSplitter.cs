﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LiveSplit.Cuphead;
using LiveSplit.UI.Components;
using System.IO;


namespace HitCounterManager
{
    public class CupheadSplitter
    {
        public static SplitterMemory cup = new SplitterMemory();
        public DTCuphead dataCuphead;
        public bool _StatusProcedure = true;
        public bool _StatusCuphead = false;
        public ProfilesControl _profile;
        private static LiveSplit.Model.LiveSplitState state = new LiveSplit.Model.LiveSplitState();

        public DTCuphead getDataCuphead()
        {
            return this.dataCuphead;
        }
        public void setDataCuphead(DTCuphead data, ProfilesControl profile)
        {
            this.dataCuphead = data;
            this._profile = profile;
        }

        public bool getCupheadStatusProcess(int delay) //Use Delay 0 only for first Starts
        {
            Thread.Sleep(delay);
            return _StatusCuphead = cup.HookProcess();
        }

        public void setProcedure(bool procedure)
        {
            this._StatusProcedure = procedure;
            if (procedure) { LoadAutoSplitterProcedure(); }
        }

        public void setStatusSplitting(bool status)
        {
            dataCuphead.enableSplitting = status;
            if (status) {LoadAutoSplitterProcedure(); }
        }

        public void LoadAutoSplitterProcedure()
        {
            var taskRefresh = new Task(() =>
            {
                RefreshCuphead();
            });
            var task1 = new Task(() =>
            {
                elementToSplit();
            });
            taskRefresh.Start();
            task1.Start();
        }

        public void resetSplited()
        {
            if (dataCuphead.getElementToSplit().Count > 0)
            {
                foreach (var b in dataCuphead.getElementToSplit())
                {
                    b.IsSplited = false;
                }
            }
        }

        public void clearData()
        {
            dataCuphead.elementToSplit.Clear();
        }


        public void AddElement(string element)
        {
            DefinitionCuphead.ElementsToSplitCup cElem = new DefinitionCuphead.ElementsToSplitCup()
            {
                Title = element
            };
            dataCuphead.elementToSplit.Add(cElem);
        }

        public void RemoveElement(string element)
        {
            dataCuphead.elementToSplit.RemoveAll(i => i.Title == element);
        }

        #region init()
        public void RefreshCuphead()
        {
            int delay = 2000;
            _StatusCuphead = getCupheadStatusProcess(0);
            while (_StatusProcedure && dataCuphead.enableSplitting)
            {
                Thread.Sleep(10);
                _StatusCuphead = getCupheadStatusProcess(delay);
                if (!_StatusCuphead) { delay = 2000; } else { delay = 20000; }
            }
        }


        private bool ElementCase(string Title)
        {
            bool shouldSplit = false;
            switch (Title)
            {
                //Levels
                case "Forest Follies":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_1_1F" && cup.LevelComplete(Levels.Platforming_Level_1_1); break;
                case "Treetop Trouble":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_1_2F" && cup.LevelComplete(Levels.Platforming_Level_1_2); break;
                case "Funfair Fever":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_2_1F" && cup.LevelComplete(Levels.Platforming_Level_2_1); break;
                case "Funhouse Frazzle":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_2_2F" && cup.LevelComplete(Levels.Platforming_Level_2_2); break;
                case "Perilous Piers":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_3_1F" && cup.LevelComplete(Levels.Platforming_Level_3_1); break;
                case "Rugged Ridge":
                    shouldSplit = cup.SceneName() == "scene_level_platforming_3_2F" && cup.LevelComplete(Levels.Platforming_Level_3_2); break;
                case "Mausoleum I":
                    shouldSplit = cup.SceneName() == "scene_level_mausoleum" && cup.LevelMode() == Mode.Easy && (cup.LevelEnding() && cup.LevelWon()); break;
                case "Mausoleum II":
                    shouldSplit = cup.SceneName() == "scene_level_mausoleum" && cup.LevelMode() == Mode.Normal && (cup.LevelEnding() && cup.LevelWon()); break;
                case "Mausoleum III":
                    shouldSplit = cup.SceneName()== "scene_level_mausoleum" && cup.LevelMode() == Mode.Hard && (cup.LevelEnding() && cup.LevelWon()); break;
                case "Inkwell Isle 1":
                    shouldSplit = cup.SceneName() == "scene_map_world_1" && ((cup.SceneName() + (cup.InGame() ? " (In Game)" : ""))!= "scene_level_house_elder_kettle"); break;
                case "Inkwell Isle 2":
                    shouldSplit = cup.SceneName() == "scene_map_world_2" && ((cup.SceneName() + (cup.InGame() ? " (In Game)" : "")) != "scene_map_world_1"); break;
                case "Inkwell Isle 3":
                    shouldSplit = cup.SceneName() == "scene_map_world_3" && ((cup.SceneName() + (cup.InGame() ? " (In Game)" : "")) != "scene_map_world_2"); break;
                case "Inkwell Hell":
                    shouldSplit = cup.SceneName() == "scene_map_world_4" && ((cup.SceneName() + (cup.InGame() ? " (In Game)" : "")) != "scene_map_world_3"); break;

                //Bosses
                case "The Root Pack":
                    shouldSplit = cup.SceneName() == "scene_level_veggies" && cup.LevelComplete(Levels.Veggies); break;
                case "Goopy Le Grande":
                    shouldSplit = cup.SceneName() == "scene_level_slime" && cup.LevelComplete(Levels.Slime); break;
                case "Cagney Carnation":
                    shouldSplit = cup.SceneName() == "scene_level_flower" && cup.LevelComplete(Levels.Flower); break;
                case "Ribby And Croaks":
                    shouldSplit = cup.SceneName() == "scene_level_frogs" && cup.LevelComplete(Levels.Frogs); break;
                case "Hilda Berg":
                    shouldSplit = cup.SceneName() == "scene_level_flying_blimp" && cup.LevelComplete(Levels.FlyingBlimp); break;
                case "Baroness Von Bon Bon":
                    shouldSplit = cup.SceneName() == "scene_level_baroness" && cup.LevelComplete(Levels.Baroness); break;
                case "Djimmi The Great":
                    shouldSplit = cup.SceneName() == "scene_level_flying_genie" && cup.LevelComplete(Levels.FlyingGenie); break;
                case "Beppi The Clown":
                    shouldSplit = cup.SceneName() == "scene_level_clown" && cup.LevelComplete(Levels.Clown); break;
                case "Wally Warbles":
                    shouldSplit = cup.SceneName() == "scene_level_flying_bird" && cup.LevelComplete(Levels.FlyingBird); break;
                case "Grim Matchstick":
                    shouldSplit = cup.SceneName() == "scene_level_dragon" && cup.LevelComplete(Levels.Dragon); break;
                case "Rumor Honeybottoms":
                    shouldSplit = cup.SceneName() == "scene_level_bee" && cup.LevelComplete(Levels.Bee); break;
                case "Captin Brineybeard":
                    shouldSplit = cup.SceneName() == "scene_level_pirate" && cup.LevelComplete(Levels.Pirate); break;
                case "Werner Werman":
                    shouldSplit = cup.SceneName() == "scene_level_mouse" && cup.LevelComplete(Levels.Mouse); break;
                case "Dr. Kahl's Robot":
                    shouldSplit = cup.SceneName() == "scene_level_robot" && cup.LevelComplete(Levels.Robot); break;
                case "Sally Stageplay":
                    shouldSplit = cup.SceneName() == "scene_level_sally_stage_play" && cup.LevelComplete(Levels.SallyStagePlay); break;
                case "Cala Maria":
                    shouldSplit = cup.SceneName() == "scene_level_flying_mermaid" && cup.LevelComplete(Levels.FlyingMermaid); break;
                case "Phantom Express":
                    shouldSplit = cup.SceneName() == "scene_level_train" && cup.LevelComplete(Levels.Train); break;
                case "King Dice":
                    shouldSplit = cup.SceneName() == "scene_level_dice_palace_main" && cup.LevelComplete(Levels.DicePalaceMain); break;
                case "Devil":
                    shouldSplit = cup.SceneName() == "scene_level_devil" && cup.LevelComplete(Levels.Devil); break;
                default: shouldSplit = false; break;       
            }
            return shouldSplit;
        }

        private void elementToSplit()
        {
            while (dataCuphead.enableSplitting && _StatusProcedure)
            {
                foreach (var element in dataCuphead.getElementToSplit())
                {
                    if (!element.IsSplited && ElementCase(element.Title))
                    {
                        element.IsSplited = true;
                        try { _profile.ProfileSplitGo(+1); } catch (Exception) { };
                    }
                }
            }
        }
        #endregion
    }
}