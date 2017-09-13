using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoGenericRepo.Data;
using TodoGenericRepo.Views;
using Xamarin.Forms;

namespace TodoGenericRepo
{

    public partial class  App : Application
    {
        static TodoItemDatabase database;

        public App()
        {
            Resources = new ResourceDictionary();
            Resources.Add("primaryGreen", Color.FromHex("91CA47"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

            var nav = new NavigationPage(new TodoListPage());
            nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
            nav.BarTextColor = Color.White;

            MainPage = nav;
        }

        public static TodoItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TodoItemDatabase(
                        DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return database;
            }
        }
        
         static SQLiteAsyncConnection dbAsyncConnection;
        public static SQLiteAsyncConnection DbConnectionAsync
        {
            get
            {
                if (dbAsyncConnection == null)
                {
                    dbAsyncConnection = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return dbAsyncConnection;
            }
        }
        static SQLiteConnection dbConnection;
        public static SQLiteConnection DbConnection
        {
            get
            {
                if (dbConnection == null)
                {
                    dbConnection = new SQLiteConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return dbConnection;
            }
        }

        public int ResumeAtTodoId { get; set; }
        TotoRepository totoRepo => new TotoRepository();
        protected override void OnStart()
        {
            Debug.WriteLine("OnStart");

            
            
            // always re-set when the app starts
            // users expect this (usually)
            //			Properties ["ResumeAtTodoId"] = "";
            if (Properties.ContainsKey("ResumeAtTodoId"))
            {
                var rati = Properties["ResumeAtTodoId"].ToString();
                Debug.WriteLine("   rati=" + rati);
                if (!String.IsNullOrEmpty(rati))
                {
                    Debug.WriteLine("   rati=" + rati);
                    ResumeAtTodoId = int.Parse(rati);

                    if (ResumeAtTodoId >= 0)
                    {
                        var todoPage = new TodoDetailPage();
                        new Task(async () =>
                        {
                            todoPage.BindingContext = await totoRepo.GetAsync(ResumeAtTodoId);
                            await MainPage.Navigation.PushAsync(todoPage, false); // no animation
                        });
                    }
                }
            }
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("OnSleep saving ResumeAtTodoId = " + ResumeAtTodoId);
            // the app should keep updating this value, to
            // keep the "state" in case of a sleep/resume
            Properties["ResumeAtTodoId"] = ResumeAtTodoId;
        }

        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
            if (Properties.ContainsKey("ResumeAtTodoId"))
            {
                var rati = Properties["ResumeAtTodoId"].ToString();
                Debug.WriteLine("   rati=" + rati);
                if (!String.IsNullOrEmpty(rati))
                {
                    Debug.WriteLine("   rati=" + rati);
                    ResumeAtTodoId = int.Parse(rati);

                    if (ResumeAtTodoId >= 0)
                    {
                        var todoPage = new TodoDetailPage();
                        new Task(async () =>
                        {
                            todoPage.BindingContext = await totoRepo.GetAsync(ResumeAtTodoId);
                            await MainPage.Navigation.PushAsync(todoPage, false); // no animation
                        });
                    }
                }
            }
        }
    }
}

