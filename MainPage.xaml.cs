using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI;
using Microsoft.Windows.Storage.Pickers;
using System.IO;
using WinRT.Interop;

namespace CSharp_TextEditorWinUI
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            EditorTextBox.Text = string.Empty;
        }

        private async void Abrir_Click(object sender, RoutedEventArgs e)
        {
            //FileOpenPicker openPicker = new FileOpenPicker();

            //new FileOpenPicker(windowId);

            //openPicker.FileTypeFilter.Add(".txt");
            //openPicker.FileTypeFilter.Add("*");

            App app = (App)Application.Current;
            Window? window = app.MainWindow;

            nint hwnd = WindowNative.GetWindowHandle(window);

            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);

            //new FileOpenPicker(windowId);

            FileOpenPicker openPicker = new FileOpenPicker(windowId);
            openPicker.FileTypeFilter.Add(".txt");
            openPicker.FileTypeFilter.Add("*");
            var result = await openPicker.PickSingleFileAsync();

            if (result != null)
            {
                string content = await File.ReadAllTextAsync(result.Path);
                EditorTextBox.Text = content;
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GuardarComo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}