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
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace AutoSplitterCore
{
    public partial class ProfileManager : Form
    {
        private static string savePath = String.Empty;
        private readonly string defaultPath = Path.GetFullPath("./AutoSplitterProfiles");
        private readonly string CustomSavePath = Path.GetFullPath("./PathSaveProfileAutoSplitter.xml");
        private SaveModule saveModule = null;

        public ProfileManager(SaveModule saveModule)
        {
            InitializeComponent();
            this.saveModule = saveModule;
        }

        private void ProfileManager_Load(object sender, EventArgs e)
        {
            btnSetProfile.Hide();
            btnSetAuthor.Hide();

            if (File.Exists(CustomSavePath))
            {
                savePath = File.ReadAllText(CustomSavePath);
                if (savePath == String.Empty)
                {
                    savePath = defaultPath;
                    File.Delete(CustomSavePath);
                }
            }
            else
            {
                savePath = defaultPath;
            }
           
            if (!Directory.Exists(savePath) && savePath == defaultPath )
            {
                Directory.CreateDirectory(savePath);
            }

            RefreshForm();
        }

        private int prevIndex = -1;
        public void RefreshForm()
        {
            textBoxSavePath.Text = savePath;
            textBoxCurrrentProfile.Text = saveModule.GetProfileName();
            textBoxAuthor.Text = saveModule.GetAuthor();
            
            prevIndex = comboBoxProfiles.SelectedIndex;

            comboBoxProfiles.Items.Clear();
            foreach (string file in Directory.GetFiles(savePath))
            {
                var auxfile = file.Remove(0, savePath.Length+1);
                if (auxfile.Contains("xml"))
                {
                    comboBoxProfiles.Items.Add(auxfile);
                }                
            }
            comboBoxProfiles.Refresh();
            if (comboBoxProfiles.Items.Count > 0 && prevIndex < 0)
                comboBoxProfiles.SelectedIndex = 0;
            else
            {
                if (comboBoxProfiles.Items.Count == 0) prevIndex = -1;
                    comboBoxProfiles.SelectedIndex = prevIndex;
            }          

            RefreshSummary();
        }

        private void RefreshSummary()
        {
            saveModule.UpdateAutoSplitterData();
            String Summary = String.Empty;
            String Line = "\r\n";
            string Space = "      ";
            #region GeneralSummary
            Summary += "=======================================================" + Line;
            Summary += "General:" + Line;
            Summary += "=======================================================" + Line;
            Summary += "Profile: " + saveModule.GetProfileName() + Line;
            Summary += "Author: " + saveModule.GetAuthor() + Line;
            Summary += "Practice Mode: " + saveModule.GetPracticeMode() + Line;
            Summary += "Game Selected: " + saveModule.GetGameSelected() + Line;
            Summary += Line;
            #endregion
            #region TimingSummary
            Summary += "=======================================================" + Line;
            Summary += "Timing:" + Line;
            Summary += "=======================================================" + Line;
            Summary += "- Sekiro: " + Line;
            Summary += "      AutoTimer: " + saveModule.dataAS.DataSekiro.autoTimer + Line;
            Summary += "      Method: "; _ = saveModule.dataAS.DataSekiro.gameTimer ? Summary += "IGT" : Summary += "Real Time"; Summary += Line;
            Summary += Line;
            Summary += "- Dark Souls 1: " + Line;
            Summary += "      AutoTimer: " + saveModule.dataAS.DataDs1.autoTimer + Line;
            Summary += "      Method: "; _ = saveModule.dataAS.DataDs1.gameTimer ? Summary += "IGT" : Summary += "Real Time"; Summary += Line;
            Summary += Line;
            Summary += "- Dark Souls 2: " + Line;
            Summary += "      AutoTimer: " + saveModule.dataAS.DataDs2.autoTimer + Line;
            Summary += "      Method: "; _ = saveModule.dataAS.DataDs2.gameTimer ? Summary += "IGT" : Summary += "Real Time"; Summary += Line;
            Summary += Line;
            Summary += "- Dark Souls 3: " + Line;
            Summary += "      AutoTimer: " + saveModule.dataAS.DataDs3.autoTimer + Line;
            Summary += "      Method: "; _ = saveModule.dataAS.DataDs3.gameTimer ? Summary += "IGT" : Summary += "Real Time"; Summary += Line;
            Summary += Line;
            Summary += "- Elden Ring: " + Line;
            Summary += "      AutoTimer: " + saveModule.dataAS.DataElden.autoTimer + Line;
            Summary += "      Method: "; _ = saveModule.dataAS.DataElden.gameTimer ? Summary += "IGT" : Summary += "Real Time"; Summary += Line;
            Summary += Line;
            Summary += "- Hollow Knight: " + Line;
            Summary += "      AutoTimer: " + saveModule.dataAS.DataHollow.autoTimer + Line;
            Summary += "      Method: "; _ = saveModule.dataAS.DataHollow.gameTimer ? Summary += "IGT" : Summary += "Real Time"; Summary += Line;
            Summary += Line;
            Summary += "- Celeste: " + Line;
            Summary += "      AutoTimer: " + saveModule.dataAS.DataCeleste.autoTimer + Line;
            Summary += "      Method: "; _ = saveModule.dataAS.DataCeleste.gameTimer ? Summary += "IGT" : Summary += "Real Time"; Summary += Line;
            Summary += Line;
            Summary += "- Cuphead: " + Line;
            Summary += "      AutoTimer: " + saveModule.dataAS.DataCuphead.autoTimer + Line;
            Summary += "      Method: "; _ = saveModule.dataAS.DataCuphead.gameTimer ? Summary += "IGT" : Summary += "Real Time"; Summary += Line;
            Summary += Line;
            Summary += "- Dishonored: " + Line;
            Summary += "      AutoTimer: " + saveModule.dataAS.DataDishonored.autoTimer + Line;
            Summary += "      Method: "; _ = saveModule.dataAS.DataDishonored.gameTimer ? Summary += "IGT" : Summary += "Real Time"; Summary += Line;
            Summary += Line;
            #endregion
            #region SekiroSummary
            Summary += "=======================================================" + Line;
            Summary += "Sekiro Flags:" + Line;
            Summary += "=======================================================" + Line;
            Summary += "Bosses: " + Line;
            if (saveModule.dataAS.DataSekiro.bossToSplit.Count > 0)
            {
                foreach (var b in saveModule.dataAS.DataSekiro.bossToSplit)
                {
                    Summary += Space + b.Title + " - " + b.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "MiniBosses: " + Line;
            if (saveModule.dataAS.DataSekiro.miniBossToSplit.Count > 0)
            {
                foreach (var b in saveModule.dataAS.DataSekiro.miniBossToSplit)
                {
                    Summary += Space + b.Title + " - " + b.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;


            Summary += "Idols: " + Line;
            if (saveModule.dataAS.DataSekiro.idolsTosplit.Count > 0)
            {
                foreach (var idol in saveModule.dataAS.DataSekiro.idolsTosplit)
                {
                    Summary += Space + idol.Title + " - " + idol.Location + " - " + idol.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Positions: " + Line;
            Summary += Space + "Margin: " + saveModule.dataAS.DataSekiro.positionMargin +Line;
            if (saveModule.dataAS.DataSekiro.positionsToSplit.Count > 0)
            {
                foreach (var position in saveModule.dataAS.DataSekiro.positionsToSplit)
                {
                    Summary += Space + position.vector + " - " + position.Mode + Line;
                }
            }
            else
                Summary += Space + Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Mortal Journey: " + Line;
            Summary += Space + "Enable: " + saveModule.dataAS.DataSekiro.mortalJourneyRun + Line;
            Summary += Line;

            Summary += "Custom Flags: " + Line;
            if (saveModule.dataAS.DataSekiro.flagToSplit.Count > 0)
            {
                foreach (var cf in saveModule.dataAS.DataSekiro.flagToSplit)
                {
                    Summary += Space + cf.Id + " - " + cf.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;
            #endregion
            #region DarkSouls1Summary
            Summary += "=======================================================" + Line;
            Summary += "Dark Souls 1 Flags:" + Line;
            Summary += "=======================================================" + Line;
            Summary += "Bosses: " + Line;
            if (saveModule.dataAS.DataDs1.bossToSplit.Count > 0)
            {
                foreach (var b in saveModule.dataAS.DataDs1.bossToSplit)
                {
                    Summary += Space + b.Title + " - " + b.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Bonfire: " + Line;
            if (saveModule.dataAS.DataDs1.bonfireToSplit.Count > 0)
            {
                foreach (var bonDs1 in saveModule.dataAS.DataDs1.bonfireToSplit)
                {
                    Summary += Space + bonDs1.Title + " - " + bonDs1.Value + " - " + bonDs1.Mode + Line; 
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Attributes: " + Line;
            if (saveModule.dataAS.DataDs1.lvlToSplit.Count > 0)
            {
                foreach (var lvl in saveModule.dataAS.DataDs1.lvlToSplit)
                {
                    Summary += Space + lvl.Attribute + ": " + lvl.Value + " - " + lvl.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Positions: " + Line;
            Summary += Space + "Margin: " + saveModule.dataAS.DataDs1.positionMargin + Line;
            if (saveModule.dataAS.DataDs1.positionsToSplit.Count > 0)
            {
                foreach (var sb in saveModule.dataAS.DataDs1.positionsToSplit)
                {
                    Summary += Space + sb.vector + " - " + sb.Mode + Line;
                }
            }
            else
                Summary += Space + Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Attributes: " + Line;
            if (saveModule.dataAS.DataDs1.lvlToSplit.Count > 0)
            {
                foreach (var LvlDs1 in saveModule.dataAS.DataDs1.lvlToSplit)
                {
                    Summary += Space + LvlDs1.Attribute + ": " + LvlDs1.Value + " - " + LvlDs1.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Items: " + Line;
            if (saveModule.dataAS.DataDs1.itemToSplit.Count > 0)
            {
                foreach (var items in saveModule.dataAS.DataDs1.itemToSplit)
                {
                    Summary += Space + items.Title + " - " + items.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;
            #endregion
            #region DarkSouls2Summary
            Summary += "=======================================================" + Line;
            Summary += "Dark Souls 2 Flags:" + Line;
            Summary += "=======================================================" + Line;

            Summary += "Bosses: " + Line;
            if (saveModule.dataAS.DataDs2.bossToSplit.Count > 0)
            {
                foreach (var b in saveModule.dataAS.DataDs2.bossToSplit)
                {
                    Summary += Space + b.Title + " - " + b.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Attributes: " + Line;
            if (saveModule.dataAS.DataDs2.lvlToSplit.Count > 0)
            {
                foreach (var lvl in saveModule.dataAS.DataDs2.lvlToSplit)
                {
                    Summary += Space + lvl.Attribute + ": " + lvl.Value + " - " + lvl.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Positions: " + Line;
            Summary += Space + "Margin: " + saveModule.dataAS.DataDs2.positionMargin + Line;
            if (saveModule.dataAS.DataDs2.positionsToSplit.Count > 0)
            {
                foreach (var sb in saveModule.dataAS.DataDs2.positionsToSplit)
                {
                    Summary += Space + sb.vector + " - " + sb.Mode + Line;
                }
            }
            else
                Summary += Space + Space + "Not Have Flags" + Line;
            Summary += Line;
            #endregion
            #region DarkSouls3Summary
            Summary += "=======================================================" + Line;
            Summary += "Dark Souls 3 Flags:" + Line;
            Summary += "=======================================================" + Line;

            Summary += "Bosses: " + Line;
            if (saveModule.dataAS.DataDs3.bossToSplit.Count > 0)
            {
                foreach (var b in saveModule.dataAS.DataDs3.bossToSplit)
                {
                    Summary += Space + b.Title + " - " + b.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Attributes: " + Line;
            if (saveModule.dataAS.DataDs3.lvlToSplit.Count > 0)
            {
                foreach (var lvl in saveModule.dataAS.DataDs3.lvlToSplit)
                {
                    Summary += Space + lvl.Attribute + ": " + lvl.Value + " - " + lvl.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Bonfire: " + Line;
            if (saveModule.dataAS.DataDs3.bonfireToSplit.Count > 0)
            {
                foreach (var bon in saveModule.dataAS.DataDs3.bonfireToSplit)
                {
                    Summary += Space + bon.Title + " - " + bon.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Custom Flags: " + Line;
            if (saveModule.dataAS.DataDs3.flagToSplit.Count > 0)
            {
                foreach (var cf in saveModule.dataAS.DataDs3.flagToSplit)
                {
                    Summary += Space + cf.Id + " - " + cf.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Positions: " + Line;
            Summary += Space + "Margin: " + saveModule.dataAS.DataDs3.positionMargin + Line;
            if (saveModule.dataAS.DataDs3.positionsToSplit.Count > 0)
            {
                foreach (var sb in saveModule.dataAS.DataDs3.positionsToSplit)
                {
                    Summary += Space + sb.vector + " - " + sb.Mode + Line;
                }
            }
            else
                Summary += Space + Space + "Not Have Flags" + Line;
            Summary += Line;
            #endregion
            #region EldenRingSummary
            Summary += "=======================================================" + Line;
            Summary += "Elden Ring Flags:" + Line;
            Summary += "=======================================================" + Line;

            Summary += "Bosses: " + Line;
            if (saveModule.dataAS.DataElden.bossToSplit.Count > 0)
            {
                foreach (var b in saveModule.dataAS.DataElden.bossToSplit)
                {
                    Summary += Space + b.Title + " - " + b.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Grace: " + Line;
            if (saveModule.dataAS.DataElden.graceToSplit.Count > 0)
            {
                foreach (var bon in saveModule.dataAS.DataElden.graceToSplit)
                {
                    Summary += Space + bon.Title + " - " + bon.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Positions: " + Line;
            Summary += Space + "Margin: " + saveModule.dataAS.DataElden.positionMargin + Line;
            if (saveModule.dataAS.DataElden.positionToSplit.Count > 0)
            {
                foreach (var sb in saveModule.dataAS.DataDs2.positionsToSplit)
                {
                    Summary += Space + sb.vector + " - " + sb.Mode + Line;
                }
            }
            else
                Summary += Space + Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Custom Flags: " + Line;
            if (saveModule.dataAS.DataElden.flagsToSplit.Count > 0)
            {
                foreach (var cf in saveModule.dataAS.DataElden.flagsToSplit)
                {
                    Summary += Space + cf.Id + " - " + cf.Mode + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;
            #endregion
            #region HollowKnightSummary
            Summary += "=======================================================" + Line;
            Summary += "Hollow Knight Flags:" + Line;
            Summary += "=======================================================" + Line;


            Summary += "Bosses: " + Line;
            if (saveModule.dataAS.DataHollow.bossToSplit.Count > 0)
            {
                foreach (var b in saveModule.dataAS.DataHollow.bossToSplit)
                {
                    Summary += Space + b.Title + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "MiniBosses: " + Line;
            if (saveModule.dataAS.DataHollow.miniBossToSplit.Count > 0)
            {
                foreach (var b in saveModule.dataAS.DataHollow.miniBossToSplit)
                {
                    Summary += Space + b.Title + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Pantheon: " + Line;
            if (saveModule.dataAS.DataHollow.phanteonToSplit.Count > 0)
            {
                Summary += Space + "Mode: "; _ = saveModule.dataAS.DataHollow.PantheonMode == 0 ? Summary += "Per Boss Split" : Summary += "Per Pantheon Split"; Summary += Line;
                foreach (var p in saveModule.dataAS.DataHollow.phanteonToSplit)
                {
                    Summary += Space + p.Title + Line;
                }
            }else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Skills: " + Line;
            if (saveModule.dataAS.DataHollow.skillsToSplit.Count > 0)
            {
                foreach (var s in saveModule.dataAS.DataHollow.skillsToSplit)
                {
                    Summary += Space + s.Title + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;


            Summary += "Charms: " + Line;
            if (saveModule.dataAS.DataHollow.charmToSplit.Count > 0)
            {
                foreach (var s in saveModule.dataAS.DataHollow.charmToSplit)
                {
                    Summary += Space + s.Title + Line;
                }
            }
            else
                Summary += Space + "Not Have Flags" + Line;
            Summary += Line;

            Summary += "Positions: " + Line;
            Summary += Space + "Margin: " + saveModule.dataAS.DataHollow.positionMargin + Line;
            if (saveModule.dataAS.DataHollow.positionToSplit.Count > 0)
            {
                foreach (var p in saveModule.dataAS.DataHollow.positionToSplit)
                {
                    Summary += Space + p.position + " - " + p.sceneName + Line;
                }
            }
            else
                Summary += Space + Space + "Not Have Flags" + Line;
            Summary += Line;
            #endregion
            #region CelesteSummary
            Summary += "=======================================================" + Line;
            Summary += "Celeste Flags:" + Line;
            Summary += "=======================================================" + Line;

            Summary += "Chapters/Checkpoints: " + Line;
            if (saveModule.dataAS.DataCeleste.chapterToSplit.Count > 0)
            {
                foreach (var c in saveModule.dataAS.DataCeleste.chapterToSplit)
                {
                    Summary += Space + c.Title + Line;
                }
            }
            else
                Summary += Space + Space + "Not Have Flags" + Line;
            Summary += Line;
            #endregion
            #region CupheadSummary
            Summary += "=======================================================" + Line;
            Summary += "Cuphead Flags:" + Line;
            Summary += "=======================================================" + Line;
            Summary += "Boss/World: " + Line;
            if (saveModule.dataAS.DataCuphead.elementToSplit.Count > 0)
            {
                foreach (var c in saveModule.dataAS.DataCuphead.elementToSplit)
                {
                    Summary += Space + c.Title + Line;
                }
            }
            else
                Summary += Space + Space + "Not Have Flags" + Line;
            Summary += Line;
            #endregion
            #region DishonoredSummary
            Summary += "=======================================================" + Line;
            Summary += "Dishonored Flags:" + Line;
            Summary += "=======================================================" + Line;

            Summary += "Options" + Line;
            foreach (var o in saveModule.dataAS.DataDishonored.DishonoredOptions)
            {
                Summary += Space + o.Option + ": " + o.Enable + Line;
            }
            Summary += Line;
            #endregion
            #region ASLSummary

            Summary += "=======================================================" + Line;
            Summary += "ASL:" + Line;
            Summary += "=======================================================" + Line;

            Summary += XmlToString(saveModule.GetAslData(),2) + Line;

            #endregion
            Summary += "=======================================================" + Line;
            Summary += "=======================================================" + Line;

            TextBoxSummary.Text = Summary;
        }

        public string XmlToString(XmlNode node, int indentation)
        {
            using (var sw = new StringWriter())
            {
                using (var xw = new XmlTextWriter(sw))
                {
                    xw.Formatting = Formatting.Indented;
                    xw.Indentation = indentation;
                    node.WriteContentTo(xw);
                }
                return sw.ToString();
            }
        }

        private void btnLoadProfile_Click(object sender, EventArgs e)
        {
            if (comboBoxProfiles.Items.Count > 0)
            {
                var selectItem = savePath + "\\" + comboBoxProfiles.SelectedItem.ToString();
                if (!File.Exists(selectItem))
                    MessageBox.Show("File Corrupt or not Exist", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DialogResult result = MessageBox.Show("Do you want save Current Profile Before?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        btnSaveProfile_Click(null, null);
                    if (result != DialogResult.Cancel)
                    {
                        string mainSave = Path.GetFullPath("HitCounterManagerSaveAutoSplitter.xml");
                        File.Delete(mainSave);
                        File.Copy(selectItem, mainSave);
                        saveModule.ReLoadAutoSplitterSettings();
                    }
                }
            }else
                MessageBox.Show("There are no Profiles to Load", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshForm();
        }

        private void btnRemoveProfile_Click(object sender, EventArgs e)
        {
            if (comboBoxProfiles.Items.Count > 0)
            {
                var selectItem = savePath + "\\" + comboBoxProfiles.SelectedItem.ToString();
                DialogResult result = MessageBox.Show("Do you want Remove This Profile?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes && File.Exists(selectItem))
                    File.Delete(selectItem);
                RefreshForm();
            }
            else
                MessageBox.Show("There are no Profiles to Remove", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            if (textBoxCurrrentProfile.Text == String.Empty)
            {
                MessageBox.Show("Dont Set String Empty on Profile Name", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBoxAuthor.Text == string.Empty)
            {
                MessageBox.Show("Dont Set String Empty on Profile Author", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBoxCurrrentProfile.Text != saveModule.GetProfileName()) btnSetProfile_Click(null, null);
            if (textBoxAuthor.Text != saveModule.GetAuthor()) btnSetAuthor_Click(null, null);
            saveModule.ResetFlags();
            saveModule.SaveAutoSplitterSettings();
            RefreshForm();
            string mainSave = Path.GetFullPath("HitCounterManagerSaveAutoSplitter.xml");
            string destSave = savePath + "\\" + saveModule.GetProfileName() + ".xml";
            if (File.Exists(destSave))
            {
                DialogResult result = MessageBox.Show("File Exist, Do You Want Replace?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    File.Delete(destSave);
                    File.Copy(mainSave, destSave);
                }
            }
            else
            {
                File.Copy(mainSave, destSave);
            }

            RefreshForm();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            Stream stream = new FileStream(CustomSavePath, FileMode.OpenOrCreate);
            var Browser = new FolderBrowserDialog();
            DialogResult result = Browser.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(Browser.SelectedPath))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.Write(Browser.SelectedPath);
                }
                    savePath = Browser.SelectedPath;
                    textBoxSavePath.Text = savePath;
            }
            stream.Close();
            if (savePath == defaultPath) File.Delete(CustomSavePath);
            RefreshForm();
        }

        private void btnSetProfile_Click(object sender, EventArgs e)
        {
            if (textBoxCurrrentProfile.Text != String.Empty)
            {
                saveModule.SetProfileName(textBoxCurrrentProfile.Text);
                btnSetProfile.Hide();
                RefreshSummary();
            }
            else
                MessageBox.Show("Dont Set String Empty on Profile Name", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSetAuthor_Click(object sender, EventArgs e)
        {
            if (textBoxAuthor.Text != String.Empty)
            {
                saveModule.SetAuthor(textBoxAuthor.Text);
                btnSetAuthor.Hide();
                RefreshSummary();
            }
            else
                MessageBox.Show("Dont Set String Empty on Profile Author", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void textBoxCurrrentProfile_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCurrrentProfile.Text != saveModule.GetProfileName())
                btnSetProfile.Show();
            else
                btnSetProfile.Hide();
        }

        private void textBoxAuthor_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAuthor.Text != saveModule.GetAuthor())
                btnSetAuthor.Show();
            else
                btnSetAuthor.Hide();
        }
    }
}
