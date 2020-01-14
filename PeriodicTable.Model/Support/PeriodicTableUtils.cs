using PeriodicTable.Model.DAO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PeriodicTable.Model.Support
{
    public static class PeriodicTableUtils
    {
        /// <summary>
        /// Get the max eletrons of some period
        /// </summary>
        /// <param name="period">The period</param>
        /// <returns>Returns the max eletrons of the period (2, 8, 18, 32, ...)</returns>
        public static uint GetPeriodMaxE(uint period)
        {
            var count = 2U;

            for (var i = 1U; i <= period / 2; i++)
            {
                count += (2 + i * 4);
            }

            return count;
        }

        /// <summary>
        /// Gets the last element of some period
        /// </summary>
        /// <param name="period">The period</param>
        /// <returns>Returns the last element of the period (2, 10, 18, 36...)</returns>
        public static uint GetLastOfPeriod(uint period)
        {
            var last = 0U;
            for (var i = 1U; i <= period; i++)
            {
                last += GetPeriodMaxE(i);
            }

            return last;
        }

        /// <summary>
        /// Get the group (column) of the element in the period table
        /// </summary>
        /// <param name="numberOfElectrons">The number of electrons (or the number of protons)</param>
        /// <returns>Returns the group of the element</returns>
        public static uint GetGroup(uint numberOfElectrons)
        {
            var group = numberOfElectrons;
            var period = GetPeriod(numberOfElectrons);

            group -= GetLastOfPeriod(period - 1);

            return group;
        }

        /// <summary>
        /// Get the period (row) of the element in the period table
        /// </summary>
        /// <param name="numberOfElectrons">The number of electrons (or the number of protons)</param>
        /// <returns>Returns the period of the element</returns>
        public static uint GetPeriod(uint numberOfElectrons)
        {
            var period = 1U;
            var numE = (int)numberOfElectrons;

            while (numE > 0)
            {
                numE -= (int)GetPeriodMaxE(period);

                if (numE > 0)
                {
                    period++;
                }
            }

            return period;
        }

        /// <summary>
        /// Get the electronic distribution (1s2, 2s2, 2p6...)
        /// Warning: this function does not have treatment for exceptions like Cr, Cu and Au, those which don't follow Linus Pauling distribution.
        /// </summary>
        /// <param name="numberOfElectrons">The number of electrons (or the number of protons)</param>
        /// <returns>Returns a List<string> with the electronic distribution</returns>
        public static List<string> ElectronicDistribution(uint numberOfElectrons)
        {
            var ret = new List<string>();

            string[] sublevels = { "s", "p", "d", "f", "g" };

            var lastRow = 0;
            var row = 0;
            var col = 0;

            var incLastRow = false;

            while (col >= 0)
            {
                if ((int)numberOfElectrons - (2 + col * 4) < 0)
                {
                    break;
                }
                else
                {
                    numberOfElectrons -= (uint)(2 + col * 4);
                    ret.Add($"{row + 1}{sublevels[col]}{2 + col * 4}");
                }

                if (col == 0)
                {
                    row = lastRow;

                    if (incLastRow)
                    {
                        lastRow++;
                    }

                    col = lastRow;
                    incLastRow = !incLastRow;
                }
                else
                {
                    col--;
                }

                row++;
            }

            if (numberOfElectrons > 0)
            {
                ret.Add($"{row + 1}{sublevels[col]}{numberOfElectrons}");
            }

            return ret;
        }

        /// <summary>
        /// Electronic distribution via API
        /// </summary>
        /// <param name="numberOfElectrons">The number of electrons (or the number of protons)</param>
        /// <returns>Returns the electronic distribution</returns>
        public static List<string> APIElectronicDistribution(uint numberOfElectrons)
        {
            var ret = new List<string>();
            var electronicConfiguration = ElementDAO.Select((int)numberOfElectrons).ElectronicConfiguration;

            while (Regex.Match(electronicConfiguration, @"\[.*\]").Success)
            {
                var e = ElementDAO.Select(Regex.Replace(electronicConfiguration, @"^.*\[(.*)\].*$", @"$1"));
                electronicConfiguration = Regex.Replace(electronicConfiguration, @"(\[(.*)\])", e.ElectronicConfiguration);
            }

            foreach (var s in electronicConfiguration.Split(' '))
            {
                ret.Add(s);
            }

            return ret;
        }

        /// <summary>
        /// Get the available electrons
        /// </summary>
        /// <param name="numberOfElectrons">The number of electrons (or the number of protons)</param>
        /// <returns>Returns a unsigned integer with the amount of available electrons</returns>
        public static uint GetFreeElectrons(uint numberOfElectrons)
        {
            var distribution = APIElectronicDistribution(numberOfElectrons);
            distribution.Sort();

            distribution = distribution.FindAll(x => { return x.StartsWith($"{distribution[distribution.Count - 1][0]}"); });
            var s = 0U;
            foreach (var str in distribution)
            {
                var num = Convert.ToUInt32(Regex.Replace(str, "[0-9][s, p, d, f, g](.*)", "$1"));
                s += num;
            }

            return s;
        }

        /// <summary>
        /// Get the amount of electrons per level (2, 8, 8, 18, 18...)
        /// </summary>
        /// <param name="numberOfElectrons">The number of electrons (or the number of protons)</param>
        /// <returns>Returns a string with the amount of electrons per level</returns>
        public static string GetElectronsPerLevel(uint numberOfElectrons)
        {
            var distribution = APIElectronicDistribution(numberOfElectrons);
            distribution.Sort();

            var d = new List<uint>();
            for (var i = 1; i <= distribution[distribution.Count - 1][0] - '0'; i++)
            {
                var dist = distribution.FindAll(x => { return x.StartsWith($"{i}"); });
                var sum = 0U;

                foreach (var s in dist)
                {
                    var num = Convert.ToUInt32(Regex.Replace(s, "[0-9][s, p, d, f, g](.*)", "$1"));
                    sum += num;
                }

                d.Add(sum);
            }

            var str = string.Join("\n", d);
            return str;
        }
    }
}
