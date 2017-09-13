using System;
using System.Diagnostics;
using TodoGenericRepo.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoGenericRepo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListPage : ContentPage
    {
        public TodoListPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var totoRepo = new TotoRepository();

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtTodoId = -1;
            try
            {
                listView.ItemsSource = await totoRepo.GetAsync();
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TodoDetailPage
            {
                BindingContext = new TodoItem()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
            Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);

            await Navigation.PushAsync(new TodoDetailPage
            {
                BindingContext = e.SelectedItem as TodoItem
            });
        }
    }
}
