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
        private string? currentFilePath = null;
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

            App app = (App)Application.Current;
            Window? window = app.MainWindow;

            nint hwnd = WindowNative.GetWindowHandle(window);

            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);

            FileOpenPicker openPicker = new FileOpenPicker(windowId);
            openPicker.FileTypeFilter.Add(".txt");
            openPicker.FileTypeFilter.Add("*");
            var result = await openPicker.PickSingleFileAsync();

            if (result != null)
            {
                currentFilePath = result.Path;

                string content = await File.ReadAllTextAsync(result.Path);
                EditorTextBox.Text = content;
            }
        }
        private async void GuardarComo_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;
            Window? window = app.MainWindow;

            nint hwnd = WindowNative.GetWindowHandle(window);

            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);

            FileSavePicker savePicker = new FileSavePicker(windowId);

            savePicker.FileTypeChoices.Add(
            "Archivo de texto",
            new List<string> { ".txt" }
            );

            savePicker.SuggestedFileName = "Documento";

            var result = await savePicker.PickSaveFileAsync();

            if (result != null)
            {
                await File.WriteAllTextAsync(
                    result.Path,
                    EditorTextBox.Text
                );
            }
        }
        private async void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (currentFilePath != null) 
            { 
               await File.WriteAllTextAsync(
                   currentFilePath, 
                   EditorTextBox.Text);
            }
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}