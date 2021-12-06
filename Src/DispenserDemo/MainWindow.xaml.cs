using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace DispenserDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dispenser dispenser;

        private DispenserJob dispenserJob;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NoOfCanisterTextBox.Text))
                {
                    MessageBox.Show("Please enter Number of canisters", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                int numberOfCanisters = Convert.ToInt32(NoOfCanisterTextBox.Text);
                dispenser = new Dispenser(numberOfCanisters);
                EnableControls();
                MessageBox.Show(dispenser.ToString(), "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void DispenseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(DispenseAmountTextBox.Text))
                {
                    MessageBox.Show("Please enter dispense amount", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (dispenser == null)
                    throw new ArgumentNullException(nameof(dispenser), "Cloudn't proceed dispense without initializing canisters");

                int dispenseAmount = Convert.ToInt32(DispenseAmountTextBox.Text);
                dispenserJob = new DispenserJob(dispenser.Canisters, dispenseAmount);
                dispenser.Dispense(dispenserJob);
                MessageBox.Show("Dispense finished", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException ex)
            {
                dispenser.InitCanisters();
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void RefillButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RefillAmountTextBox.Text))
                {
                    MessageBox.Show("Please enter refill amount", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (dispenser == null)
                    throw new ArgumentNullException("Cloudn't proceed refill without initializing canisters");


                int refillAmount = Convert.ToInt32(RefillAmountTextBox.Text);
                var canisters = dispenser.Canisters;

                for (int i = 0; i < canisters.Count; i++)
                {
                    var item = canisters.ElementAt(i);
                    int currentLevel = item.CurrentLevel + refillAmount;
                    if (currentLevel > item.MaximumLevel)
                    {
                        throw new InvalidOperationException($"Refill colud not proceed. {item.CanisterId} current level exceeds maxmimum level. Adjust refill amount");
                    }                    
                    item.Fill(item, currentLevel);
                    currentLevel = 0;
                }

                MessageBox.Show("Refilled all canisters", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        

        private void EnableControls()
        {
            DispenseAmountTextBox.IsEnabled = true;
            DispenseButton.IsEnabled = true;
            RefillAmountTextBox.IsEnabled = true;
            RefillButton.IsEnabled = true;
        }

        private void DigitOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
