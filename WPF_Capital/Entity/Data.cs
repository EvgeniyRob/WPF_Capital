using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using WPF_Capital.Enums;

namespace WPF_Capital.Entity
{
    public class Data
    {
        public Data(decimal depoStart, SrategyType srategyType) 
        {
            SrategyType = srategyType;

            Depo = depoStart;
        }

        #region Propertis-------------------------------------------------------------------

        public SrategyType SrategyType { get; set; }


        public decimal Depo
        {
            get => _depo;

            set
            {
                _depo = value;
                ResultDepo = value;
            }
        }
        decimal _depo;
        /// <summary>
        /// Результата эквити (депо)
        /// </summary>
        public decimal ResultDepo
        {
            get => _resultDepo;

            set
            {
                _resultDepo = value;

                Profit = ResultDepo - Depo;

                PercentProfit = Profit * 100 / Depo;

                ListEquity.Add(ResultDepo);

                CalcDrawDown();   //  (1.9) 18.02
            }
        }
        decimal _resultDepo;

        public decimal Profit { get; set; }
        /// <summary>
        /// Относительный профит в процентах
        /// </summary>
        public decimal PercentProfit { get; set; }


        /// <summary>
        /// Максимальная абсольтная просадка в деньгах
        /// </summary>
        public decimal MaxDrawDown 
        {
            get => _maxDrawDown;

            set 
            {
                _maxDrawDown = value;  //1:15:10  

                CalcPercentDrawDown();

            } 
        }
        decimal _maxDrawDown;
        /// <summary>
        /// Максимальная относительная просадка в процентах
        /// </summary>
        public decimal PercentDrawDown { get; set; }



        #endregion

        #region Fields---------------------------------------------------------------------------------------

        List<decimal> ListEquity= new List<decimal>();

        private decimal _max = 0;

        private decimal _min = 0;


        #endregion

        #region Methods----------------------------------------------------------------------------------

        public List<decimal> GetListEquity()
        {
            return ListEquity;
        }

        private void CalcDrawDown()
        {
            if (_max < ResultDepo)
            {
                _max = ResultDepo;
                _min = ResultDepo;
            }
            if(_min > ResultDepo)
            {
                _min = ResultDepo;

                if(MaxDrawDown < _max - _min)
                {
                    MaxDrawDown = _max - _min;
                }
            }
        }

        private void CalcPercentDrawDown()
        {
            decimal percent = MaxDrawDown * 100 / ResultDepo;

            if (percent >PercentDrawDown) PercentDrawDown= Math.Round(percent, 2);

        }
        #endregion
    }
}
