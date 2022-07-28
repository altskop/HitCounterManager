﻿//MIT License

//Copyright (c) 2019-2022 Ezequiel Medina

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SoulMemory;

namespace HitCounterManager
{
    public partial class AutoSplitter : Form
    {
        SekiroSplitter sekiroSplitter;
        HollowSplitter hollowSplitter;
        
        

        public Exception Exception { get; set; }

        public AutoSplitter(SekiroSplitter sekiroSplitter, HollowSplitter hollowSplitter)
        {
            InitializeComponent();
            this.sekiroSplitter = sekiroSplitter;
            this.hollowSplitter = hollowSplitter;
            refreshForm();
        }

        public void refreshForm()
        {
            #region SekiroTab       
            panelPositionS.Hide();
            panelBossS.Hide();
            panelIdolsS.Hide();
            groupBoxAshinaOutskirts.Hide();
            groupBoxHirataEstate.Hide();
            groupBoxAshinaCastle.Hide();
            groupBoxAbandonedDungeon.Hide();
            groupBoxSenpouTemple.Hide();
            groupBoxSunkenValley.Hide();
            groupBoxAshinaDepths.Hide();
            groupBoxFountainhead.Hide();

            #endregion
            #region HollowTab
            panelBossH.Hide();
            panelItemH.Hide();
            groupBossH.Hide();
            groupBoxMBH.Hide();
            groupBoxPantheon.Hide();
            checkedListBoxPantheon.Hide();
            checkedListBoxPp.Hide();
            lbl_warning.Hide();
            groupBoxCharms.Hide();
            groupBoxSkillsH.Hide();
            panelPositionH.Hide();

            #endregion
            checkStatusGames();
        }

