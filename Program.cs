using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _1_ZD
{
    class Program
    {
        static void Normil_Time(ref int hours, ref int minutes, ref int seconds)
        {
            minutes += seconds / 60;
            seconds %= 60;
            hours += minutes / 60;
            minutes %= 60;
        }

        static string MakeCorrectTimeString(int hours, int minut, int seconds)
        {
            string secondsString = "";
            if (seconds <= 9)
            {
                secondsString = String.Format("0{0}", seconds);
            }
            else
            {
                secondsString = String.Format("{0}", seconds);
            }
            string minutesString = "";
            if (minut <= 9)
            {
                minutesString = String.Format("0{0}", minut);
            }
            else
            {
                minutesString = String.Format("{0}", minut);
            }
            string hoursString = "";
            if (hours <= 9)
            {
                hoursString = String.Format("0{0}", hours);
            }
            if (hours >= 10 && hours <= 23)
            {
                hoursString = String.Format("{0}", hours);
            }
            if (hours == 24)
            {
                hoursString = String.Format("00");
            }
            string correctTimeString = String.Format("{0}:{1}:{2}", hoursString, minutesString, secondsString);
            return correctTimeString;
        }

        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("INPUT.TXT");
            StreamWriter sw = new StreamWriter("OUTPUT.TXT");
            string[] currentTime = sr.ReadLine().Split(':');
            int currentHours = Convert.ToInt32(currentTime[0]);
            int currentMinutes = Convert.ToInt32(currentTime[1]);
            int currentSeconds = Convert.ToInt32(currentTime[2]);
            bool newDayCome = false;
            if (currentSeconds == 60)
            {
                currentMinutes++;
                currentSeconds = 0;
            }
            if (currentMinutes >= 60)
            {
                if (currentHours == 23)
                {
                    newDayCome = true;
                }
                currentHours++;
                currentMinutes -= 60;
                currentHours %= 24;
            }
            string[] timerValues = sr.ReadLine().Split(':');
            sr.Close();
            int timerHours = 0, timerMinutes = 0, timerSeconds = 0;
            if (timerValues.GetLength(0) == 3)
            {
                timerHours = Convert.ToInt32(timerValues[0]);
                timerMinutes = Convert.ToInt32(timerValues[1]);
                timerSeconds = Convert.ToInt32(timerValues[2]);
            }
            if (timerValues.GetLength(0) == 2)
            {
                timerMinutes = Convert.ToInt32(timerValues[0]);
                timerSeconds = Convert.ToInt32(timerValues[1]);
            }
            if (timerValues.GetLength(0) == 1)
            {
                timerSeconds = Convert.ToInt32(timerValues[0]);
            }

            Normil_Time(ref timerHours, ref timerMinutes, ref timerSeconds);

            long currentTimeTotalSeconds = (long)currentHours * 3600 + (long)currentMinutes * 60 + currentSeconds;
            long timerTotalSeconds = (long)timerHours * 3600 + (long)timerMinutes * 60 + timerSeconds;

            long startOfNextDayTotalSeconds = 24 * 3600;
            long secondsToNextDay = startOfNextDayTotalSeconds - currentTimeTotalSeconds;

            if (secondsToNextDay > timerTotalSeconds)
            {
                int newHours = currentHours + (int)(timerTotalSeconds / 3600);
                timerTotalSeconds %= 3600;
                int newMinutes = currentMinutes + (int)(timerTotalSeconds / 60);
                timerTotalSeconds %= 60;
                int newSeconds = currentSeconds + (int)timerTotalSeconds;
                Normil_Time(ref newHours, ref newMinutes, ref newSeconds);
                string correctTimeString = MakeCorrectTimeString(newHours, newMinutes, newSeconds);
                string resultString = "";
                if (newDayCome)
                {
                    resultString = String.Format("{0}+{1} days", correctTimeString, 1);
                }
                else
                {
                    resultString = correctTimeString;
                }
                sw.Write(resultString);
                sw.Close();
                return;
            }
            else
            {
                long secondsLeft = timerTotalSeconds - secondsToNextDay;
                int newHours = 0;
                int newMinutes = 0;
                int newSeconds = 0;
                int numberOfDays = 1 + (int)(secondsLeft / (24 * 3600));
                secondsLeft %= (24 * 3600);
                newHours = (int)(secondsLeft / 3600);
                secondsLeft %= 3600;
                newMinutes = (int)(secondsLeft / 60);
                secondsLeft %= 60;
                newSeconds = (int)secondsLeft;
                Normil_Time(ref newHours, ref newMinutes, ref newSeconds);
                string correctTimeString = MakeCorrectTimeString(newHours, newMinutes, newSeconds);
                if (newDayCome)
                {
                    numberOfDays++;
                }
                string resultTimeString = String.Format("{0}+{1} days", correctTimeString, numberOfDays);
                sw.Write(resultTimeString);
                sw.Close();
                return;
            }
        }
    }
}