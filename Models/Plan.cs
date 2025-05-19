using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TimeIsMoney.Models;
public partial class Plan
{
    [PrimaryKey, AutoIncrement]
    public int planId
    {
        get; set;
    }
    public string name
    {
        get; set;
    }

    public string text
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

    public string DateDifferenceFromToday
    {
        get
        {
            //I think for this version no comments needed names are self explanatory
            DateTime dateTime = DateTime.Parse(date);
            TimeSpan difference = DateTime.Today - dateTime.Date;
            var diffMonth = (DateTime.Now.Month - dateTime.Month);
            var diffYear = (DateTime.Now.Year - dateTime.Year);
            if (difference.TotalDays < 0)
            {
                var nextDates = Math.Abs(difference.TotalDays);
                if (nextDates == 1)
                {
                    return "B       Завтра";
                }
                else if (nextDates == 2)
                {
                    return "C       Послезавтра";
                }
                else if (nextDates <=6)
                {
                    return "D       На этой неделе";
                }
                else if (diffMonth == 0)
                {
                    return "E       В этом месяце";
                }
                else if (diffMonth < 0)
                {
                    return "F       В след. месяце";
                }
                else
                {
                    return "G       " + dateTime.ToString("dd MMMM yyyy");
                }
            }

            else if (difference.TotalDays == 0)
            {
                return "A       Сегодня";
            }
            else if (difference.TotalDays == 1)
            {
                return "H       Вчера";
            }
            else if (difference.TotalDays == 2)
            {
                return "I       Два дня назад";
            }
            else if (difference.TotalDays <= 7)
            {
                return "J       На этой неделе (прошло)";
            }
            else if (diffMonth == 0)
            {
                return "K       В этом месяце (прошло)";
            }
            else if (diffYear == 0)
            {
                string targetDate = dateTime.ToString("MMMM");
                return "L       " + targetDate[0].ToString().ToUpper() + targetDate.Substring(1);
            }
            else
            {
                return "M       " + dateTime.ToString("dd MMMM yyyy");
            }

        }
    }
}

