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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DnDHelper;

namespace DnDHelper.WPF
{
    /// <summary>
    /// Interaction logic for CharacterDetails.xaml
    /// </summary>
    public partial class CharacterDetails : UserControl
    {
        private Character _character;
        private Helper _helper;

        public CharacterDetails()
        {
            InitializeComponent();
        }

        public void Init(Helper help, Character chr)
        {
            try
            {
                _character = chr;
                _helper = help;
                listView1.ItemsSource = _character.Attacks;
                listView2.ItemsSource = _character.Effects;
                listView3.ItemsSource = _character.KnownSpells;
                listView4.ItemsSource = _character.AvailableCastings;
                listView6.ItemsSource = _character.Spells.Where(f => f.IsCasted == false).OrderByDescending(k => k.Definition.Level);
                listView7.ItemsSource = _character.Skills;
                comboBox2.ItemsSource = _character.AvailableCastings;
                comboBox1.ItemsSource = SpellEditWindow.Types;
                comboBox4.ItemsSource = Rules.SkillsDefinition.Values;
                comboBox4.DisplayMemberPath = "Name";
                List<Atut> atutes = Rules.AtutesDictionary.List;
                atutes.Sort(new AtutComparer());
                comboBox5.ItemsSource = atutes;
                comboBox5.DisplayMemberPath = "Name";
                comboBox6.ItemsSource = _character.Staffes;
                comboBox6.DisplayMemberPath = "ChargesDetails";
                comboBox6.SelectedIndex = 0;
                comboBox7.ItemsSource = _character.Arrows;
                comboBox7.DisplayMemberPath = "ChargesDetails";
                comboBox7.SelectedIndex = 0;
                comboBox8.ItemsSource = _character.Potions;
                comboBox8.DisplayMemberPath = "ChargesDetails";
                comboBox8.SelectedIndex = 0;
                comboBox9.ItemsSource = _character.Others;
                comboBox9.DisplayMemberPath = "ChargesDetails";
                comboBox9.SelectedIndex = 0;
                listView9.ItemsSource = _character.Atutes;
                ContentGrid.DataContext = _character;
                textBox18.Text = _helper.CurrentTime.Date.AddDays(-7).ToString();
                if (_character.ImagePath != null)
                {
                    image1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + _character.ImagePath));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Inicjalizacja się nie powiodła:" + exc.ToString());
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            listView1.Items.Refresh();
        }

        #region Okno - Ataki

        private void DodajAtak_Click(object sender, RoutedEventArgs e)
        {
            Attack attack = new Attack();
            _character.Attacks.Add(attack);
            AttackEditWindow wnd = new AttackEditWindow(attack);
            if (wnd.ShowDialog() == true)
            {
                Refresh();
            }
        }

