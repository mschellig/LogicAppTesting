using LiveDemo_UnitTests.DTOs;

namespace LiveDemo_UnitTests.Preparations
{
    public static class IssMockPreparations
    {
        //ISS Position in Pacific Ocean
        public static IssPosition_DTO CreateOceanDTO()
        {
            return new()
            {
                IssPosition = new()
                {
                    Longitude = "-150.7447",
                    Latitude = "34.2554"
                }
            };
        }

        //ISS Position in Asia
        public static IssPosition_DTO CreateAsiaDTO()
        {
            return new()
            {
                IssPosition = new()
                {
                    Longitude = "107.2398",
                    Latitude = "45.2613"
                }
            };
        }

        //ISS Position in Baden-Württemberg
        public static IssPosition_DTO CreateBaWuDTO()
        {
            return new()
            {
                IssPosition = new()
                {
                    Longitude = "9.189915556236617",
                    Latitude = "48.49399394699852"
                }
            };
        }

        //ISS Position in Stuttgart
        public static IssPosition_DTO CreateStuttgartDTO()
        {
            return new()
            {
                IssPosition = new()
                {
                    Longitude = "9.178585905260924",
                    Latitude = "48.776464768329326"
                }
            };
        }
    }
}
