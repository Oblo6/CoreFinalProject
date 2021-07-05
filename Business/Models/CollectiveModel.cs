using AppCore.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Business.Models
{
    public class CollectiveModel : Record
    {
        [Required]
        [StringLength(200)]
        [DisplayName("Group Name")]
        public string Name { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("User Count")]
        public int UserCount { get; set; }

        public List<UserModel> Users { get; set; }

        [DisplayName("Users")]
        public List<int> UserIds { get; set; }

        private string _names;
        [DisplayName("Users")]
        public string UserNamesHtml
        {
            get
            {
                _names = "";
                if (Users != null && Users.Count > 0)
                {
                    foreach (var item in Users)
                    {
                        _names += item.UserName + ", ";
                    }
                }
                return _names.Trim(' ', ',');
            }
        }

        public int CollectiveUserId { get; set; }

        public List<int> ExpenseIds { get; set; }

        private int _expenseCount;
        [DisplayName("Expense Count")]
        public int ExpenseCount
        {
            get
            {
                _expenseCount = 0;
                if (Users != null && Users.Count > 0)
                    foreach (var user in Users)
                        _expenseCount += user.Expenses.Count();
                return _expenseCount;
            }
        }
        private double _totalCost;

        [DisplayName("Total Cost")]
        //public double TotalCost { get; set; }
        public double TotalCost
        {
            get
            {
                _totalCost = 0;
                if (Users != null)
                    foreach (var user in Users)
                        foreach (var expense in user.Expenses)
                            _totalCost += expense.Cost;
                return _totalCost;
            }
        }

        private double _userCost;
        [DisplayName("Expense Per Person")]
        public double ExpensePerPerson
        {
            get
            {
                _userCost = TotalCost / UserCount;
                return _userCost;
            }
        }


        private string paid;
        [DisplayName("Total payments made by users")]
        public string PaidByUsers
        {
            get
            {
                paid = "";
                if (Users != null && Users.Count > 0)
                    foreach (var user in Users)
                        paid += user.UserName + ": " + (user.Expenses.Sum(e => e.Cost) + " ").ToString();
                return paid;
            }
        }

        private string groupReturn;
        [DisplayName("Users remaining payments")]
        public string MustPay
        {
            get
            {
                groupReturn = "";
                if (Users != null && Users.Count > 0)
                    foreach (var user in Users)
                        groupReturn += user.UserName + ": " + (ExpensePerPerson - user.Expenses.Sum(e => e.Cost) + " ").ToString();
                return groupReturn;
            }
        }
    }
}
