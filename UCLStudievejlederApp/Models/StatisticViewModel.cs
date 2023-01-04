namespace UCLStudievejlederApp.Models
{
    public class StatisticViewModel
    {
        //Alle formularer
        public double AllFormulars{ get; set; }

        //formularer via tlelefon/virtuel/personlig
        public double PhoneFormulars{ get; set; }

        public double VirtuelleFormulars { get; set; }
        public double PersonalFormulars { get; set; }

        //tidsforbrug
        public double TimeUseOneFormulars { get; set; }
        public double TimeUseTwoFormulars { get; set; }
        public double TimeUseThreeFormulars { get; set; }

        //formulare per afdeling

        public double amountOfFredericiaFormulars { get; set; }
        public double amountOfJellingFormulars { get; set; }
        public double amountOfOdenseFormulars { get; set; }
        public double amountOfSvendborgFormulars { get; set; }
        public double amountOfVejleFormulars { get; set; }

    }
}
