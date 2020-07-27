using Microsoft.Win32;
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
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WhatsAppSender.Model;
using System.Web;
using System.Net;
using System.Threading;

namespace WhatsAppSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<Contact> contacts = new List<Contact>();
        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {

            String[] contactFileData=null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (ofd.ShowDialog() == true)
            {
                contactFileData = File.ReadAllLines(ofd.FileName);

                try
                {
                    foreach (var item in contactFileData)
                    {
                        contacts.Add(new Contact(long.Parse(item)));
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void MessageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (ofd.ShowDialog() == true)
            {
                messageBox.Text = File.ReadAllText(ofd.FileName);
            }
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(messageBox.Text.Trim())) { 
            
                ChromeDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://web.whatsapp.com/");
                String message = WebUtility.HtmlEncode(messageBox.Text);
               
                
                Thread.Sleep(10000);
                foreach (Contact i in contacts)
                {
                    try{
		                driver.ExecuteScript("window.open('');");
                        driver.SwitchTo().Window(driver.WindowHandles[1]);
                        driver.Navigate().GoToUrl("https://web.whatsapp.com/send?phone="+i.ContactNumber+"&text="+message+"&source&data&app_absent");
                  
                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
                     
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("_1U1xa"))).Click();
                       
                        driver.Close();
                        driver.SwitchTo().Window(driver.WindowHandles[0]);
                       
                    }
                    catch(Exception){}

               }
                
                
                driver.Quit();
                MessageBox.Show("Done Sending All Messages");
                messageBox.Clear();
                contacts.Clear();
            }
        }
    }
}