        private void AutoSplitter_Load(object sender, EventArgs e)
        {
            DTSekiro sekiroData = sekiroSplitter.getDataSekiro();
            #region SekiroLoad.Bosses
            foreach (DefinitionsSekiro.Boss boss in sekiroData.getBossToSplit())
            {
                listBoxBosses.Items.Add(boss.Title + " - " + boss.Mode);
            }
            #endregion
            #region SekiroLoad.Idols
            foreach (DefinitionsSekiro.Idol idols in sekiroData.getidolsTosplit())
            {
                for (int i = 0; i < checkedListBoxAshina.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAshina.Items[i].ToString())
                    {
                        checkedListBoxAshina.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxHirataEstate.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxHirataEstate.Items[i].ToString())
                    {
                        checkedListBoxHirataEstate.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxAshinaCastle.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAshinaCastle.Items[i].ToString())
                    {
                        checkedListBoxAshinaCastle.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxAbandonedDungeon.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAbandonedDungeon.Items[i].ToString())
                    {
                        checkedListBoxAbandonedDungeon.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxSenpouTemple.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxSenpouTemple.Items[i].ToString())
                    {
                        checkedListBoxSenpouTemple.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxSunkenValley.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxSunkenValley.Items[i].ToString())
                    {
                        checkedListBoxSunkenValley.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxAshinaDepths.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxAshinaDepths.Items[i].ToString())
                    {
                        checkedListBoxAshinaDepths.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxFountainhead.Items.Count; i++)
                {
                    if (idols.Title == checkedListBoxFountainhead.Items[i].ToString())
                    {
                        checkedListBoxFountainhead.SetItemChecked(i, true);
                    }
                }

            }
            #endregion
            #region SekiroLoad.Position
            foreach (DefinitionsSekiro.Position position in sekiroData.getPositionsToSplit())
            {
                listBoxPositionsS.Items.Add(position.vector + " - " + position.mode);
            }
            comboBoxMarginS.SelectedIndex = sekiroData.positionMargin;
            #endregion
            #region UpdateLoad
            string fullPath = Path.GetFullPath("SoulMemory.dll");
            this.lblVersionCurrent.Text = System.Reflection.Assembly.LoadFile(fullPath).GetName().Version.ToString();
            #endregion
            DTHollow hollowData = hollowSplitter.getDataHollow();
            #region HollowLoad.Boss
            foreach (var b in hollowData.getBosstoSplit())
            {
                for (int i = 0; i < checkedListBoxBossH.Items.Count; i++)
                {
                    if (b.Title == checkedListBoxBossH.Items[i].ToString())
                    {
                        checkedListBoxBossH.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.MiniBoss
            foreach (var mb in hollowData.getMiniBossToSplit())
            {
                for (int i = 0; i < checkedListBoxHMB.Items.Count; i++)
                {
                    if (mb.Title == checkedListBoxHMB.Items[i].ToString())
                    {
                        checkedListBoxHMB.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.Pantheon
            foreach (var p in hollowData.getPhanteonToSplit())
            {
                for (int i = 0; i < checkedListBoxPantheon.Items.Count; i++)
                {
                    if (p.Title == checkedListBoxPantheon.Items[i].ToString())
                    {
                        checkedListBoxPantheon.SetItemChecked(i, true);
                    }
                }

                for (int i = 0; i < checkedListBoxPp.Items.Count; i++)
                {
                    if (p.Title == checkedListBoxPp.Items[i].ToString())
                    {
                        checkedListBoxPp.SetItemChecked(i, true);
                    }
                }

            }
            comboBoxHowP.SelectedIndex = hollowData.PantheonMode;

            #endregion
            #region HollowLoad.Charm
            foreach (var c in hollowData.getCharmToSplit())
            {
                for (int i = 0; i < checkedListBoxCharms.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxCharms.Items[i].ToString())
                    {
                        checkedListBoxCharms.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.Skill
            foreach (var c in hollowData.getSkillsToSplit())
            {
                for (int i = 0; i < checkedListBoxSkillsH.Items.Count; i++)
                {
                    if (c.Title == checkedListBoxSkillsH.Items[i].ToString())
                    {
                        checkedListBoxSkillsH.SetItemChecked(i, true);
                    }
                }
            }
            #endregion
            #region HollowLoad.Position
            foreach (var p in hollowData.getPositionToSplit())
            {
                listBoxPositionH.Items.Add(p.position + p.sceneName);
            }
            comboBoxMarginH.SelectedIndex = hollowData.positionMargin;
            #endregion
        }

        private void refresh_Btn(object sender, EventArgs e)
        {
            checkStatusGames();
        }

        public void checkStatusGames()
        {
            Exception excS = null;
            if (sekiroSplitter.getSekiroStatusProcess(out excS, 0))
            {
                sekiroRunning.Show();
                SekiroNotRunning.Hide();
            }
            else
            {
                SekiroNotRunning.Show();
                sekiroRunning.Hide();
            }
            if (hollowSplitter.getHollowStatusProcess(0))
            {
                HollowRunning.Show();
                HollowNotRunning.Hide();
            }
            else
            {
                HollowRunning.Hide();
                HollowNotRunning.Show();
            }


        }

        #region Sekiro.UI
        Vector3f Vector;
        private void toSplitSelectSekiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panelPositionS.Hide();
            this.panelBossS.Hide();
            this.panelIdolsS.Hide();


            switch (toSplitSelectSekiro.SelectedIndex)
            {
                case 0: //Kill a Boss
                    this.panelBossS.Show();
                    break;
                case 1: // Is Activated a Idol
                    this.panelIdolsS.Show();
                    break;
                case 2: //Target Position
                    this.panelPositionS.Show();
                    break;
            }
        }




        private void btnGetPotition_Click(object sender, EventArgs e)
        {
            var Vector = sekiroSplitter.getCurrentPosition();
            this.Vector = Vector;
            this.textBoxX.Clear();
            this.textBoxY.Clear();
            this.textBoxZ.Clear();
            this.textBoxX.Paste(Vector.X.ToString("0.00"));
            this.textBoxY.Paste(Vector.Y.ToString("0.00"));
            this.textBoxZ.Paste(Vector.Z.ToString("0.00"));

        }

        private void btnAddPosition_Click(object sender, EventArgs e)
        {
            DialogResult error;
            if (this.Vector != null)
            {
                var contains1 = !listBoxPositionsS.Items.Contains(this.Vector + " - " + "Inmediatly");
                var contains2 = !listBoxPositionsS.Items.Contains(this.Vector + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    
                    if (comboBoxHowPosition.SelectedIndex == -1)
                    {
                        error = MessageBox.Show("Select 'How' do you want split ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (this.Vector.X == 0 && this.Vector.Y == 0 && this.Vector.Z == 0)
                        {
                            error = MessageBox.Show("Dont use cords 0,0,0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            sekiroSplitter.setProcedure(false);
                            listBoxPositionsS.Items.Add(this.Vector + " - " + comboBoxHowPosition.Text.ToString());
                            error = MessageBox.Show("Move your charapter to evit autosplitting ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            sekiroSplitter.AddPosition(this.Vector, comboBoxHowPosition.Text.ToString());
                            sekiroSplitter.setProcedure(true);
                        }
                    }
                }
                else
                {
                    error = MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                error = MessageBox.Show("Plase get a position ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listBoxPositions_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (this.listBoxPositionsS.SelectedItem != null)
            {
                int i = listBoxPositionsS.Items.IndexOf(listBoxPositionsS.SelectedItem);
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemovePosition(i);
                sekiroSplitter.setProcedure(true);
                listBoxPositionsS.Items.Remove(listBoxPositionsS.SelectedItem);
            }
        }

        private void comboBoxMargin_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select = comboBoxMarginS.SelectedIndex;
            sekiroSplitter.setPositionMargin(select);
        }

        private void btn_AddBoss_Click(object sender, EventArgs e)
        {
            DialogResult error;
            if (comboBoxBoss.SelectedIndex == -1 || comboBoxHowBoss.SelectedIndex == -1)
            {
                error = MessageBox.Show("Plase select boss and 'How' do you want split  ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var contains1 = !listBoxPositionsS.Items.Contains(comboBoxBoss.Text.ToString() + " - " + "Inmediatly");
                var contains2 = !listBoxPositionsS.Items.Contains(comboBoxBoss.Text.ToString() + " - " + "Loading game after");
                if (contains1 && contains2)
                {
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.AddBoss(comboBoxBoss.Text.ToString(), comboBoxHowBoss.Text.ToString());
                    listBoxBosses.Items.Add(comboBoxBoss.Text.ToString() + " - " + comboBoxHowBoss.Text.ToString());
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    error = MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBoxBosses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listBoxBosses.SelectedItem != null)
            {
                int i = listBoxBosses.Items.IndexOf(listBoxBosses.SelectedItem);
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.RemoveBoss(i);
                sekiroSplitter.setProcedure(true);
                listBoxBosses.Items.Remove(listBoxBosses.SelectedItem);
            }
        }

        private void comboBoxZoneSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBoxAshinaOutskirts.Hide();
            groupBoxHirataEstate.Hide();
            groupBoxAshinaCastle.Hide();
            groupBoxAbandonedDungeon.Hide();
            groupBoxSenpouTemple.Hide();
            groupBoxSunkenValley.Hide();
            groupBoxAshinaDepths.Hide();
            groupBoxFountainhead.Hide();

            switch (comboBoxZoneSelect.SelectedIndex)
            {
                case 0: //Ashina Outskirts
                    groupBoxAshinaOutskirts.Show();
                    break;
                case 1: //Hirata Estate
                    groupBoxHirataEstate.Show();
                    break;
                case 2: //Ashina Castle
                    groupBoxAshinaCastle.Show();
                    break;
                case 3: //Abandoned Dungeon
                    groupBoxAbandonedDungeon.Show();
                    break;
                case 4: //Senpou Temple
                    groupBoxSenpouTemple.Show();
                    break;
                case 5: //Sunken Valley
                    groupBoxSunkenValley.Show();
                    break;
                case 6: //Ashina Depths
                    groupBoxAshinaDepths.Show();
                    break;
                case 7: //Fountainhead Palace
                    groupBoxFountainhead.Show();
                    break;
            }
        }

        private void listBoxAshinaOutskirts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAshinaOutskirts.SelectedIndex != -1)
            {
                labelIdolSelectedAO.Text = listBoxAshinaOutskirts.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAshinaOutskirts.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmAO.Checked = false;
                    radioLagAO.Checked = true;

                }
                else
                {
                    radioImmAO.Checked = true;
                    radioLagAO.Checked = false;
                }
            }
        }

        private void listBoxHirataEstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxHirataEstate.SelectedIndex != -1)
            {
                labelIdolSelectedHE.Text = listBoxHirataEstate.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxHirataEstate.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmHE.Checked = false;
                    radioLagHE.Checked = true;

                }
                else
                {
                    radioImmHE.Checked = true;
                    radioLagHE.Checked = false;
                }
            }

        }

        private void listBoxAshinaCastle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAshinaCastle.SelectedIndex != -1)
            {
                labelIdolSelectedAC.Text = listBoxAshinaCastle.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAshinaCastle.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmAC.Checked = false;
                    radioLagAC.Checked = true;

                }
                else
                {
                    radioImmAC.Checked = true;
                    radioLagAC.Checked = false;
                }
            }
        }

        private void listBoxAbandonedDungeon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAbandonedDungeon.SelectedIndex != -1)
            {
                labelIdolSelectedAD.Text = listBoxAbandonedDungeon.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAbandonedDungeon.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmAD.Checked = false;
                    radioLagAD.Checked = true;

                }
                else
                {
                    radioImmAD.Checked = true;
                    radioLagAD.Checked = false;
                }
            }
        }

        private void listBoxSunkenValley_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSunkenValley.SelectedIndex != -1)
            {
                labelIdolSelectedSV.Text = listBoxSunkenValley.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxSunkenValley.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmSV.Checked = false;
                    radioLagSV.Checked = true;

                }
                else
                {
                    radioImmSV.Checked = true;
                    radioLagSV.Checked = false;
                }
            }
        }

        private void listBoxAshinaDepths_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAshinaDepths.SelectedIndex != -1)
            {
                labelIdolSelectedADe.Text = listBoxAshinaDepths.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxAshinaDepths.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmADe.Checked = false;
                    radioLagADe.Checked = true;

                }
                else
                {
                    radioImmADe.Checked = true;
                    radioLagADe.Checked = false;
                }
            }
        }

        private void listBoxSenpouTemple_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSenpouTemple.SelectedIndex != -1)
            {
                labelIdolSelectedTS.Text = listBoxSenpouTemple.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxSenpouTemple.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmTS.Checked = false;
                    radioLagTS.Checked = true;

                }
                else
                {
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
        }

        private void listBoxFountainhead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFountainhead.SelectedIndex != -1)
            {
                labelIdolSelectedF.Text = listBoxFountainhead.SelectedItem.ToString();
                string mode = sekiroSplitter.FindIdol(listBoxFountainhead.SelectedItem.ToString());
                if (mode == "Loading game after")
                {
                    radioImmF.Checked = false;
                    radioLagF.Checked = true;

                }
                else
                {
                    radioImmF.Checked = true;
                    radioLagF.Checked = false;
                }
            }
        }

        private void btnAddAshinaOutskirts_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAshinaOutskirts.SelectedIndex != -1)
            {
                if (!checkedListBoxAshina.GetItemChecked(listBoxAshinaOutskirts.SelectedIndex))
                {
                    checkedListBoxAshina.SetItemChecked(listBoxAshinaOutskirts.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmAO.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAshinaOutskirts.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxAshina.SetItemChecked(listBoxAshinaOutskirts.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxAshinaOutskirts.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmAO.Checked = true;
                    radioLagAO.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddHirata_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxHirataEstate.SelectedIndex != -1)
            {
                if (!checkedListBoxHirataEstate.GetItemChecked(listBoxHirataEstate.SelectedIndex))
                {
                    checkedListBoxHirataEstate.SetItemChecked(listBoxHirataEstate.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmHE.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxHirataEstate.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxHirataEstate.SetItemChecked(listBoxHirataEstate.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxHirataEstate.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmHE.Checked = true;
                    radioLagHE.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddAC_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAshinaCastle.SelectedIndex != -1)
            {
                if (!checkedListBoxAshinaCastle.GetItemChecked(listBoxAshinaCastle.SelectedIndex))
                {
                    checkedListBoxAshinaCastle.SetItemChecked(listBoxAshinaCastle.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmAC.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAshinaCastle.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxAshinaCastle.SetItemChecked(listBoxAshinaCastle.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxAshinaCastle.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmAC.Checked = true;
                    radioLagAC.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddAD_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAbandonedDungeon.SelectedIndex != -1)
            {
                if (!checkedListBoxAbandonedDungeon.GetItemChecked(listBoxAbandonedDungeon.SelectedIndex))
                {
                    checkedListBoxAbandonedDungeon.SetItemChecked(listBoxAbandonedDungeon.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmAC.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAbandonedDungeon.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxAbandonedDungeon.SetItemChecked(listBoxAbandonedDungeon.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxAbandonedDungeon.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmAC.Checked = true;
                    radioLagAC.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddTS_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxSenpouTemple.SelectedIndex != -1)
            {
                if (!checkedListBoxSenpouTemple.GetItemChecked(listBoxSenpouTemple.SelectedIndex))
                {
                    checkedListBoxSenpouTemple.SetItemChecked(listBoxSenpouTemple.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmTS.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxSenpouTemple.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxSenpouTemple.SetItemChecked(listBoxSenpouTemple.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxSenpouTemple.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddSV_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxSunkenValley.SelectedIndex != -1)
            {
                if (!checkedListBoxSunkenValley.GetItemChecked(listBoxSunkenValley.SelectedIndex))
                {
                    checkedListBoxSunkenValley.SetItemChecked(listBoxSunkenValley.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmTS.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxSunkenValley.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxSunkenValley.SetItemChecked(listBoxSunkenValley.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxSunkenValley.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddADe_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxAshinaDepths.SelectedIndex != -1)
            {
                if (!checkedListBoxAshinaDepths.GetItemChecked(listBoxAshinaDepths.SelectedIndex))
                {
                    checkedListBoxAshinaDepths.SetItemChecked(listBoxAshinaDepths.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmTS.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxAshinaDepths.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxAshinaDepths.SetItemChecked(listBoxAshinaDepths.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxAshinaDepths.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmTS.Checked = true;
                    radioLagTS.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddF_Click(object sender, EventArgs e)
        {
            string mode;
            if (listBoxFountainhead.SelectedIndex != -1)
            {
                if (!checkedListBoxFountainhead.GetItemChecked(listBoxFountainhead.SelectedIndex))
                {
                    checkedListBoxFountainhead.SetItemChecked(listBoxFountainhead.SelectedIndex, true);
                    sekiroSplitter.setProcedure(false);
                    if (radioImmF.Checked)
                    {
                        mode = "Inmediatly";
                    }
                    else
                    {
                        mode = "Loading game after";
                    }
                    sekiroSplitter.AddIdol(listBoxFountainhead.SelectedItem.ToString(), mode);
                    sekiroSplitter.setProcedure(true);
                }
                else
                {
                    checkedListBoxFountainhead.SetItemChecked(listBoxFountainhead.SelectedIndex, false);
                    sekiroSplitter.setProcedure(false);
                    sekiroSplitter.RemoveIdol(listBoxFountainhead.SelectedItem.ToString());
                    sekiroSplitter.setProcedure(true);
                    radioImmF.Checked = true;
                    radioLagF.Checked = false;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Select a item before to add?", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesactiveSekiro_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                sekiroSplitter.setProcedure(false);
                sekiroSplitter.clearData();
                sekiroSplitter.setProcedure(true);
                this.Controls.Clear();
                this.InitializeComponent();
                refreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
            }

        }

        #endregion
        #region Update.UI
        private void btnCheckVersion_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/FrankvdStam/SoulSplitter/releases");
        }
        #endregion
        #region Hollow UI
        private void toSplitSelectHollow_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBossH.Hide();
            panelItemH.Hide();
            panelPositionH.Hide();
            
            switch (toSplitSelectHollow.SelectedIndex)
            {
                case 0: //Kill a enemy
                    panelBossH.Show();
                    break;
                case 1: //Obtain a item
                    panelItemH.Show();
                    break;
                case 2: //Get Position
                    panelPositionH.Show();
                    break;

            }
        }

        private void comboBoxSelectKindBoss_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBossH.Hide();
            groupBoxMBH.Hide();
            groupBoxPantheon.Hide();

            switch (comboBoxSelectKindBoss.SelectedIndex)
            {
                case 0: //Boss
                    groupBossH.Show();
                    break;
                case 1: //Phanteom
                    groupBoxPantheon.Show();
                    break;
                case 2: //MiniBoss - Dreamers- Coliseum and Others
                    groupBoxMBH.Show();
                    break;

            }
        }

        private void checkedListBoxBossH_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxBossH.SelectedIndex != -1)
            {
                if (checkedListBoxBossH.GetItemChecked(checkedListBoxBossH.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddBoss(checkedListBoxBossH.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemoveBoss(checkedListBoxBossH.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }

        private void checkedListBoxMBH_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxHMB.SelectedIndex != -1)
            {
                if (checkedListBoxHMB.GetItemChecked(checkedListBoxHMB.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddMiniBoss(checkedListBoxHMB.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemoveMiniBoss(checkedListBoxHMB.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }

        private void checkedListBoxPantheon_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxPantheon.SelectedIndex != -1)
            {
                if (checkedListBoxPantheon.GetItemChecked(checkedListBoxPantheon.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddPantheon(checkedListBoxPantheon.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemovePantheon(checkedListBoxPantheon.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }

        private void checkedListBoxPp_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxPp.SelectedIndex != -1)
            {
                if (checkedListBoxPp.GetItemChecked(checkedListBoxPp.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddPantheon(checkedListBoxPp.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemovePantheon(checkedListBoxPp.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }


        private void checkedListBoxCharms_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxCharms.SelectedIndex != -1)
            {
                if (checkedListBoxCharms.GetItemChecked(checkedListBoxCharms.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddCharm(checkedListBoxCharms.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemoveCharm(checkedListBoxCharms.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }


        private void checkedListBoxSkillsH_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxSkillsH.SelectedIndex != -1)
            {
                if (checkedListBoxSkillsH.GetItemChecked(checkedListBoxSkillsH.SelectedIndex) == false)
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.AddSkill(checkedListBoxSkillsH.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
                else
                {
                    hollowSplitter.setProcedure(false);
                    hollowSplitter.RemoveSkill(checkedListBoxSkillsH.SelectedItem.ToString());
                    hollowSplitter.setProcedure(true);
                }
            }
        }
        private void comboBoxHowP_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBoxPantheon.Hide();
            checkedListBoxPp.Hide();
            lbl_warning.Hide();

            if (comboBoxHowP.SelectedIndex != -1)
            {
                switch (comboBoxHowP.SelectedIndex)
                {
                    case 0: //P1+P2+P3+P4 or P5                       
                        checkedListBoxPantheon.Show();
                        lbl_warning.Show();
                        hollowSplitter.dataHollow.PantheonMode = 0;
                        break;
                    case 1: //Split one per Pantheon
                        checkedListBoxPp.Show();
                        hollowSplitter.dataHollow.PantheonMode = 1;
                        break;

                }
            }
        }

       

        private void comboBoxItemSelectH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxItemSelectH.SelectedIndex != -1)
            {
                groupBoxCharms.Hide();
                groupBoxSkillsH.Hide();
                switch (comboBoxItemSelectH.SelectedIndex)
                {
                    case 0: //Skills                     
                        groupBoxSkillsH.Show();
                        break;
                    case 1: //Charms
                        groupBoxCharms.Show();
                        break;

                }
            }
        }

        PointF VectorH;
        private void btn_getPositionH_Click(object sender, EventArgs e)
        {

            var Vector = hollowSplitter.getCurrentPosition();
            this.VectorH = Vector;
            this.textBoxXh.Clear();
            this.textBoxYh.Clear();
            this.textBoxSh.Clear();
            this.textBoxXh.Paste(Vector.X.ToString("0.00"));
            this.textBoxYh.Paste(Vector.Y.ToString("0.00"));
            this.textBoxSh.Paste(hollowSplitter.currentPosition.sceneName);
        }

        private void comboBoxMarginH_SelectedIndexChanged(object sender, EventArgs e)
        {
            int select = comboBoxMarginH.SelectedIndex;
            hollowSplitter.dataHollow.positionMargin = select;
        }

        private void btn_AddPositionH_Click(object sender, EventArgs e)
        {
            DialogResult error;
            if (this.VectorH != null)
            {
                var contains1 = !listBoxPositionH.Items.Contains(this.VectorH + textBoxSh.Text);
                if (contains1)
                {
                    
                    if (this.VectorH.X == 0 && this.VectorH.Y == 0)
                    {
                        error = MessageBox.Show("Dont use cords 0,0", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        listBoxPositionH.Items.Add(this.VectorH + textBoxSh.Text);
                        error = MessageBox.Show("Move your charapter to evit autosplitting ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        hollowSplitter.setProcedure(false);
                        hollowSplitter.AddPosition(this.VectorH, textBoxSh.Text);
                        hollowSplitter.setProcedure(true);
                    }
                }
                else
                {
                    error = MessageBox.Show("You have already added this trigger", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                error = MessageBox.Show("Plase get a position ", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void listBoxPositionH_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxPositionH.SelectedItem != null)
            {
                int i = listBoxPositionH.Items.IndexOf(listBoxPositionH.SelectedItem);
                hollowSplitter.setProcedure(false);
                hollowSplitter.RemovePosition(i);
                hollowSplitter.setProcedure(true);
                listBoxPositionH.Items.Remove(listBoxPositionH.SelectedItem);
            }
        }

        private void btn_DesactiveAllH_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to disable everything?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                hollowSplitter.setProcedure(false);
                hollowSplitter.clearData();
                hollowSplitter.setProcedure(true);
                this.Controls.Clear();
                this.InitializeComponent();
                refreshForm();
                this.AutoSplitter_Load(null, null);//Load Others Games Settings
            }
        }
        #endregion
    }
}