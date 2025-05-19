using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
