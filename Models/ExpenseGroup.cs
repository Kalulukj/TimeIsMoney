namespace TimeIsMoney.Models;

public class ExpenseGroup : List<Expense>
{
    public int Name
    {
        get; set;
    }

    public ExpenseGroup(int name, List<Expense> expense) : base(expense)
    {
        Name = name;
    }
}