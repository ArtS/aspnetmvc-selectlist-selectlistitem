using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation;

namespace Example.Models
{
    [FluentValidation.Attributes.Validator(typeof(UserProfileModelValidator))]
    public class UserProfileModel
    {
        public SelectList list;

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        // This property holds user-selected country
        // WARNING: IT MUST BE NULLABLE, otherwise dafault value specified in the SelectList call will be ignored
        [Display(Name = "Country of birth")]
        public int? CountryOfBirthId { get; set; }

        [Display(Name = "Country of residence")]
        public int? CountryOfResidenceId { get; set; }

        // This property holds all available countries for drop down list
        public IEnumerable<Country> CountriesOfBirth { get; set; }

        // This property holds instances of SelectList items
        public IEnumerable<SelectListItem> CountriesOfResidence { get; set; }

        // Property to store human-readable name
        public string CountryOfBirthName { get; set; }        
        
        // Property to store human-readable name
        public string CountryOfResidenceName { get; set; }

        public int DefaultCountryOfBirthId { get; internal set; }
    }

    public class UserProfileModelValidator : AbstractValidator<UserProfileModel>
    {
        public UserProfileModelValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty().WithMessage("Please enter your first name");
            RuleFor(m => m.LastName).NotEmpty().WithMessage("Please enter your last name");
            RuleFor(m => m.CountryOfBirthId).NotEmpty().WithMessage("Please select your country of birth");
            RuleFor(m => m.CountryOfResidenceId).NotEmpty().WithMessage("Please select your country of residence");
        }
    }
}