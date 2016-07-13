using System.Collections.Generic;
using System.Web.Mvc;
using Example.Models;
using System.Linq;
using System;

namespace Example.Controllers
{
    public class ProfileController : Controller
    {
        //
        // 1. Action method for displaying the 'User Profile' page
        //
        public ActionResult UserProfile()
        {
            // Get existing user profile object from the session or create a new one
            var model = Session["UserProfileModel"] as UserProfileModel ?? new UserProfileModel();
            InitialiseModel(model);

            return View(model);
        }

        private void InitialiseModel(UserProfileModel model)
        {
            model.CountriesOfResidence = GetCountrySelectListItems();
            model.CountriesOfBirth = GetAllCountries();
            model.DefaultCountryOfBirthId = model.CountriesOfBirth
                .FirstOrDefault(country => country.Name.Equals("Australia", StringComparison.InvariantCultureIgnoreCase))
                .Id;
        }

        private IEnumerable<SelectListItem> GetCountrySelectListItems()
        {
            var allCountries = GetAllCountries();
            var items = new List<SelectListItem>();

            foreach (var country in allCountries)
            {
                items.Add(new SelectListItem()
                {
                    Text = country.Name,
                    Value = country.Id.ToString(),
                    Selected = country.Id == 13 ? true : false
                });
            }

            return items;
        }
        
        //
        // 2. Action method for handling user-entered data when 'Update' button is pressed.
        //
        [HttpPost]
        public ActionResult UserProfile(UserProfileModel model)
        {
            InitialiseModel(model);

            // In case everything is fine - i.e. "FirstName", "LastName" and country names are entered/selected,
            // redirect a user to the "ViewProfile" page, and pass the user object along via Session
            if (ModelState.IsValid)
            {
                Session["UserProfileModel"] = model;
                return RedirectToAction("ViewProfile");
            }

            // Something is not right - re-render the registration page, keeping user-entered data
            // and display validation errors
            return View(model);
        }

        //
        // 3. Action method for displaying 'ViewProfile' page
        //
        public ActionResult ViewProfile()
        {
            // Get user profile information from the session
            var model = Session["UserProfileModel"] as UserProfileModel;
            if (model == null)
                return RedirectToAction("UserProfile");

            // Get a human-readable description of a currently selected countries
            var countryOfBirth = GetAllCountries().Where(c => c.Id == model.CountryOfBirthId).FirstOrDefault();
            model.CountryOfBirthName = countryOfBirth.Name;

            var countryOfResidence = GetAllCountries().Where(c => c.Id == model.CountryOfResidenceId).FirstOrDefault();
            model.CountryOfResidenceName = countryOfResidence.Name;

            // Display View Profile page that shows FirstName, Last Name and a selected State.
            return View(model);
        }

