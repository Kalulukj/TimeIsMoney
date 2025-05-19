namespace TimeIsMoney.Models;

public partial class ExpenseCategory
{
    [PrimaryKey, AutoIncrement]
    public int eCategoryId
    {
        get; set;
    }

    public string name
    {
        get; set;
    }

    public string color
    {
        get; set;
    }

    public Color getFormatColor
    {
        get
        {
            if (color != null)
            {
                return Color.FromArgb(color);
            }
            return Color.FromArgb("#757575");
        }
    }
}