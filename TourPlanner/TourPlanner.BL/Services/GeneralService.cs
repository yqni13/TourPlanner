using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.Models.Enums;

namespace TourPlanner.BL.Services
{
    public static class GeneralService
    {
        public static double StringTimeConverterToSeconds(string time)
        {
            DateTime timeType = DateTime.Parse(time);
            double seconds = timeType.Second;
            double minutes = timeType.Minute * 60;
            double hours = timeType.Hour * 3600;
            return seconds + minutes + hours;
        }

        public static Tour AddTourDescription(Tour tour, string description)
        {
            tour.Description = description;
            return tour;
        }

        public static List<string> GetRouteTypes()
        {
            List<string> types = new();
            types.Add("fastest");
            types.Add("shortest");
            types.Add("pedestrian");
            types.Add("bicyle");

            return types;
        }

        public static int CalculatePopularity(Tour tour)
        {
            int result = 0;
            foreach(TourLogs log in tour.TourLogs)
            {
                result += 1;
            }
            return result;
        }

        public static string CalculateChildFriendly(Tour tour)
        {
            return "Childfriendly";
        }        

        public static string AverageTime(Collection<TourLogs> tourLogs)
        {
            string convertingTime;
            double timeInSeconds = 0;
            foreach (TourLogs logs in tourLogs)
            {
                convertingTime = logs.TotalTime.ToString();
                timeInSeconds += GeneralService.StringTimeConverterToSeconds(convertingTime);
            }

            // Converting back to correct calculated and readable hh:mm:ss string.
            timeInSeconds = timeInSeconds / tourLogs.Count;

            return TimeSpan.FromSeconds(timeInSeconds).ToString();
        }

        public static double AverageDifficulty(Collection<TourLogs> tourlogs)
        {
            double number = 0;
            foreach (TourLogs logs in tourlogs)
            {
                var enumNumber = EnumCalculations.EnumDifficultyToDouble(logs.Difficulty);
                number += enumNumber;
            }
            return number / tourlogs.Count;
        }

        public static double AverageRating(Collection<TourLogs> tourlogs)
        {
            double number = 0;
            foreach (TourLogs logs in tourlogs)
            {
                var enumNumber = EnumCalculations.EnumRatingToDouble(logs.Rating);
                number += enumNumber;
            }
            return number / tourlogs.Count;
        }
                
    }    
}
