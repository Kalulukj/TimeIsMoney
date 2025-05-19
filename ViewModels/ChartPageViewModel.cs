using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TimeIsMoney.Models;

namespace TimeIsMoney.ViewModels
{
    public partial class ChartPageViewModel : BaseNoteViewModel
    {
        public ObservableCollection<Expense> expenseList
        {
            get;
        }
        public ObservableCollection<Expense> groupList
        {
            get; set;
        }
        public ObservableCollection<Expense> groupList2
        {
            get; set;
        }

        readonly Color[] palette;
        public Color[] Palette => palette;
        public ChartPageViewModel()
        {
            expenseList = new ObservableCollection<Expense>();
            groupList = new ObservableCollection<Expense>();
            groupList2 = new ObservableCollection<Expense>();
            palette = PaletteLoader.LoadPalette("#42A5F5", "#EC407A", "#26C6DA", "#AB47BC", "#EF5350",
                "#D59E17", "#FFA726", "#66BB6A", "#8D6E63", "#78909C", "#26A69A", "#5C6BC0", "#7E57C2",
                "#FF7043", "#00C853", "#8BC34A", "#FF8A80", "#FF80AB");
        }

        [RelayCommand]
        private async Task LoadExpense()
        {
            IsBusy = true;
            try
            {
                expenseList.Clear();
                groupList.Clear();
                var newExpenseList = await App.NoteService.GetExpenseAsync();
                foreach (var item in newExpenseList)
                {
                    expenseList.Add(item);
                }
                if (expenseList.Count == 0)
                {
                    DateTime exampleDate = DateTime.Now;
                    expenseList.Add(new Expense()
                    {
                        name = "Название вашей категории",
                        text = "Некоторая ваша трата",
                        expenseId = -1,
                        date = exampleDate.Date.ToString(),
                        time = exampleDate.TimeOfDay.ToString(),
                        eCategoryId = 0,
                        cost = 50
                    });
                }
                var dict = (expenseList.GroupBy(x => x.name, x => x.cost).Select(x => new Expense { name = x.Key, cost = x.Sum() }));
                foreach (var item in dict)
                {
                    groupList.Add(item);
                }

                dict = (expenseList.GroupBy(x => x.name, x => x.cost).Select(x => new Expense { name = x.Key, cost = x.Count() }));
                foreach (var item in dict)
                {
                    groupList2.Add(item);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
    static class PaletteLoader
    {
        public static Color[] LoadPalette(params string[] values)
        {
            Color[] colors = new Color[values.Length];
            for (int i = 0; i < values.Length; i++)
                colors[i] = Color.FromArgb(values[i]);
            return colors;
        }
    }
}
