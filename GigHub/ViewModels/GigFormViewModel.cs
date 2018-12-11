
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using GigHub.Controllers;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {

        public int Id { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<GigsController, ActionResult>> Update = (c => c.Update(this));
                Expression<Func<GigsController, ActionResult>>  Create = (c => c.Create(this));

                var action = (Id != 0 ? Update : Create);

                return (action.Body as MethodCallExpression).Method.Name;

            }
        }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDateValidation]
        public string Date { get; set; }

        [Required]
        [TimeValidation]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

       
        public IEnumerable<Genre> Genres { get; set; }


        public DateTime GetDateTime()
        {

            return DateTime.Parse(string.Format("{0} {1}", Date, Time));

        }

        public string HeadingText { get; set; }
    }
}