        /// <summary>
        /// Simulates retrieval of countries from a DB.
        /// </summary>
        private IEnumerable<Country> GetAllCountries()
        {
            return new List<Country>
            {
                new Country {Name = "Afghanistan", Id=1},
                new Country {Name = "Albania", Id=2},
                new Country {Name = "Algeria", Id = 5},
                new Country {Name = "American Samoa", Id = 4},
                new Country {Name = "Andorra", Id = 5},
                new Country {Name = "Angola", Id = 6},
                new Country {Name = "Anguilla", Id = 7},
                new Country {Name = "Antarctica", Id = 8},
                new Country {Name = "Antigua And Barbuda", Id = 9},
                new Country {Name = "Argentina", Id = 10},
                new Country {Name = "Armenia", Id = 11},
                new Country {Name = "Aruba", Id = 12},
                new Country {Name = "Australia", Id = 13},
                new Country {Name = "Austria", Id = 14},
                new Country {Name = "Azerbaijan", Id = 15},
                new Country {Name = "Bahamas", Id = 16},
                new Country {Name = "Bahrain", Id = 17},
                new Country {Name = "Bangladesh", Id = 18},
                new Country {Name = "Barbados", Id = 19},
                new Country {Name = "Belarus", Id = 20},
                new Country {Name = "Belgium", Id = 21},
                new Country {Name = "Belize", Id = 22},
                new Country {Name = "Benin", Id = 23},
                new Country {Name = "Bermuda", Id = 24},
                new Country {Name = "Bhutan", Id = 25},
                new Country {Name = "Bolivia", Id = 26},
                new Country {Name = "Bosnia And Herzegovina", Id = 27},
                new Country {Name = "Botswana", Id = 28},
                new Country {Name = "Bouvet Island", Id = 29},
                new Country {Name = "Brazil", Id = 30},
                new Country {Name = "British Indian Ocean Territory", Id = 31},
                new Country {Name = "Brunei Darussalam", Id = 32},
                new Country {Name = "Bulgaria", Id = 33},
                new Country {Name = "Burkina Faso", Id = 34},
                new Country {Name = "Burundi", Id = 35},
                new Country {Name = "Cambodia", Id = 36},
                new Country {Name = "Cameroon", Id = 37},
                new Country {Name = "Canada", Id = 38},
                new Country {Name = "Cape Verde", Id = 39},
                new Country {Name = "Cayman Islands", Id = 40},
                new Country {Name = "Central African Republic", Id = 41},
                new Country {Name = "Chad", Id = 42},
                new Country {Name = "Chile", Id = 43},
                new Country {Name = "China", Id = 44},
                new Country {Name = "Christmas Island", Id = 45},
                new Country {Name = "Cocos (keeling) Islands", Id = 46},
                new Country {Name = "Colombia", Id = 47},
                new Country {Name = "Comoros", Id = 48},
                new Country {Name = "Congo", Id = 49},
                new Country {Name = "Congo, The Democratic Republic Of The", Id = 50},
                new Country {Name = "Cook Islands", Id = 51},
                new Country {Name = "Costa Rica", Id = 52},
                new Country {Name = "Cote D'ivoire", Id = 53},
                new Country {Name = "Croatia", Id = 54},
                new Country {Name = "Cuba", Id = 55},
                new Country {Name = "Cyprus", Id = 56},
                new Country {Name = "Czech Republic", Id = 57},
                new Country {Name = "Denmark", Id = 58},
                new Country {Name = "Djibouti", Id = 59},
                new Country {Name = "Dominica", Id = 60},
                new Country {Name = "Dominican Republic", Id = 61},
                new Country {Name = "East Timor", Id = 62},
                new Country {Name = "Ecuador", Id = 63},
                new Country {Name = "Egypt", Id = 64},
                new Country {Name = "El Salvador", Id = 65},
                new Country {Name = "Equatorial Guinea", Id = 66},
                new Country {Name = "Eritrea", Id = 67},
                new Country {Name = "Estonia", Id = 68},
                new Country {Name = "Ethiopia", Id = 69},
                new Country {Name = "Falkland Islands (malvinas)", Id = 70},
                new Country {Name = "Faroe Islands", Id = 71},
                new Country {Name = "Fiji", Id = 72},
                new Country {Name = "Finland", Id = 73},
                new Country {Name = "France", Id = 74},
                new Country {Name = "French Guiana", Id = 75},
                new Country {Name = "French Polynesia", Id = 76},
                new Country {Name = "French Southern Territories", Id = 77},
                new Country {Name = "Gabon", Id = 78},
                new Country {Name = "Gambia", Id = 79},
                new Country {Name = "Georgia", Id = 80},
                new Country {Name = "Germany", Id = 81},
                new Country {Name = "Ghana", Id = 82},
                new Country {Name = "Gibraltar", Id = 83},
                new Country {Name = "Greece", Id = 84},
                new Country {Name = "Greenland", Id = 85},
                new Country {Name = "Grenada", Id = 86},
                new Country {Name = "Guadeloupe", Id = 87},
                new Country {Name = "Guam", Id = 88},
                new Country {Name = "Guatemala", Id = 89},
                new Country {Name = "Guinea", Id = 90},
                new Country {Name = "Guinea-bissau", Id = 91},
                new Country {Name = "Guyana", Id = 92},
                new Country {Name = "Haiti", Id = 93},
                new Country {Name = "Heard Island And Mcdonald Islands", Id = 94},
                new Country {Name = "Holy See (vatican City State)", Id = 95},
                new Country {Name = "Honduras", Id = 96},
                new Country {Name = "Hong Kong", Id = 97},
                new Country {Name = "Hungary", Id = 98},
                new Country {Name = "Iceland", Id = 99},
                new Country {Name = "India", Id = 100},
                new Country {Name = "Indonesia", Id = 101},
                new Country {Name = "Iran, Islamic Republic Of", Id = 102},
                new Country {Name = "Iraq", Id = 103},
                new Country {Name = "Ireland", Id = 104},
                new Country {Name = "Israel", Id = 105},
                new Country {Name = "Italy", Id = 106},
                new Country {Name = "Jamaica", Id = 107},
                new Country {Name = "Japan", Id = 108},
                new Country {Name = "Jordan", Id = 109},
                new Country {Name = "Kazakstan", Id = 110},
                new Country {Name = "Kenya", Id = 111},
                new Country {Name = "Kiribati", Id = 112},
                new Country {Name = "Korea, Democratic People's Republic Of", Id = 113},
                new Country {Name = "Korea, Republic Of", Id = 114},
                new Country {Name = "Kosovo", Id = 115},
                new Country {Name = "Kuwait", Id = 116},
                new Country {Name = "Kyrgyzstan", Id = 117},
                new Country {Name = "Lao People's Democratic Republic", Id = 118},
                new Country {Name = "Latvia", Id = 119},
                new Country {Name = "Lebanon", Id = 120},
                new Country {Name = "Lesotho", Id = 121},
                new Country {Name = "Liberia", Id = 122},
                new Country {Name = "Libyan Arab Jamahiriya", Id = 123},
                new Country {Name = "Liechtenstein", Id = 124},
                new Country {Name = "Lithuania", Id = 125},
                new Country {Name = "Luxembourg", Id = 126},
                new Country {Name = "Macau", Id = 127},
                new Country {Name = "Macedonia, The Former Yugoslav Republic Of", Id = 128},
                new Country {Name = "Madagascar", Id = 129},
                new Country {Name = "Malawi", Id = 130},
                new Country {Name = "Malaysia", Id = 131},
                new Country {Name = "Maldives", Id = 132},
                new Country {Name = "Mali", Id = 133},
                new Country {Name = "Malta", Id = 134},
                new Country {Name = "Marshall Islands", Id = 135},
                new Country {Name = "Martinique", Id = 136},
                new Country {Name = "Mauritania", Id = 137},
                new Country {Name = "Mauritius", Id = 138},
                new Country {Name = "Mayotte", Id = 139},
                new Country {Name = "Mexico", Id = 140},
                new Country {Name = "Micronesia, Federated States Of", Id = 141},
                new Country {Name = "Moldova, Republic Of", Id = 142},
                new Country {Name = "Monaco", Id = 143},
                new Country {Name = "Mongolia", Id = 144},
                new Country {Name = "Montserrat", Id = 145},
                new Country {Name = "Montenegro", Id = 146},
                new Country {Name = "Morocco", Id = 147},
                new Country {Name = "Mozambique", Id = 148},
                new Country {Name = "Myanmar", Id = 149},
                new Country {Name = "Namibia", Id = 150},
                new Country {Name = "Nauru", Id = 151},
                new Country {Name = "Nepal", Id = 152},
                new Country {Name = "Netherlands", Id = 153},
                new Country {Name = "Netherlands Antilles", Id = 154},
                new Country {Name = "New Caledonia", Id = 155},
                new Country {Name = "New Zealand", Id = 156},
                new Country {Name = "Nicaragua", Id = 157},
                new Country {Name = "Niger", Id = 158},
                new Country {Name = "Nigeria", Id = 159},
                new Country {Name = "Niue", Id = 160},
                new Country {Name = "Norfolk Island", Id = 161},
                new Country {Name = "Northern Mariana Islands", Id = 162},
                new Country {Name = "Norway", Id = 163},
                new Country {Name = "Oman", Id = 164},
                new Country {Name = "Pakistan", Id = 165},
                new Country {Name = "Palau", Id = 166},
                new Country {Name = "Palestinian Territory, Occupied", Id = 167},
                new Country {Name = "Panama", Id = 168},
                new Country {Name = "Papua New Guinea", Id = 169},
                new Country {Name = "Paraguay", Id = 170},
                new Country {Name = "Peru", Id = 171},
                new Country {Name = "Philippines", Id = 172},
                new Country {Name = "Pitcairn", Id = 173},
                new Country {Name = "Poland", Id = 174},
                new Country {Name = "Portugal", Id = 175},
                new Country {Name = "Puerto Rico", Id = 176},
                new Country {Name = "Qatar", Id = 177},
                new Country {Name = "Reunion", Id = 178},
                new Country {Name = "Romania", Id = 179},
                new Country {Name = "Russian Federation", Id = 180},
                new Country {Name = "Rwanda", Id = 181},
                new Country {Name = "Saint Helena", Id = 182},
                new Country {Name = "Saint Kitts And Nevis", Id = 183},
                new Country {Name = "Saint Lucia", Id = 184},
                new Country {Name = "Saint Pierre And Miquelon", Id = 185},
                new Country {Name = "Saint Vincent And The Grenadines", Id = 186},
                new Country {Name = "Samoa", Id = 187},
                new Country {Name = "San Marino", Id = 188},
                new Country {Name = "Sao Tome And Principe", Id = 189},
                new Country {Name = "Saudi Arabia", Id = 190},
                new Country {Name = "Senegal", Id = 191},
                new Country {Name = "Serbia", Id = 192},
                new Country {Name = "Seychelles", Id = 193},
                new Country {Name = "Sierra Leone", Id = 194},
                new Country {Name = "Singapore", Id = 195},
                new Country {Name = "Slovakia", Id = 196},
                new Country {Name = "Slovenia", Id = 197},
                new Country {Name = "Solomon Islands", Id = 198},
                new Country {Name = "Somalia", Id = 199},
                new Country {Name = "South Africa", Id = 200},
                new Country {Name = "South Georgia And The South Sandwich Islands", Id = 201},
                new Country {Name = "Spain", Id = 202},
                new Country {Name = "Sri Lanka", Id = 203},
                new Country {Name = "Sudan", Id = 204},
                new Country {Name = "Suriname", Id = 205},
                new Country {Name = "Svalbard And Jan Mayen", Id = 206},
                new Country {Name = "Swaziland", Id = 207},
                new Country {Name = "Sweden", Id = 208},
                new Country {Name = "Switzerland", Id = 209},
                new Country {Name = "Syrian Arab Republic", Id = 210},
                new Country {Name = "Taiwan, Province Of China", Id = 211},
                new Country {Name = "Tajikistan", Id = 212},
                new Country {Name = "Tanzania, United Republic Of", Id = 213},
                new Country {Name = "Thailand", Id = 214},
                new Country {Name = "Togo", Id = 215},
                new Country {Name = "Tokelau", Id = 216},
                new Country {Name = "Tonga", Id = 217},
                new Country {Name = "Trinidad And Tobago", Id = 218},
                new Country {Name = "Tunisia", Id = 219},
                new Country {Name = "Turkey", Id = 220},
                new Country {Name = "Turkmenistan", Id = 221},
                new Country {Name = "Turks And Caicos Islands", Id = 222},
                new Country {Name = "Tuvalu", Id = 223},
                new Country {Name = "Uganda", Id = 224},
                new Country {Name = "Ukraine", Id = 225},
                new Country {Name = "United Arab Emirates", Id = 226},
                new Country {Name = "United Kingdom", Id = 227},
                new Country {Name = "United States", Id = 228},
                new Country {Name = "United States Minor Outlying Islands", Id = 229},
                new Country {Name = "Uruguay", Id = 230},
                new Country {Name = "Uzbekistan", Id = 231},
                new Country {Name = "Vanuatu", Id = 232},
                new Country {Name = "Venezuela", Id = 233},
                new Country {Name = "Viet Nam", Id = 234},
                new Country {Name = "Virgin Islands, British", Id = 235},
                new Country {Name = "Virgin Islands, U.s.", Id = 236},
                new Country {Name = "Wallis And Futuna", Id = 237},
                new Country {Name = "Western Sahara", Id = 238},
                new Country {Name = "Yemen", Id = 239},
                new Country {Name = "Zambia", Id = 240},
                new Country {Name = "Zimbabwe", Id = 241}
            };
        }
    }
}