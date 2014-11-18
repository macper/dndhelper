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

namespace DnDHelper.WPF
{
    /// <summary>
    /// Interaction logic for ItemsWindow.xaml
    /// </summary>
    public partial class ItemsWindow : Window
    {
        Helper _helper;
        ItemLocalization _local;
        Character _character;
        bool _modal;
        static BaseTypes[] _baseTypes = Enum.GetValues(typeof(BaseTypes)).Cast<BaseTypes>().OrderBy(o => o.ToString()).ToArray();
        public Item SelectedItem
        {
            get
            {
                if (listView1.SelectedItem != null)
                {
                    return (Item)listView1.SelectedItem;
                }
                return null;
            }
        }

        public ItemsWindow(Helper help, bool modal)
        {
            InitializeComponent();
            _helper = help;
            listView1.ItemsSource = _helper.Items;
            comboBox1.ItemsSource = new string[] { "Broń", "Zbroja" };
            comboBox2.ItemsSource = _baseTypes;
            comboBox3.ItemsSource = _baseTypes;
            _modal = modal;
        }

        public ItemsWindow(Helper help, bool modal, BaseTypes defaultFilter)
        {
            InitializeComponent();
            _helper = help;
            listView1.ItemsSource = _helper.Items;
            comboBox1.ItemsSource = new string[] { "Broń", "Zbroja" };
            comboBox2.ItemsSource = _baseTypes;
            comboBox3.ItemsSource = _baseTypes;
            _modal = modal;
            comboBox3.SelectedItem = defaultFilter;
            Find();
        }

        public ItemsWindow(Helper help, ItemLocalization local, Character ch)
        {
            InitializeComponent();
            _helper = help;
            listView1.ItemsSource = _helper.Items;
            comboBox1.ItemsSource = new string[] { "Broń", "Zbroja" };
            comboBox2.ItemsSource = _baseTypes;
            comboBox3.ItemsSource = _baseTypes;
            _local = local;
            _character = ch;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "Nowy przedmiot";
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Item newIt = _helper.Items.FirstOrDefault(el => el.Name == textBox1.Text);
            if (newIt == null)
            {
                newIt = new Item();
                _helper.Items.Add(newIt);
            }
            
            try
            {
                int tmp = 0;
                if (comboBox1.SelectedIndex == 0)
                {
                    newIt.Damage = textBox3.Text.ToUpper();
                }
                else
                {
                    
                    int.TryParse(textBox4.Text, out tmp);
                    newIt.AC = tmp;
                    int.TryParse(textBox5.Text, out tmp);
                    newIt.MaxDexterityBonus = tmp;
                    int.TryParse(textBox6.Text, out tmp);
                    newIt.Panalty = tmp;
                }
                newIt.BaseType = (BaseTypes)comboBox2.SelectedItem;
                newIt.Name = textBox1.Text;
                int.TryParse(textBox2.Text, out tmp);
                newIt.Cost = tmp;
                newIt.Specials = textBox7.Text;
                int.TryParse(textBox8.Text, out tmp);
                newIt.Charges = tmp;
                listView1.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Błąd walidacji");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Wybierz coś");
                return;
            }
            _helper.Items.Remove(listView1.SelectedItem as Item);
            listView1.Items.Refresh();
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Item it = (Item)listView1.SelectedItem;
                comboBox1.SelectedItem = it.Type;
                comboBox2.SelectedItem = it.BaseType;
                textBox1.Text = it.Name;
                textBox2.Text = it.Cost.ToString();
                textBox3.Text = it.Damage;
                textBox4.Text = it.AC.ToString();
                textBox5.Text = it.MaxDexterityBonus.ToString();
                textBox6.Text = it.Panalty.ToString();
                textBox7.Text = it.Specials;
                textBox8.Text = it.Charges.ToString();
                comboBox1.SelectedValue = it.Type;
            }
            catch
            {
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (_local != ItemLocalization.None)
            {
                Item item = null;
                if (listView1.SelectedItem != null)
                {
                    item = (Item)listView1.SelectedItem;
                }
                
                DialogResult = true;
                switch (_local)
                {
                    case ItemLocalization.RightHand:
                        _character.RightHand = item;
                        break;

                    case ItemLocalization.LeftHand:
                        _character.LeftHand = item;
                        break;

                    case ItemLocalization.Torso:
                        _character.Torso = item;
                        break;

                    case ItemLocalization.Belt:
                        _character.Belt = item;
                        break;

                    case ItemLocalization.Boots:
                        _character.Boots = item;
                        break;

                    case ItemLocalization.Cloak:
                        _character.Cloak = item;
                        break;

                    case ItemLocalization.Gloves:
                        _character.Gloves = item;
                        break;

                    case ItemLocalization.LeftRing:
                        _character.LeftRing = item;
                        break;

                    case ItemLocalization.RightRing:
                        _character.RightRing = item;
                        break;

                    case ItemLocalization.SecondWeapon:
                        _character.SecondWeapon = item;
                        break;

                    case ItemLocalization.Neclease:
                        _character.Neclease = item;
                        break;

                    case ItemLocalization.Helmet:
                        _character.Helmet = item;
                        break;
                }
            }
            if (_modal)
            {
                DialogResult = true;
            }
            Close();
        }

        private void comboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Find();
        }

        private void textBox9_TextChanged(object sender, TextChangedEventArgs e)
        {
            Find();
        }

        private void Find()
        {
            List<Item> retList = new List<Item>();
            _helper.Items.ForEach(f =>
                {
                    if (comboBox3.SelectedItem != null)
                    {
                        if (f.BaseType == (BaseTypes)comboBox3.SelectedItem)
                        {
                            if (textBox9.Text != string.Empty)
                            {
                                if (f.Name.ToLower().StartsWith(textBox9.Text))
                                {
                                    retList.Add(f);
                                }
                            }
                            else
                            {
                                retList.Add(f);
                            }
                        }
                    }
                }
            );
            listView1.ItemsSource = retList.OrderBy(o => o.Name);
        }

    }

    public enum ItemLocalization { None, RightHand, LeftHand, Torso, SecondWeapon, LeftRing, RightRing, Neclease, Cloak, Belt, Boots, Gloves, Helmet };
}