        private void EdytujAtak_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem == null)
            {
                MessageBox.Show("Wybierz jakiś atak żeby go edytować");
                return;
            }
            Attack attack = (Attack)listView1.SelectedItem;
            AttackEditWindow wnd = new AttackEditWindow(attack);
            if (wnd.ShowDialog() == true)
            {
                Refresh();
            }
        }

        private void UsunAtak_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem == null)
            {
                MessageBox.Show("Wybierz jakiś atak żeby go usunąć");
                return;
            }
            _character.Attacks.Remove((Attack)listView1.SelectedItem);
            Refresh();
        }

        #endregion

        #region Okno - efekty
        private void DodajEfekt_Click(object sender, RoutedEventArgs e)
        {
            Effect newEfekt = new Effect();
            EffectEditWindow efWnd = new EffectEditWindow(newEfekt, _helper);
            if (efWnd.ShowDialog() == true)
            {
                _character.Effects.Add(newEfekt);
                listView2.Items.Refresh();
            }
        }

        private void EdytujEfekt_Click(object sender, RoutedEventArgs e)
        {
            if (listView2.SelectedItem == null)
            {
                MessageBox.Show("Coś byś se wybrał najpierw");
                return;
            }
            Effect efekt = (Effect)listView2.SelectedItem;
            EffectEditWindow efWnd = new EffectEditWindow(efekt, _helper);
            if (efWnd.ShowDialog() == true)
            {
                listView2.Items.Refresh();
            }
        }

        private void UsunEfekt_Click(object sender, RoutedEventArgs e)
        {
            if (listView2.SelectedItem == null)
            {
                MessageBox.Show("Coś trza wybrać, nie ma rady");
                return;
            }
            Effect efekt = (Effect)listView2.SelectedItem;
            _character.Effects.Remove(efekt);
            listView2.Items.Refresh();
        }

        #endregion

        #region Okno - czary

        private void DodajCzar_Click(object sender, RoutedEventArgs e)
        {
            SpellEditWindow spellWnd = new SpellEditWindow(_helper, _character);
            if (spellWnd.ShowDialog() == true)
            {
                listView3.Items.Refresh();
            }
        }

        private void listView3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView3.SelectedItem != null)
            {
                textBox5.Text = listView3.SelectedItem.ToString();
            }
        }

        private void DodajCzarPoziom_Click(object sender, RoutedEventArgs e)
        {
            SpellCasting sCast = new SpellCasting();
            if (_character.AvailableCastings == null)
            {
                _character.AvailableCastings = new List<SpellCasting>();
            }
            _character.AvailableCastings.Add(sCast);
            listView4.Items.Refresh();
        }

        private void listView4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView4.SelectedItem != null)
            {
                CzaryPoziomContent.DataContext = listView4.SelectedItem;
            }
        }

        private void UsunCzarPoziom_Click(object sender, RoutedEventArgs e)
        {
            if (listView4.SelectedItem != null)
            {
                _character.AvailableCastings.Remove((SpellCasting)listView4.SelectedItem);
                listView4.Items.Refresh();
            }
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                SpellCasting sCast = (SpellCasting)comboBox2.SelectedItem;
                var spells = _character.KnownSpells.Where(f => f.SpellTypes.Contains(sCast.Type) && f.Level == sCast.Level);
                comboBox3.DataContext = spells;
                comboBox3.ItemsSource = spells;
                comboBox3.DisplayMemberPath = "Name";
                listView5.ItemsSource = _character.Spells.Where(f => f.Definition.SpellTypes.Contains(sCast.Type) && f.Definition.Level == sCast.Level);
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                SpellCasting sCast = (SpellCasting)comboBox2.SelectedItem;
                var spells = _character.Spells.Where(f => f.Definition.SpellTypes.Contains(sCast.Type) && f.Definition.Level == sCast.Level);
                if (spells.Count() == sCast.Count)
                {
                    MessageBox.Show("Osiągnięo limit czarów");
                    return;
                }
                if (comboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Wybierz czar");
                    return;
                }
                Spell spell = new Spell() { Definition = (SpellDefinition)comboBox3.SelectedItem, Name = ((SpellDefinition)comboBox3.SelectedItem).Name };
                _character.Spells.Add(spell);
                listView5.ItemsSource = _character.Spells.Where(f => f.Definition.SpellTypes.Contains(sCast.Type) && f.Definition.Level == sCast.Level);
                listView5.Items.Refresh();
            }
        }

        private void UsunCzarPrzygotowany_Click(object sender, RoutedEventArgs e)
        {
            if (listView5.SelectedItem != null)
            {
                _character.Spells.Remove((Spell)listView5.SelectedItem);
                SpellCasting sCast = (SpellCasting)comboBox2.SelectedItem;
                listView5.ItemsSource = _character.Spells.Where(f => f.Definition.SpellTypes.Contains(sCast.Type) && f.Definition.Level == sCast.Level);
                listView5.Items.Refresh();
            }
        }

        private void CzarRzuc_Click(object sender, RoutedEventArgs e)
        {
            if (listView6.SelectedItem != null)
            {
                ((Spell)listView6.SelectedItem).IsCasted = true;
                listView6.ItemsSource = _character.Spells.Where(f => f.IsCasted == false).OrderByDescending(k => k.Definition.Level);
            }
        }

        private void listView6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView6.SelectedItem != null)
            {
                textBox15.Text = ((Spell)listView6.SelectedItem).Definition.ToString();
            }
        }
        
        #endregion

        #region Okno - ekwipunek

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.RightHand, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox2.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.LeftHand, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox3.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.Torso, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox4.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button24_Click(object sender, RoutedEventArgs e)
        {
            ArrowWnd wnd = new ArrowWnd(BaseTypes.Wand, _character.Staffes, _helper);
            if (wnd.ShowDialog() == true)
            {
                comboBox6.Items.Refresh();
            }
        }

        private void button25_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox6.SelectedItem != null)
            {
                ((Item)comboBox6.SelectedItem).Charges++;
                comboBox6.Items.Refresh();
                var sel = comboBox6.SelectedItem;
                comboBox6.SelectedItem = null;
                comboBox6.SelectedItem = sel;
            }
        }

        private void button26_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox6.SelectedItem != null)
            {
                ((Item)comboBox6.SelectedItem).Charges--;
                comboBox6.Items.Refresh();
                var sel = comboBox6.SelectedItem;
                comboBox6.SelectedItem = null;
                comboBox6.SelectedItem = sel;
            }
        }

        private void button31_Click(object sender, RoutedEventArgs e)
        {
            ArrowWnd wnd = new ArrowWnd(BaseTypes.Potion, _character.Potions, _helper);
            if (wnd.ShowDialog() == true)
            {
                comboBox8.Items.Refresh();
            }
        }

        private void button32_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox8.SelectedItem != null)
            {
                ((Item)comboBox8.SelectedItem).Charges++;
                comboBox8.Items.Refresh();
                var sel = comboBox8.SelectedItem;
                comboBox8.SelectedItem = null;
                comboBox8.SelectedItem = sel;
            }
        }

        private void button33_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox8.SelectedItem != null)
            {
                ((Item)comboBox8.SelectedItem).Charges--;
                comboBox8.Items.Refresh();
                var sel = comboBox8.SelectedItem;
                comboBox8.SelectedItem = null;
                comboBox8.SelectedItem = sel;
            }
        }

        private void button27_Click(object sender, RoutedEventArgs e)
        {
            ArrowWnd wnd = new ArrowWnd(BaseTypes.Arrow, _character.Arrows, _helper);
            if (wnd.ShowDialog() == true)
            {
                comboBox7.Items.Refresh();
            }
        }

        private void button29_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox7.SelectedItem != null)
            {
                ((Item)comboBox7.SelectedItem).Charges--;
                comboBox7.Items.Refresh();
                var sel = comboBox7.SelectedItem;
                comboBox7.SelectedItem = null;
                comboBox7.SelectedItem = sel;
            }
        }

        private void button28_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox7.SelectedItem != null)
            {
                ((Item)comboBox7.SelectedItem).Charges++;
                comboBox7.Items.Refresh();
                var sel = comboBox7.SelectedItem;
                comboBox7.SelectedItem = null;
                comboBox7.SelectedItem = sel;
            }
        }


        private void button18_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.RightRing, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox22.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button19_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.LeftRing, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox23.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button20_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.Neclease, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox24.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button30_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.Belt , _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox28.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.SecondWeapon, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox21.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button21_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.Boots, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox25.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button22_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.Gloves, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox26.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button23_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.Cloak, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox27.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button34_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, ItemLocalization.Helmet, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox29.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button35_Click(object sender, RoutedEventArgs e)
        {
            ArrowWnd wnd = new ArrowWnd(BaseTypes.Other, _character.Others, _helper);
            if (wnd.ShowDialog() == true)
            {
                comboBox9.Items.Refresh();
            }
        }

        private void button36_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox9.SelectedItem != null)
            {
                ((Item)comboBox9.SelectedItem).Charges++;
                comboBox9.Items.Refresh();
                var sel = comboBox9.SelectedItem;
                comboBox9.SelectedItem = null;
                comboBox9.SelectedItem = sel;
            }
        }

        private void button37_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox9.SelectedItem != null)
            {
                ((Item)comboBox9.SelectedItem).Charges--;
                comboBox9.Items.Refresh();
                var sel = comboBox9.SelectedItem;
                comboBox9.SelectedItem = null;
                comboBox9.SelectedItem = sel;
            }
        }

        private void label42_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.Helmet);
        }

        private void label17_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.RightHand);
        }

        private void label18_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.LeftHand);
        }

        private void label19_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.Torso);
        }

        private void label32_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.RightRing);
        }

        private void label33_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.LeftRing);
        }

        private void label34_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.Neclease);
        }

        private void label40_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.Belt);
        }

        private void label20_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.SecondWeapon);
        }

        private void label35_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.Boots);
        }

        private void label36_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.Gloves);
        }

        private void label37_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem(_character.Cloak);
        }

        private void label38_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem((Item)comboBox6.SelectedItem);
        }

        private void label39_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem((Item)comboBox7.SelectedItem);
        }

        private void label41_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem((Item)comboBox8.SelectedItem);
        }

        private void label43_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowItem((Item)comboBox8.SelectedItem);
        }

        private void ShowItem(Item it)
        {
            QuickWeaponWnd wnd = new QuickWeaponWnd(it);
            wnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            wnd.Show();
        }

        #endregion

        #region Okno - umiejętności

        private void AddSkill_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                Skill skill = new Skill() { Name = ((Skill)comboBox4.SelectedItem).Name };
                switch (((Skill)comboBox4.SelectedItem).BonusProperty)
                {
                    case BaseAttribute.Charisma:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.Charisma);
                        break;

                    case BaseAttribute.Constitution:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.Constitution);
                        break;

                    case BaseAttribute.Dexterity:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.Dexterity);
                        break;

                    case BaseAttribute.Inteligence:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.Inteligence);
                        break;

                    case BaseAttribute.Strength:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.Strength);
                        break;

                    case BaseAttribute.Wisdom:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.WillThrow);
                        break;
                }
                _character.Skills.Add(skill);
                listView7.Items.Refresh();
            }
        }

        private void listView7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView7.SelectedItem != null)
            {
                SkillContent.DataContext = listView7.SelectedItem;
                comboBox4.SelectedItem = (Skill)listView7.SelectedItem;
                textBox17.Text = ((Skill)listView7.SelectedItem).Description;
            }
        }

        private void RemoveSkill_Click(object sender, RoutedEventArgs e)
        {
            if (listView7.SelectedItem != null)
            {
                _character.Skills.Remove((Skill)listView7.SelectedItem);
            }
        }

        #endregion

        #region Okno - atuty

        private void comboBox5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox5.SelectedItem != null)
            {
                textBox19.Text = GetAtutFullDescription((Atut)comboBox5.SelectedItem);
                textBox20.Text = "";
            }
        }

        private void button17_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox5.SelectedItem != null)
            {
                Atut toAdd = (Atut)comboBox5.SelectedItem;
                toAdd.AdditionalInfo = textBox20.Text;
                _character.Atutes.Add(toAdd);
                RefreshCharacter();
            }
        }

        private void button16_Click(object sender, RoutedEventArgs e)
        {
            if (listView9.SelectedItem != null)
            {
                _character.Atutes.Remove((Atut)listView9.SelectedItem);
                RefreshCharacter();
            }
        }

        private void listView9_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView9.SelectedItem != null)
            {
                textBox19.Text = GetAtutFullDescription((Atut)listView9.SelectedItem);
                textBox20.Text = ((Atut)listView9.SelectedItem).AdditionalInfo;
            }
        }

        private string GetAtutFullDescription(Atut a)
        {
            return string.Format("{0}{1}{1}Wymagania:{2}", a.Description, System.Environment.NewLine, a.Requirements);
        }

        #endregion

        /// <summary>
        /// Odpoczynek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            _character.CurrentStats.HP = _character.OriginalStats.HP;
            foreach (Spell spell in _character.Spells)
            {
                spell.IsCasted = false;
            }
            RefreshCharacter();
        }

        /// <summary>
        /// Przywrócenie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            _character.CurrentStats.AC = _character.OriginalStats.AC;
            _character.CurrentStats.AttackSkill = _character.OriginalStats.AttackSkill;
            _character.CurrentStats.Constitution = _character.OriginalStats.Constitution;
            _character.CurrentStats.Dexterity = _character.OriginalStats.Dexterity;
            _character.CurrentStats.Inteligence = _character.OriginalStats.Inteligence;
            _character.CurrentStats.ReflexThrow = _character.OriginalStats.ReflexThrow;
            _character.CurrentStats.Strength = _character.OriginalStats.Strength;
            _character.CurrentStats.StrongThrow = _character.OriginalStats.StrongThrow;
            _character.CurrentStats.WillThrow = _character.OriginalStats.WillThrow;
            _character.CurrentStats.Wisdom = _character.OriginalStats.Wisdom;
            RefreshCharacter();
        }

       
        /// <summary>
        /// Oblicz - klasa/rasa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            if (Rules.RaceTable.Any(r => r.Name == tbRace.Text))
            {
                int level;
                if (!int.TryParse(tbLevel.Text, out level))
                {
                    MessageBox.Show("Nieprawidłowy poziom");
                    return;
                }

                _character.OriginalStats.AttackSkill = Rules.ClassTable[tbClass.Text].AttackPerLevel[level];
                _character.OriginalStats.StrongThrow = Rules.ClassTable[tbClass.Text].ThrowPerLevel[level].EnduranceThrow;
                _character.OriginalStats.ReflexThrow = Rules.ClassTable[tbClass.Text].ThrowPerLevel[level].ReflexThrow;
                _character.OriginalStats.WillThrow = Rules.ClassTable[tbClass.Text].ThrowPerLevel[level].WillThrow;
                _character.OriginalStats.HP = (int)((Rules.ClassTable[tbClass.Text].PW / 2) * level) + (int)(Rules.ClassTable[tbClass.Text].PW / 2);

                var raceDef = Rules.RaceTable.Single(r => r.Name == tbRace.Text);

                foreach (var key in raceDef.Bonuses)
                    {
                        switch (key.Attribute)
                        {
                            case BaseAttribute.Strength:
                                _character.OriginalStats.Strength += key.Value;
                                break;

                            case BaseAttribute.Dexterity:
                                _character.OriginalStats.Dexterity += key.Value;
                                break;

                            case BaseAttribute.Constitution:
                                _character.OriginalStats.Constitution += key.Value;
                                break;

                            case BaseAttribute.Inteligence:
                                _character.OriginalStats.Inteligence += key.Value;
                                break;

                            case BaseAttribute.Wisdom:
                                _character.OriginalStats.Wisdom += key.Value;
                                break;
                        }
                    }
                    _character.OriginalStats.ReflexThrow += Rules.GetStandardBonus(_character.OriginalStats.Dexterity);
                    _character.OriginalStats.StrongThrow += Rules.GetStandardBonus(_character.OriginalStats.Constitution);
                    _character.OriginalStats.HP += level * Rules.GetStandardBonus(_character.OriginalStats.Constitution);
                    _character.OriginalStats.WillThrow += Rules.GetStandardBonus(_character.OriginalStats.Wisdom);

                Class cl = Rules.ClassTable[tbClass.Text];
                _character.AvailableCastings = new List<SpellCasting>();
                if (cl.SpellsPerLevel != null && cl.SpellsPerLevel.Count > 0)
                {
                    foreach (SpellCasting sc in cl.SpellsPerLevel[level])
                    {
                        _character.AvailableCastings.Add(sc);
                    }
                }
                _character.BaseInitiative = Rules.GetStandardBonus(_character.OriginalStats.Dexterity);
                button7_Click(sender, e);
                button8_Click(sender, e);
                RefreshCharacter();
                return;
            }
            MessageBox.Show("Nie udało się obliczyć - nie znaleziono rasy albo klasy");
        }

        /// <summary>
        /// Oblicz - ataki i KP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int baseKP = 10;
                int panalties = 0;
                int maxBonus = int.MaxValue;
                if (_character.LeftHand != null && _character.LeftHand.Type == "Zbroja")
                {
                    baseKP += _character.LeftHand.AC;
                    panalties += _character.LeftHand.Panalty;
                    if (_character.LeftHand.MaxDexterityBonus < maxBonus)
                    {
                        maxBonus = _character.LeftHand.MaxDexterityBonus;
                    }
                }
                if (_character.Torso != null)
                {
                    baseKP += _character.Torso.AC;
                    panalties += _character.Torso.Panalty;
                    if (_character.Torso.MaxDexterityBonus < maxBonus)
                    {
                        maxBonus = _character.Torso.MaxDexterityBonus;
                    }
                }
                int dexterityBonus = Rules.GetStandardBonus(_character.OriginalStats.Dexterity);
                if (dexterityBonus > maxBonus)
                {
                    dexterityBonus = maxBonus;
                }
                baseKP += dexterityBonus;
                _character.OriginalStats.AC = baseKP;
                _character.CurrentStats.AC = baseKP;

                string[] attacks = null;
                if (_character.OriginalStats.AttackSkill.Contains("\\"))
                {
                    attacks = _character.OriginalStats.AttackSkill.Split('\\');
                }
                else
                {
                    attacks = new string[1] { _character.OriginalStats.AttackSkill };
                }
                foreach (string attackSkill in attacks)
                {
                    Attack atak = new Attack();
                    atak.ToHit = int.Parse(attackSkill);
                    atak.ToHit += Rules.GetStandardBonus(_character.OriginalStats.Strength);
                    if (_character.RightHand != null)
                    {
                        if (_character.RightHand.Damage.Contains("+"))
                        {
                            string[] dmgs = _character.RightHand.Damage.Split('+');
                            int bonus = int.Parse(dmgs[1]);
                            bonus += Rules.GetStandardBonus(_character.OriginalStats.Strength);
                            atak.Damage = dmgs[0] + "+" + bonus.ToString();
                        }
                        else
                        {
                            atak.Damage = _character.RightHand.Damage + "+" + Rules.GetStandardBonus(_character.OriginalStats.Strength).ToString();
                        }
                    }
                    else
                    {
                        atak.Damage = "K4" + "+" + Rules.GetStandardBonus(_character.OriginalStats.Strength).ToString();
                    }
                    _character.Attacks.Add(atak);
                }
                MessageBox.Show("Policzono");
                RefreshCharacter();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Nie udało się policzyć: " + exc.ToString());
            }
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            AddGold wnd = new AddGold(_character, true);
            if (wnd.ShowDialog() == true)
            {
                RefreshCharacter();
            }
        }

        private void RefreshCharacter()
        {
            Character tmp = _character;
            Init(_helper, new Character());
            Init(_helper, tmp);
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            AddGold wnd = new AddGold(_character, false);
            if (wnd.ShowDialog() == true)
            {
                RefreshCharacter();
            }
        }

        private void button13_Click(object sender, RoutedEventArgs e)
        {
            ImageWindow wnd = new ImageWindow(_character);
            wnd.ShowDialog();
        }

        private void button14_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                IEnumerable<KilledCreature> list = _character.Kills.Where(s => s.Date >= DateTime.Parse(textBox18.Text));
                listView8.ItemsSource = list;
                listView8.Items.Refresh();
            }
            catch
            {
            }
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            PopUp.HealWindow healWnd = new PopUp.HealWindow(_character);
            healWnd.ShowDialog();
            RefreshCharacter();
        }

        



        

        
 
    }
}
