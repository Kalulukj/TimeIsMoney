using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TimeIsMoney.Models
{
    public partial class NoteCategory
    {
        [PrimaryKey, AutoIncrement]
        public int nCategoryId
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
}
