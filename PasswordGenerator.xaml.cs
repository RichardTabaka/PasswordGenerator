using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PasswordPal
{
    /// <summary>
    /// Interaction logic for PasswordGenerator.xaml
    /// </summary>
    public partial class PasswordGenerator : Window
    {
        public PasswordGenerator()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Basic characters to be used in PW creation
            string AZString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string azString = "abcdefghijklmnopqrstuvwxyz";
            string numString = "0123456789";
            string specialString = "!@#$%&*?";
            List<string> reqs = new List<string>();
            List<bool> reqsHit = new List<bool>();

            bool allUnchecked = (!(bool)AZ.IsChecked && !(bool)az.IsChecked && !(bool)num.IsChecked && !(bool)special.IsChecked); // If nothing is selected, must default to something

            // Check Requirements
            if((bool)AZ.IsChecked)
            {
                reqs.Add(AZString);
                reqsHit.Add(false);
            }
            if((bool)az.IsChecked)
            {
                reqs.Add(azString);
                reqsHit.Add(false);
            }
            if((bool)num.IsChecked)
            {
                reqs.Add(numString);
                reqsHit.Add(false);
            }
            if((bool)special.IsChecked)
            {
                reqs.Add(specialString);
                reqsHit.Add(false);
            }
            if(allUnchecked)
            {
                // Defaults to just lowercase letters
                reqs.Add(azString);
                reqsHit.Add(false);
            }

            string generatedPassword = PassGen(reqsHit, reqs);
            Generated.Text = generatedPassword;
            //Generated.Background.Opacity = 1;
        }
        
        public string PassGen(List<bool> reqsHit, List<string> reqs)
        {
            // Build a Password containing AT LEAST one of each required char type
            Random rnd = new Random();
            string pass = "";

            for(int i = 0; i < 12; i++)
            {
                // Use random to pick next character:
                int reqType = rnd.Next(reqs.Count); // Pick a random requirement to pull from
                int charNum = rnd.Next(reqs[reqType].Length); // Pick random char from string

                // Mark related reqsHit value as true
                reqsHit[reqType] = true;

                pass = pass + reqs[reqType][charNum];
            }

            // Verify all requirements are met
            bool allChecked = true;
            foreach(bool b in reqsHit)
            {
                if (!b)
                {
                    allChecked = false;
                }
            }
            if(allChecked)
            {
                return pass;
            } else
            {
                // If, somehow, they weren't simply generate a new password
                return PassGen(reqsHit, reqs);
            }
        }
    }
}
