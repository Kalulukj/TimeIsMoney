namespace TimeIsMoney.Models;

public partial class Expense
{
    [PrimaryKey, AutoIncrement]
    public int expenseId
    {
        get; set;
    }

    public string time
    {
        get; set;
    }

    public string date
    {
        get; set;
    }

    public decimal cost
    {
        get; set;
    }

    public string text
    {
        get; set;
    }

    public string color
    {
        get; set;
    } = "#757575";

    public string name
    {
        get; set;
    } = "Без категории";

    public int eCategoryId
    {
        get; set;
    } = 0;

    public string getFormatDate
    {
        get
        {
            if (date != null)
            {
                return DateTime.Parse(date).ToString("M");
            }
            return date;
        }
    }

    public string getFormatTime
    {
        get
        {
            if (time != null)
            {
                return DateTime.Parse(time).ToString("HH:mm");
            }
            return time;
        }
    }

    public Color getFormatColor
    {
        get
        {
            if (color != null)
            {
                return Color.FromArgb(color);
            }
            return Color.FromArgb(color);
        }
    }

    public string DateDifferenceFromToday
    {
        get
        {
            //I think for this version no comments needed names are self explanatory
            DateTime dateTime = DateTime.Parse(date);
            TimeSpan difference = DateTime.Today - dateTime.Date;
            var diffMonth = (DateTime.Now.Month - dateTime.Month);
            var diffYear = (DateTime.Now.Year - dateTime.Year);
            if (difference.TotalDays == 0)
            {
                return "A       Сегодня";
            }
            else if (difference.TotalDays == 1)
            {
                return "B       Вчера";
            }
            else if (difference.TotalDays == 2)
            {
                return "C       Два дня назад";
            }
            else if (difference.TotalDays <= 7)
            {
                return "D       На этой неделе";
            }
            else if (diffMonth == 0)
            {
                return "E       В этом месяце";
            }
            else if (diffYear == 0)
            {
                string targetDate = dateTime.ToString("MMMM");
                return "F       " + targetDate[0].ToString().ToUpper() + targetDate.Substring(1);
            }
            else
            {
                return "J       " + dateTime.ToString("dd MMMM yyyy");
            }
        }
    }
}