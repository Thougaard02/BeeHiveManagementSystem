using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHiveManagementSystem
{
    public static class HoneyVault
    {
        private static float honey = 25f;
        private static float nectar = 100f;

        public const float NECTAR_CONVERSION_RATIO = .19f;
        public const float LOW_LEVEL_WARNING = 10f;
        public static string StatusReport
        {
            get
            {
                string status = $"{honey: 0.0} units of honey\n" +
                                $"{nectar: 0.0 units of nectar}";
                string warnings = "";
                if (honey < LOW_LEVEL_WARNING)
                {
                    warnings += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
                }
                if (nectar < NECTAR_CONVERSION_RATIO)
                {
                    warnings += "\nLOW NECTAR - ADD A NECTAR COLLECTOR";
                }
                return status + warnings;
            }
        }

        public static void CollectNectar(float amount)
        {
            if (amount > 0f)
            {
                nectar += amount;
            }
        }
        public static void ConvertNectarToHoney(float amount)
        {
            float nectarToConvert = amount;
            if (nectarToConvert > nectar)
            {
                nectarToConvert = nectar;
            }

            nectar -= nectarToConvert;
            honey += nectarToConvert * NECTAR_CONVERSION_RATIO;
        }

        public static bool ConsumeHoney(float amount)
        {
            if (amount > honey)
            {
                honey -= amount;
                return true;
            }
            return false;
        }
    }
}
