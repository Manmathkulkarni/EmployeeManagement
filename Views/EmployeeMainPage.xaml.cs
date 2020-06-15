using EmployeeManagement.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeManagement.Views
{
    /// <summary>
    /// Interaction logic for EmployeeMainPage.xaml
    /// </summary>
    public partial class EmployeeMainPage : Page
    {
        public EmployeeMainPage()
        {
            InitializeComponent();
            this.ShowsNavigationUI = false;
        }

        private async void BtnView_Click(object sender, RoutedEventArgs e)
        {
            ListEmployee.ItemsSource = null;
            int EmpID = 0;
            if (!string.IsNullOrEmpty(TextEmployeeId.Text) && int.TryParse(TextEmployeeId.Text, out EmpID))
            {
                ListEmployee.Visibility = Visibility.Visible;
                var allEmployees = await RestService.GetEmployee(EmpID);
                if(allEmployees != null)
                    ListEmployee.ItemsSource = allEmployees.data;
                else
                    MessageBox.Show("Please try again later!!");
            }
            else
                MessageBox.Show("Please enter valid employee id!!");

        }

        private async void BtnViewAll_Click(object sender, RoutedEventArgs e)
        {
            ListEmployee.ItemsSource = null;
            ListEmployee.Visibility = Visibility.Visible;
            var allEmployees = await RestService.GetAllEmployees();
            if(allEmployees != null)
                ListEmployee.ItemsSource = allEmployees.data;
            else
                MessageBox.Show("Please try again later!!");

        }


        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int EmpID = 0 ;
            if (!string.IsNullOrEmpty(TextEmployeeId.Text) && int.TryParse(TextEmployeeId.Text, out EmpID))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Employee", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var deleteEmployee = await RestService.DeleteEmployee(EmpID);
                    if (deleteEmployee.status == "success")
                        MessageBox.Show("Employee deleted successfully");
                    else if (deleteEmployee.status == "failed")
                        MessageBox.Show("Unable to delete employee!! Try again later. ");
                }
            }
            else
                MessageBox.Show("Please enter valid employee id!!");
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int EmpID = 0;
            if (!string.IsNullOrEmpty(TextUpdateId.Text) && int.TryParse(TextUpdateId.Text, out EmpID))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Update Employee", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ListEmployee.Visibility = Visibility.Visible;
                    var employee = await RestService.GetEmployee(EmpID);
                    if (employee != null)
                    {
                        TextID.Text = employee.data[0].id;
                        TextName.Text = employee.data[0].employee_name;
                        TextAge.Text = employee.data[0].employee_salary;
                        TextSalary.Text = employee.data[0].employee_age;
                    }
                    else
                        MessageBox.Show("Please try again later!!");
                   
                    LblCreateOrUpdate.Content = "Update Employee";
                    TextID.Visibility = Visibility.Visible;
                    TextID.IsEnabled = false;
                    LblTextId.Visibility = Visibility.Visible;
                    FormGrid.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("Please enter employee id to update!");
            }
        }

        private void BtnCreateNew_Click(object sender, RoutedEventArgs e)
        {
            LblCreateOrUpdate.Content = "Create New Employee";
            this.FormGrid.Visibility = Visibility.Visible;
            
            TextID.Visibility = Visibility.Collapsed;
            LblTextId.Visibility = Visibility.Collapsed;
            FormGrid.Visibility = Visibility.Visible;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(TextName.Text) || string.IsNullOrEmpty(TextSalary.Text) || string.IsNullOrEmpty(TextAge.Text))
            {
                MessageBox.Show("Please enter mandatory values!!"); 
            }
            else
            {
                Employee emp = new Employee()
                {
                    age = TextAge.Text,
                    name = TextName.Text,
                    salary = TextSalary.Text
                };

                if (TextID.Visibility == Visibility.Visible)
                {
                    int EmpID = 0;
                    int.TryParse(TextUpdateId.Text, out EmpID);
                    
                    var result = await RestService.UpdateEmployee(EmpID,emp);
                    if (result != null && result.status == "success")
                        MessageBox.Show("Employee created successfully");

                    else if (result == null || result.status == "failed")
                        MessageBox.Show("Unable to create employee!! Try again later. ");
                }
                else
                {
                    var result = await RestService.CreateEmployee(emp);
                    if (result != null && result.status == "success")
                        MessageBox.Show("Employee created successfully");

                    else if (result == null || result.status == "failed")
                        MessageBox.Show("Unable to create employee!! Try again later. ");
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            TextAge.Text = TextName.Text = TextSalary.Text = "";
            FormGrid.Visibility = Visibility.Hidden;
        }
    }
}
