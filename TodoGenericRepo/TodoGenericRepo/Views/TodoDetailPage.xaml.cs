using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoGenericRepo.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoGenericRepo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoDetailPage : ContentPage
    {

        TotoRepository totoRepo = null;
        public TodoDetailPage()
        {
            InitializeComponent();
            totoRepo = new TotoRepository();

        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;

            await totoRepo.Insert(todoItem);

            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await totoRepo.Delete(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        void OnSpeakClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            DependencyService.Get<ITextToSpeech>().Speak(todoItem.Name + " " + todoItem.Notes);
        }
    }
}