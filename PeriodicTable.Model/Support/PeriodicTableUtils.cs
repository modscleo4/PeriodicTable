namespace PeriodicTable.Model.Support
{
    public class PeriodicTableUtils
    {
        /// <summary>
        /// Get the max eletrons of some period
        /// </summary>
        /// <param name="period">The period</param>
        /// <returns>Returns the max eletrons of the period (2, 8, 18, 32, ...)</returns>
        public uint GetPeriodMaxE(uint period)
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
        public uint GetLastOfPeriod(uint period)
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
        public uint GetGroup(uint numberOfElectrons)
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
        public uint GetPeriod(uint numberOfElectrons)
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
    }
}
