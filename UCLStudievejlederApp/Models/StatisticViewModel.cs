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

        //formulare per afdeling %

        public double amountOfFredericiaFormulars { get; set; }
        public double amountOfJellingFormulars { get; set; }
        public double amountOfOdenseFormulars { get; set; }
        public double amountOfSvendborgFormulars { get; set; }
        public double amountOfVejleFormulars { get; set; }

        //formulare per afdeling antal

        public double TimeUseOneCountFormulars { get; set; }
        public double TimeUseTwoCountFormulars { get; set; }
        public double TimeUseThreeCountFormulars { get; set; }


        //Procent svarede emne

        public double AdminSubFormulars  { get; set; }

        public double BarselSubFormulars { get; set; }
        public double EksamenSubFormulars { get; set; }

        public double TrivselSubFormulars { get; set; }

        public double InternationalSubFormulars { get; set; }

        public double MistrivselSubFormulars { get; set; }

        public double OptVejSubFormulars { get; set; }

        public double OrdensrelSubFormulars { get; set; }

        public double OrlovSubFormulars { get; set; }

        public double OverflytGenindSubFormulars { get; set; }

        public double PersonForhSubFormulars { get; set; }
        public double PraktikSubFormulars { get; set; }
        public double IkRelSubFormulars { get; set; }

        public double SPSSubFormulars { get; set; }
        public double StudOphSubFormulars { get; set; }
        public double StdPlanSubFormulars { get; set; }
        public double StdTvivlSubFormulars { get; set; }

        public double StdUdfSubFormulars { get; set; }
        public double SygEgneSubFormulars { get; set; }
        public double UnvSamaVanSubFormulars { get; set; }

        public double OkonomiSubFormulars { get; set; }

    }
}
