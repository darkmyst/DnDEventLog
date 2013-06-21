using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace DnDEventLog
{
    /// <summary>
    /// Interaction logic for EditActor.xaml
    /// </summary>
    public partial class EditActor : Window
    {
        public EditActor(Actor actor)
        {
            InitializeComponent();
            this.DataContext = actor;
            this.Loaded += EditActor_Loaded;
        }

        private void EditActor_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEffects();
        }

        private void LoadEffects()
        {
            EffectsLB.Items.Clear();
            EffectsLB.SelectedItems.Clear();

            Actor actor = this.DataContext as Actor;
            XDocument doc = XDocument.Load("EffectCodes.xml");
            foreach (var effectCodeNode in doc.Root.Elements("EffectCode"))
            {
                string name;
                string code;
                Color bgColor;
                Color fgColor;

                try
                {
                    name = effectCodeNode.Attribute("Name").Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Effect with invalid name found");
                    continue;
                }
                try
                {
                    code = effectCodeNode.Attribute("Code").Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Effect with invalid code found");
                    continue;
                }
                try
                {
                    fgColor = (Color)ColorConverter.ConvertFromString(effectCodeNode.Attribute("ForegroundColor").Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(@"Invalid foreground color ""{0}"" for effect ""{1}""",
                        effectCodeNode.Attribute("ForegroundColor").Value,
                        name));
                    continue;
                }
                try
                {
                    bgColor = (Color)ColorConverter.ConvertFromString(effectCodeNode.Attribute("BackgroundColor").Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(@"Invalid background color ""{0}"" for effect ""{1}""",  
                        effectCodeNode.Attribute("BackgroundColor").Value,
                        name));
                    continue;
                }



                
                Brush bgBrush = new SolidColorBrush(bgColor);
                Brush fgBrush = new SolidColorBrush(fgColor);

                bgBrush.Freeze();
                fgBrush.Freeze();

                var effect = new ActorEffect()
                { 
                    Name = name, 
                    Code = code,
                    BackgroundColor =  bgBrush,
                    ForegroundColor = fgBrush
                };
                EffectsCB.Items.Add(effect);
            }

            foreach (var effect in actor.Effects)
                EffectsLB.Items.Add(effect);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = null;
            this.Close();
        }

        private void DeleteSelectedEffects_Click(object sender, RoutedEventArgs e)
        {
            var selectedEffects = EffectsLB.SelectedItems.OfType<ActorEffect>().ToList();
            var actor = this.DataContext as Actor;
            if (selectedEffects != null && selectedEffects.Count() > 0 && actor != null)
            {
                foreach (var effect in selectedEffects)
                {
                    actor.Effects.Remove(effect);
                    EffectsLB.Items.Remove(effect);
                }
            }
        }

        private void AddEffect_Click(object sender, RoutedEventArgs e)
        {
            var selectedEffect = EffectsCB.SelectedItem as ActorEffect;
            var actor = this.DataContext as Actor;
            if (selectedEffect != null && actor != null)
            {
                var newEffect = new ActorEffect()
                {
                    Code = selectedEffect.Code,
                    Name = selectedEffect.Name,
                    BackgroundColor = selectedEffect.BackgroundColor,
                    ForegroundColor = selectedEffect.ForegroundColor,
                };
                actor.Effects.Add(newEffect);
                EffectsLB.Items.Add(newEffect);
                EffectsCB.SelectedItem = null;
            }
        }

    }
}
