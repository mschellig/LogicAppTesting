using LiveDemo_UnitTests.DTOs;

namespace LiveDemo_UnitTests.Preparations
{
    public static class ReverseGeocodePreparations
    {
        //ISS Position in Pacific Ocean
        public static ReverseGeocode_DTO CreateOceanDTO()
        {
            return new()
            {
                Latitude = 34.2554,
                Longitude = -150.7447,
                City = "",
                Continent = "",
                PincipalSubdivision = ""
            };
        }

        //ISS Position in Asia
        public static ReverseGeocode_DTO CreateAsiaDTO()
        {
            return new()
            {
                Latitude = 45.2613,
                Longitude = 107.2398,
                City = "Oldziyt Sum",
                Continent = "Asia",
                PincipalSubdivision = "Dundgovĭ Aymag"
            };
        }

        //ISS Position in Baden-Württemberg
        public static ReverseGeocode_DTO CreateBaWuDTO()
        {
            return new()
            {
                Latitude = 48.49399394699852,
                Longitude = 9.189915556236617,
                City = "Reutlingen",
                Continent = "Europe",
                PincipalSubdivision = "Baden-Wurttemberg"
            };
        }

        //ISS Position in Stuttgart
        public static ReverseGeocode_DTO CreateStuttgartDTO()
        {
            return new()
            {
                Latitude = 48.776464768329326,
                Longitude = 9.178585905260924,
                City = "Stuttgart",
                Continent = "Europe",
                PincipalSubdivision = "Baden-Wurttemberg"
            };
        }
    }
}
