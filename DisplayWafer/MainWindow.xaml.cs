using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenCvSharp;
using OpenCvSharp.Flann;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using static System.Net.Mime.MediaTypeNames;
using static Template.BaseLogRecord;
using Image = System.Windows.Controls.Image;

namespace Template
{
    #region Config Class
    public class SerialNumber
    {
        [JsonProperty("Parameter1_val")]
        public string Parameter1_val { get; set; }
        [JsonProperty("Parameter2_val")]
        public string Parameter2_val { get; set; }
    }

    public class Model
    {
        [JsonProperty("SerialNumbers")]
        public SerialNumber SerialNumbers { get; set; }
    }

    public class RootObject
    {
        [JsonProperty("Models")]
        public List<Model> Models { get; set; }
    }
    #endregion

    public class MainViewModel
    {
        public ObservableCollection<string> Dies { get; set; }

        public MainViewModel()
        {
            Dies = new ObservableCollection<string>();
            // 模拟晶圆上芯片的位置
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    Dies.Add($"{j},{i}");
                }
            }
        }
    }

    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Function
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (System.Windows.MessageBox.Show("請問是否要關閉？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        #region Config
        private SerialNumber SerialNumberClass()
        {
            SerialNumber serialnumber_ = new SerialNumber
            {
                Parameter1_val = Parameter1.Text,
                Parameter2_val = Parameter2.Text
            };
            return serialnumber_;
        }

        private void LoadConfig(int model, int serialnumber, bool encryption = false)
        {
            List<RootObject> Parameter_info = Config.Load(encryption);
            if (Parameter_info != null)
            {
                Parameter1.Text = Parameter_info[model].Models[serialnumber].SerialNumbers.Parameter1_val;
                Parameter2.Text = Parameter_info[model].Models[serialnumber].SerialNumbers.Parameter2_val;
            }
            else
            {
                // 結構:2個Models、Models下在各2個SerialNumbers
                SerialNumber serialnumber_ = SerialNumberClass();
                List<Model> models = new List<Model>
                {
                    new Model { SerialNumbers = serialnumber_ },
                    new Model { SerialNumbers = serialnumber_ }
                };
                List<RootObject> rootObjects = new List<RootObject>
                {
                    new RootObject { Models = models },
                    new RootObject { Models = models }
                };
                Config.InitSave(rootObjects, encryption);
            }
        }
       
        private void SaveConfig(int model, int serialnumber, bool encryption = false)
        {
            Config.Save(model, serialnumber, SerialNumberClass(), encryption);
        }
        #endregion

        // 用于在 Visual Tree 中查找指定类型的子元素的辅助方法
        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// index arrangement︰行、列
        /// </summary>
        /// <param name="index"></param>
        private void ChangeWaferIC(string index)
        {
            ContentPresenter contentPresenter = (ContentPresenter)Wafer.ItemContainerGenerator.ContainerFromItem(index);
            // 检查 ContentPresenter 是否存在
            if (contentPresenter != null)
            {
                // 从 ContentPresenter 中找到 Button
                Button button = FindVisualChild<Button>(contentPresenter);
                // 检查按钮是否存在
                if (button != null)
                {
                    // 更改按钮的背景颜色
                    button.Background = Brushes.Red; // 或者其他你想要的颜色
                }
            }
        }

        private void ChangeAllWaferIC()
        {
            foreach (var item in Wafer.Items)
            {
                // 通过 ItemContainerGenerator 找到每个 ContentPresenter
                ContentPresenter contentPresenter = (ContentPresenter)Wafer.ItemContainerGenerator.ContainerFromItem(item);
                // 检查 ContentPresenter 是否存在
                if (contentPresenter != null)
                {
                    // 从 ContentPresenter 中找到 Button
                    Button button = FindVisualChild<Button>(contentPresenter);
                    // 检查按钮是否存在
                    if (button != null)
                    {
                        // 更改按钮的背景颜色
                        button.Background = Brushes.Red; // 或者其他你想要的颜色
                    }
                }
            }
        }
        #endregion

        #region Parameter and Init
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadConfig(0, 0);
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
        }
        BaseConfig<RootObject> Config = new BaseConfig<RootObject>();
        #region Log
        BaseLogRecord Logger = new BaseLogRecord();
        //Logger.WriteLog("儲存參數!", LogLevel.General, richTextBoxGeneral);
        #endregion
        private MainViewModel viewModel;
        #endregion

        #region Main Screen
        private void Main_Btn_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case nameof(Demo):
                    {
                        ChangeWaferIC("15,15");
                        break;
                    }
            }
        }

        private void DieButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var die = button?.DataContext as string;
            if (die != null)
            {
                // 解析座標，建立檔名，例如 "10_5.png"
                string fileName = die.Replace(",", "_") + ".bmp";
                string imagePath = System.IO.Path.Combine("Wafer Image", fileName); // 相對於執行目錄
                // 檢查圖檔是否存在
                if (!File.Exists(imagePath))
                {
                    System.Windows.MessageBox.Show($"找不到圖檔: {imagePath}");
                    return;
                }
                // 建立影像視窗
                var imageWindow = new System.Windows.Window
                {
                    Title = $"Die {die}",
                    Width = 300,
                    Height = 300,
                    Content = new Image
                    {
                        Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(imagePath))),
                        Stretch = Stretch.Uniform
                    }
                };
                imageWindow.Show(); // 或用 Show() 開非模態視窗
            }
        }
        #endregion





    }
}
