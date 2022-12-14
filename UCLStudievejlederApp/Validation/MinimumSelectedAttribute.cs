using DatabaseAccess.Institution;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using UCLStudievejlederApp.Models.Generic;

namespace UCLStudievejlederApp.Validation
{
    public class MinimumSelectedAttribute : ValidationAttribute
    {
        public int MinimumSelectedOptions { get; set; }

        public MinimumSelectedAttribute(int minimumSelectedOptions)
        {
            MinimumSelectedOptions = minimumSelectedOptions;
        }

        public override bool IsValid(object? value)
        {
            List<UCLSelectModel> selectedList = (List<UCLSelectModel>)value;
            return selectedList.Any(x => x.IsSelected);
        }
    }
}
