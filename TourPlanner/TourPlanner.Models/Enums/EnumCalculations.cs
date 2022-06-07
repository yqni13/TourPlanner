using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models.Enums
{
    public static class EnumCalculations
    {
        public static double EnumDifficultyToDouble(ETourDifficulty val)
        {
            switch (val)
            {
                case ETourDifficulty.Warmup:
                    return 1;
                case ETourDifficulty.Easy:
                    return 2;
                case ETourDifficulty.Moderate:
                    return 3;
                case ETourDifficulty.Hard:
                    return 4;
                case ETourDifficulty.Expert:
                    return 5;
                default:
                    return 1;
            }
        }

        public static double EnumRatingToDouble(ETourRating val)
        {
            switch (val)
            {
                case ETourRating.Worst:
                    return 1;
                case ETourRating.Bad:
                    return 2;
                case ETourRating.Weak:
                    return 3;
                case ETourRating.Improveable:
                    return 4;
                case ETourRating.Moderate:
                    return 5;
                case ETourRating.Advancement:
                    return 6;
                case ETourRating.Good:
                    return 7;
                case ETourRating.Excellent:
                    return 8;
                case ETourRating.Satisfying:
                    return 9;
                case ETourRating.Perfect:
                    return 10;
                default:
                    return 1;
            }
        }
    }
}
