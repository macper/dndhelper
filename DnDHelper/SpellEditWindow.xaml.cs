using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace DnDHelper.WPF
{
    /// <summary>
    /// Interaction logic for SpellEditWindow.xaml
    /// </summary>
    public partial class SpellEditWindow : Window
    {
        private bool _wasChanged;
        Helper _helper;
        Character _character;
        public static readonly SpellType[] Types = new SpellType[] { SpellType.All, SpellType.Bard, SpellType.Cleric, SpellType.Druid, SpellType.Mage, SpellType.Paladin, SpellType.Ranger };
        public static readonly string[] Levels = new string[] { "Wszystkie", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public static readonly SpellShool[] Shools = new SpellShool[] { SpellShool.Abjuration, SpellShool.Conjuration, SpellShool.Divination, SpellShool.Enchantment, SpellShool.Illusion, SpellShool.Necromancy, SpellShool.Transmutation, SpellShool.Summoning };
        public static readonly SpellRange[] SpellRanges = new SpellRange[] { SpellRange.Close, SpellRange.Infinite, SpellRange.Long, SpellRange.Medium, SpellRange.OnlyCaster, SpellRange.Special, SpellRange.Touch };

        public SpellEditWindow(Helper helper, Character character)
        {
            InitializeComponent();
            _character = character;
            InitProperties(helper);
            listView2.ItemsSource = _character.KnownSpells;
        }

        public SpellEditWindow(Helper helper)
        {
            InitializeComponent();
            InitProperties(helper);
            listView2.Visibility = System.Windows.Visibility.Collapsed;
            label8.Visibility = System.Windows.Visibility.Collapsed;
            button4.Visibility = System.Windows.Visibility.Collapsed;
            button5.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void InitProperties(Helper helper)
        {
            _helper = helper;
            comboBox1.ItemsSource = Types;
            comboBox2.ItemsSource = Levels;
            listView1.ItemsSource = Rules.Spells.OrderBy(s => s.Name);
            comboBox3.ItemsSource = Shools;
            comboBox4.ItemsSource = SpellRanges;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox2.Text = "Nowy czar";
            ContentGrid.DataContext = null;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SpellDefinition sd = _helper.GetSpell(textBox2.Text);
                if (sd == null)
                {
                    sd = new SpellDefinition() { Name = textBox2.Text };
                    Rules.Spells.Add(sd);
                }
                sd.Level = int.Parse(textBox3.Text);
                sd.SpellShool = (SpellShool)comboBox3.SelectedValue;
                sd.Description = textBox5.Text;
                sd.SpellTypes.Clear();
                if (checkBox1.IsChecked == true) sd.SpellTypes.Add(SpellType.Mage);
                if (checkBox2.IsChecked == true) sd.SpellTypes.Add(SpellType.Bard);
                if (checkBox3.IsChecked == true) sd.SpellTypes.Add(SpellType.Cleric);
                if (checkBox4.IsChecked == true) sd.SpellTypes.Add(SpellType.Druid);
                if (checkBox5.IsChecked == true) sd.SpellTypes.Add(SpellType.Ranger);
                if (checkBox6.IsChecked == true) sd.SpellTypes.Add(SpellType.Paladin);
                sd.Range = (SpellRange)comboBox4.SelectedValue;
                sd.Duration = textBox1.Text;
                _wasChanged = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Błąd walidacji:" + exc.Message);
            }
            listView1.ItemsSource = Rules.Spells.OrderBy(s => s.Name);
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView1.SelectedItem != null)
            {
                var sd = (SpellDefinition)listView1.SelectedItem;
                SpellDefinition spDef = sd;
                foreach (UIElement child in checkGrid.Children)
                {
                    CheckBox chb = child as CheckBox;
                    if (chb != null)
                    {
                        chb.IsChecked = false;
                    }
                }
                foreach (SpellType type in sd.SpellTypes)
                {
                    if (type == SpellType.Mage) checkBox1.IsChecked = true;
                    if (type == SpellType.Bard) checkBox2.IsChecked = true;
                    if (type == SpellType.Cleric) checkBox3.IsChecked = true;
                    if (type == SpellType.Druid) checkBox4.IsChecked = true;
                    if (type == SpellType.Ranger) checkBox5.IsChecked = true;
                    if (type == SpellType.Paladin) checkBox6.IsChecked = true;
                }

                comboBox3.SelectedValue = sd.SpellShool;
                comboBox4.SelectedValue = sd.Range;
                ContentGrid.DataContext = spDef;
                textBox2.Text = sd.Name;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem != null)
            {
                SpellDefinition spDef = (SpellDefinition)listView1.SelectedItem;
                Rules.Spells.Remove(spDef);
                listView1.Items.Refresh();
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem != null)
            {
                SpellDefinition spDef = (SpellDefinition)listView1.SelectedItem;
                _character.KnownSpells.Add(spDef);
                listView2.Items.Refresh();
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (listView2.SelectedItem != null)
            {
                SpellDefinition spDef = (SpellDefinition)listView2.SelectedItem;
                _character.KnownSpells.Remove(spDef);
                listView2.Items.Refresh();
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (_wasChanged)
            {
                try
                {
                    RulesConfigSection rulesConf = (RulesConfigSection)ConfigurationManager.GetSection("rulesSection");
                    using (FileStream fs = new FileStream(rulesConf.SpellsPath, FileMode.Create))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(List<SpellDefinition>));
                        xml.Serialize(fs, Rules.Spells);
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Wystąpił błąd: " + exc.ToString());
                    return;
                }
            }
            DialogResult = true;
            Close();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            List<SpellDefinition> retList = new List<SpellDefinition>();
            if (comboBox2.SelectedItem == null)
            {
                return;
            }
            Rules.Spells.ForEach(f =>
            {
                bool isOk = true;
                if ((string)comboBox2.SelectedItem != "Wszystkie")
                {
                    if (f.Level != int.Parse((string)comboBox2.SelectedItem))
                    {
                        isOk = false;
                    }
                }
                if ((SpellType)comboBox1.SelectedItem != SpellType.All && !f.SpellTypes.Contains((SpellType)comboBox1.SelectedItem))
                {
                    isOk = false;
                }
                if (textBox4.Text != string.Empty)
                {
                    if (!f.Name.ToLower().StartsWith(textBox4.Text))
                    {
                        isOk = false;
                    }
                }
                if (isOk)
                {
                    retList.Add(f);
                }
            });

            listView1.ItemsSource = retList.OrderBy(o => o.Name);
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            textBox4.Focus();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button7_Click(sender, e);
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            button7_Click(sender, e);
        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            button7_Click(sender, e);
        }

       

    }
}
