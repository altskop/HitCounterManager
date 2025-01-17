﻿//MIT License

//Copyright (c) 2022 Ezequiel Medina

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
using System.Reflection;
using System.Windows.Forms;
using SoulMemory;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AutoSplitterCore
{
    public partial class Debug : Form
    {
        private AutoSplitterMainModule mainModule;
        private int GameActive = 0;
        private System.Windows.Forms.Timer _update_timer = new System.Windows.Forms.Timer() { Interval = 100 };
        private System.Windows.Forms.Timer _update_flag = new System.Windows.Forms.Timer() { Interval = 10000 };

        public Debug(AutoSplitterMainModule mainModule)
        {
            InitializeComponent();
            this.mainModule = mainModule;
            mainModule.InitDebug();
            mainModule.LoadAutoSplitterSettings(new HitCounterManager.ProfilesControl(),null);
            _update_timer.Tick += (sender, args) => CheckInfo();
            _update_timer.Enabled = true;
            _update_flag.Tick += (sender, args) => checkBoxAutoToggle_CheckedChanged(null, null);
            _update_flag.Enabled = true;
        }

        private void Debug_Load(object sender, EventArgs e)
        {
            comboBoxIGTConversion.SelectedIndex = 1;
            checkBoxPracticeMode.Checked = mainModule.GetPracticeMode();
            checkBoxAutoHit.Checked = mainModule.GetAutoHit();
            List<string> GameList = mainModule.GetGames();
            foreach (string i in GameList) comboBoxGame.Items.Add(i);
            switch (mainModule.GetSplitterEnable())
            {
                case GameConstruction.SekiroSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.SekiroSplitterIndex; break;
                case GameConstruction.Ds1SplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.Ds1SplitterIndex; break;
                case GameConstruction.Ds2SplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.Ds2SplitterIndex; break;
                case GameConstruction.Ds3SplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.Ds3SplitterIndex; break;
                case GameConstruction.EldenSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.EldenSplitterIndex; break;
                case GameConstruction.HollowSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.HollowSplitterIndex; break;
                case GameConstruction.CelesteSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.CelesteSplitterIndex; break;
                case GameConstruction.CupheadSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.CupheadSplitterIndex; break;
                case GameConstruction.DishonoredSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.DishonoredSplitterIndex; break;
                case GameConstruction.ASLSplitterIndex: comboBoxGame.SelectedIndex = GameConstruction.ASLSplitterIndex; break;
                case GameConstruction.NoneSplitterIndex:
                default: comboBoxGame.SelectedIndex = GameConstruction.NoneSplitterIndex; break;
            }
            LabelVersion.Text = mainModule.updateModule.currentVer;
            labelCloudVer.Text = mainModule.updateModule.cloudVer;
            checkBoxAutoToggle.Checked = true;
            mainModule.debugForm = this;
        }
        private void comboBoxGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainModule.EnableSplitting(0);
            mainModule.EnableSplitting(comboBoxGame.SelectedIndex);
            GameActive = comboBoxGame.SelectedIndex;
            mainModule.igtModule.gameSelect = GameActive;
        }

        public void UpdateBoxes()
        {
            comboBoxGame.SelectedIndex = mainModule.GetSplitterEnable();
            checkBoxPracticeMode.Checked = mainModule.GetPracticeMode();
            checkBoxAutoHit.Checked = mainModule.GetAutoHit();
        }

        bool debugSplit = false;
        private void CheckInfo()
        {
            this.textBoxX.Clear();
            this.textBoxY.Clear();
            this.textBoxZ.Clear();
            this.textBoxSceneName.Clear();
            this.textBoxIGT.Clear();
            bool status = false;
            int conv = 1;
            switch (comboBoxIGTConversion.SelectedIndex)
            {
                case 0: conv = 1; break;
                case 1: conv = 1000; break;
                case 2: conv = 60000; break;
            }

            switch (GameActive)
            {
                case GameConstruction.SekiroSplitterIndex:
                    var Vector1 = mainModule.sekiroSplitter.getCurrentPosition();
                    this.textBoxX.Paste(Vector1.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector1.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector1.Z.ToString("0.00"));
                    status = mainModule.sekiroSplitter._StatusSekiro;
                    debugSplit = mainModule.sekiroSplitter._SplitGo;
                    break;
                case GameConstruction.Ds1SplitterIndex:
                    var Vector2 = mainModule.ds1Splitter.getCurrentPosition();
                    this.textBoxX.Paste(Vector2.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector2.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector2.Z.ToString("0.00"));
                    status = mainModule.ds1Splitter._StatusDs1;
                    debugSplit = mainModule.ds1Splitter._SplitGo;
                    break;
                case GameConstruction.Ds2SplitterIndex:
                    var Vector3 = mainModule.ds2Splitter.getCurrentPosition();
                    this.textBoxX.Paste(Vector3.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector3.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector3.Z.ToString("0.00"));
                    status = mainModule.ds2Splitter._StatusDs2;
                    debugSplit = mainModule.ds2Splitter._SplitGo;
                    break;
                case GameConstruction.Ds3SplitterIndex: 
                    status = mainModule.ds3Splitter._StatusDs3;
                    break;
                case GameConstruction.EldenSplitterIndex:
                   var Vector5 = mainModule.eldenSplitter.getCurrentPosition(); 
                    this.textBoxX.Paste(Vector5.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector5.Y.ToString("0.00"));
                    this.textBoxZ.Paste(Vector5.Z.ToString("0.00"));
                    status = mainModule.eldenSplitter._StatusElden;
                    debugSplit = mainModule.ds3Splitter._SplitGo;
                    break;
                case GameConstruction.HollowSplitterIndex: 
                    var Vector6 = mainModule.hollowSplitter.getCurrentPosition();
                    this.textBoxX.Paste(Vector6.X.ToString("0.00"));
                    this.textBoxY.Paste(Vector6.Y.ToString("0.00"));
                    this.textBoxSceneName.Paste(mainModule.hollowSplitter.currentPosition.sceneName.ToString());
                    // update health label text to "Health: " + health + ", lastHealth: " + lastHealth + ", hits: " + _profile.SelectedProfileInfo.GetSplitWayHits();
                    this.healthLabel.Text = "Health: " + mainModule.hollowSplitter.currentHealth + ", lastHealth: " + mainModule.hollowSplitter.lastHealth + ", hits: " + mainModule.hollowSplitter._profile.SelectedProfileInfo.ProfileName;
                    status = mainModule.hollowSplitter._StatusHollow;
                    debugSplit = mainModule.hollowSplitter._SplitGo;
                    break;
                case GameConstruction.CelesteSplitterIndex:
                    this.textBoxSceneName.Paste(mainModule.celesteSplitter.getLevelName());
                    status = mainModule.celesteSplitter._StatusCeleste;
                    debugSplit = mainModule.celesteSplitter._SplitGo;
                    break;
                case GameConstruction.CupheadSplitterIndex:
                    this.textBoxSceneName.Paste(mainModule.cupSplitter.GetSceneName());
                    status = mainModule.cupSplitter._StatusCuphead;
                    debugSplit = mainModule.cupSplitter._SplitGo;
                    break;
                case GameConstruction.DishonoredSplitterIndex:
                    status = mainModule.dishonoredSplitter._StatusDish;
                    debugSplit = mainModule.dishonoredSplitter._SplitGo; break;
                case GameConstruction.ASLSplitterIndex:
                    debugSplit = mainModule.aslSplitter._SplitGo; break;
                    
                case GameConstruction.NoneSplitterIndex:
                default: break;
            }
            this.textBoxIGT.Paste((mainModule.ReturnCurrentIGT() / conv).ToString());
            if (status) { Running.Show(); NotRunning.Hide(); } else { NotRunning.Show(); Running.Hide(); }
            if (debugSplit) { btnStatusSplitting.BackColor = System.Drawing.Color.Green; } else { btnStatusSplitting.BackColor = System.Drawing.Color.Red; }

        }

        private void btnSplitter_Click(object sender, EventArgs e)
        {
            mainModule.AutoSplitterForm(false);
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            mainModule.SaveAutoSplitterSettings();
            MessageBox.Show("Save Successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefreshGame_Click(object sender, EventArgs e)
        {
            switch (GameActive)
            {
                case GameConstruction.SekiroSplitterIndex:
                    mainModule.sekiroSplitter.getSekiroStatusProcess(0);
                    break;
                case GameConstruction.Ds1SplitterIndex:
                    mainModule.ds1Splitter.getDs1StatusProcess(0);
                    break;
                case GameConstruction.Ds2SplitterIndex:
                    mainModule.ds2Splitter.getDs2StatusProcess(0);
                    break;
                case GameConstruction.Ds3SplitterIndex:
                    mainModule.ds3Splitter.getDs3StatusProcess(0);
                    break;
                case GameConstruction.EldenSplitterIndex:
                    mainModule.eldenSplitter.getEldenStatusProcess(0);
                    break;
                case GameConstruction.HollowSplitterIndex:
                    mainModule.hollowSplitter.getHollowStatusProcess(0);
                    break;
                case GameConstruction.CelesteSplitterIndex:
                    mainModule.celesteSplitter.getCelesteStatusProcess(0);
                    break;
                case GameConstruction.CupheadSplitterIndex:
                    mainModule.cupSplitter.getCupheadStatusProcess(0);
                    break;
                case GameConstruction.DishonoredSplitterIndex:
                    mainModule.dishonoredSplitter.getDishonoredStatusProcess();
                    break;
                case GameConstruction.ASLSplitterIndex:
                    mainModule.aslSplitter.UpdateScript();
                    break;
                case GameConstruction.NoneSplitterIndex:
                default: break;
            }
        }

        private void textBoxCfID_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCfID.Text != null && textBoxCfID.Text != String.Empty)
            {
                try
                {
                    var flag = uint.Parse(textBoxCfID.Text);
                    bool status = false;
                    if (mainModule.GameOn())
                    {
                        switch (GameActive)
                        {
                            case GameConstruction.SekiroSplitterIndex:
                                status = mainModule.sekiroSplitter.CheckFlag(flag);
                                break;
                            case GameConstruction.Ds1SplitterIndex:
                                status = mainModule.ds1Splitter.CheckFlag(flag);
                                break;
                            case GameConstruction.Ds2SplitterIndex:
                                status = mainModule.ds2Splitter.CheckFlag(flag);
                                break;
                            case GameConstruction.Ds3SplitterIndex:
                                status = mainModule.ds3Splitter.CheckFlag(flag);
                                break;
                            case GameConstruction.EldenSplitterIndex:
                                status = mainModule.eldenSplitter.CheckFlag(flag);
                                break;
                            case GameConstruction.HollowSplitterIndex:
                                break;
                            case GameConstruction.CelesteSplitterIndex:
                                break;
                            case GameConstruction.CupheadSplitterIndex:
                                break;
                            case GameConstruction.DishonoredSplitterIndex:
                                break;
                            case GameConstruction.ASLSplitterIndex:
                            case GameConstruction.NoneSplitterIndex:
                            default: break;
                        }
                        if (status) { btnSplitCf.BackColor = System.Drawing.Color.Green; } else { btnSplitCf.BackColor = System.Drawing.Color.Red; }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Check Flag", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBoxPracticeMode_CheckedChanged(object sender, EventArgs e)
        {
            mainModule.SetPracticeMode(checkBoxPracticeMode.Checked);
        }

        private void checkBoxAutoHit_CheckedChanged(object sender, EventArgs e)
        {
            mainModule.SetAutoHit(checkBoxAutoHit.Checked);
        }

        private void btnStatusSplitting_Click(object sender, EventArgs e)
        {
            switch (GameActive)
            {
                case GameConstruction.SekiroSplitterIndex: 
                    mainModule.sekiroSplitter._SplitGo = !debugSplit;
                    break;
                case GameConstruction.Ds1SplitterIndex:
                    mainModule.ds1Splitter._SplitGo = !debugSplit;
                    break;
                case GameConstruction.Ds2SplitterIndex: 
                    mainModule.ds2Splitter._SplitGo = !debugSplit;
                    break;
                case GameConstruction.Ds3SplitterIndex:
                    mainModule.ds3Splitter._SplitGo = !debugSplit;
                    break;
                case GameConstruction.EldenSplitterIndex: 
                    mainModule.eldenSplitter._SplitGo = !debugSplit;
                    break;
                case GameConstruction.HollowSplitterIndex:
                    mainModule.hollowSplitter._SplitGo = !debugSplit;
                    break;
                case GameConstruction.CelesteSplitterIndex:
                   mainModule.eldenSplitter._SplitGo = !debugSplit;
                    break;
                case GameConstruction.CupheadSplitterIndex:
                    mainModule.cupSplitter._SplitGo = !debugSplit;
                    break;
                case GameConstruction.DishonoredSplitterIndex:
                    mainModule.dishonoredSplitter._SplitGo = !debugSplit;
                    break;
                case GameConstruction.ASLSplitterIndex:
                    mainModule.aslSplitter._SplitGo = !debugSplit;
                    break;
                case GameConstruction.NoneSplitterIndex:
                    break;
            }
        }

        private void checkBoxAutoToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoToggle.Checked && debugSplit)
            {
                btnStatusSplitting_Click(null, null);
                debugSplit = false;
            }
        }

        private void btnResetFlags_Click(object sender, EventArgs e)
        {
            btnStatusSplitting_Click(null, null);
            debugSplit = false;
            mainModule.ResetSplitterFlags();
        }

        private void btnSplitCf_Click(object sender, EventArgs e)
        {
            textBoxCfID_TextChanged(null, null);
        }
    }
}
