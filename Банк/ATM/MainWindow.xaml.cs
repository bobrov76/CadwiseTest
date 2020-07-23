using System;
using System.Windows;

namespace ATM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int fifty,hundred, twoHundred, fiveHundred, thousand, fiveThousand;
        int fiftyTemp, hundredTemp, twoHundredTemp, fiveHundredTemp, thousandTemp, fiveThousandTemp;
        int itogSumm = 0;
        

        private int maxAmount = 500000; // Максимальная сумма     
        private int minAmount = 10; // Минимальная сумма
        private int summMoney = 0;
        private bool isGiveMoney = false;

        public MainWindow()
        {
            InitializeComponent();

            //Обработчики событий
            giveMoneyBt.Click += GiveMoneyBt_Click;
            takeMoneyBt.Click += TakeMoneyBt_Click;
            oneBt.Click += OneBt_Click;
            twoBt.Click += TwoBt_Click;
            threeBt.Click += ThreeBt_Click;
            fourBt.Click += FourBt_Click;
            fiveBt.Click += FiveBt_Click;
            sixBt.Click += SixBt_Click;
            addBtn.Click += AddBtn_Click;
            amauntTB.PreviewTextInput += (s, e) => { if (!char.IsDigit(e.Text, 0)) e.Handled = true; };
            getSummTB.PreviewTextInput += (s, e) => { if (!char.IsDigit(e.Text, 0)) e.Handled = true; };
            getBtn.Click += GetBtn_Click;

            statusLable.Content = "Режим - Прием денег";

            getisDisable(); // Проверка пополнение или выдача

            if (summMoney == 0) giveMoneyBt.IsEnabled = false; // Если банк пустой блокируем снятие 

            getCountBills(); // Заполнение колличества купюр
        }

        private void GetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (getSummTB.Text != null && getSummTB.Text != "")
            {
                int summ = Convert.ToInt32(getSummTB.Text);
                if (summ >= minAmount && summ <= itogSumm)
                {
                    popolnenie();
                    itogSumm -= summ;
                    countLable.Content = "Всего в банкомате - " + itogSumm + " руб. ";
                }
                else
                {
                    getSummTB.Text = "";
                    MessageBox.Show("Предупреждение", "Минимальная сумма 10 руб максимальная " + itogSumm, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (amauntTB.Text != null && amauntTB.Text != "")
            {
                int summ = Convert.ToInt32(amauntTB.Text);

                if (summ > minAmount && summ + itogSumm <= maxAmount)
                {                    
                    takeManys();
                    giveMoneyBt.IsEnabled = true;
                }
                else
                {
                    summMoney = 0;
                    amauntTB.Text = "";
                    MessageBox.Show("Предупреждение", "Минимальная сумма 10 руб максимальная " + (maxAmount - itogSumm), MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void TakeMoneyBt_Click(object sender, RoutedEventArgs e)
        {
            statusLable.Content = "Режим - Прием денег";
            isGiveMoney = false;
            getisDisable();
        }

        private void GiveMoneyBt_Click(object sender, RoutedEventArgs e)
        {
            statusLable.Content = "Режим - Выдача денег";
            isGiveMoney = true;
            getisDisable();
        }

        private void SixBt_Click(object sender, RoutedEventArgs e)
        {
            if (setMany(5000))
            {
                sixBt.IsEnabled = false;               
            }
            else 
            {
                sixBt.IsEnabled = true;
                fiveThousandTemp += isGiveMoneys();
                summMoney += 5000;
                amauntTB.Text = summMoney.ToString();
            }
        }

        private void FiveBt_Click(object sender, RoutedEventArgs e)
        {
            if (setMany(1000))
            {
                fiveBt.IsEnabled = false;
            }
            else 
            {
                fiveBt.IsEnabled = true;
                thousandTemp += isGiveMoneys();
                summMoney += 1000;
                amauntTB.Text = summMoney.ToString();
            }
                
        }

        private void FourBt_Click(object sender, RoutedEventArgs e)
        {
            if (setMany(500))
            {
                fourBt.IsEnabled = false;
            }
            else
            {
                fourBt.IsEnabled = true;
                fiveHundredTemp += isGiveMoneys();
                summMoney += 500;
                amauntTB.Text = summMoney.ToString();
            }
        }

        private void ThreeBt_Click(object sender, RoutedEventArgs e)
        {
            if (setMany(100))
            {
                threeBt.IsEnabled = false;
            }
            else
            {
                threeBt.IsEnabled = true;
                twoHundredTemp += isGiveMoneys();
                summMoney += 200;
                amauntTB.Text = summMoney.ToString();
            }
        }

        private void TwoBt_Click(object sender, RoutedEventArgs e)
        {
            if (setMany(100))
            {
                twoBt.IsEnabled = false;
            }
            else
            {
                twoBt.IsEnabled = true;
                hundredTemp += isGiveMoneys();
                summMoney += 100;
                amauntTB.Text = summMoney.ToString();
            }
        }

        private void OneBt_Click(object sender, RoutedEventArgs e)
        {
            if (setMany(50))
            {
                oneBt.IsEnabled = false;
            }
            else
            {
                oneBt.IsEnabled = true;
                fiftyTemp += isGiveMoneys();
                summMoney += 50;
                amauntTB.Text = summMoney.ToString();
            }
        }

        // Формирование отображения купют
        private void getCountBills() 
        {
            fiftyLable.Content = fifty + " Купюр";
            hundredLable.Content = hundred + " Купюр";
            twoHundredLable.Content = twoHundred + " Купюр";
            fiveHundredLable.Content = fiveHundred + " Купюр";
            thousandLable.Content = thousand + " Купюр";
            fiveThousandLable.Content = fiveThousand + " Купюр";
        }

        // Подсчет суммы попалнения
        private int calcSumm() 
        {
            return fifty * 50 + hundred * 100 + twoHundred * 200 + fiveHundred * 500 + 
                thousand * 1000 + fiveThousand * 5000;
        }

        // Проверка получаем или выдаем деньги
        private int isGiveMoneys() 
        {
            int result = 0;
            if (!isGiveMoney)
            {
                result += 1; 
            } 

            getCountBills();
            calcSumm();

            amauntTB.Text = summMoney.ToString();
            return result;
        }

        // Отображение общей суммы в банке
        private void takeManys() 
        {
            if(fiftyTemp != 0) fifty += fiftyTemp;
            if (hundredTemp != 0) hundred += hundredTemp;
            if (twoHundredTemp != 0) twoHundred = twoHundredTemp;
            if (fiveHundredTemp != 0) fiveHundred += fiveHundredTemp;
            if (thousandTemp != 0) thousand += thousandTemp;
            if (fiveThousandTemp != 0) fiveThousand += fiveThousandTemp;

            fiftyTemp = 0;
            hundredTemp = 0;
            twoHundredTemp = 0;
            fiveHundredTemp = 0;
            thousandTemp = 0;
            fiveThousandTemp = 0;
            amauntTB.Text = "";
            itogSumm += summMoney;
            summMoney = 0;
            getCountBills();
            countLable.Content = "Всего в банкомате - " + itogSumm + " руб. ";
        }

        // Стилизация элементов
        private void getisDisable() 
        {
            oneBt.IsEnabled = !isGiveMoney;
            twoBt.IsEnabled = !isGiveMoney;
            threeBt.IsEnabled = !isGiveMoney;
            fourBt.IsEnabled = !isGiveMoney;
            fiveBt.IsEnabled = !isGiveMoney;
            sixBt.IsEnabled = !isGiveMoney;
            addBtn.IsEnabled = !isGiveMoney;
            getSummTB.IsEnabled = isGiveMoney;
            fiftyCB.IsEnabled = isGiveMoney;
            hundredCB.IsEnabled = isGiveMoney;
            twoHundredCB.IsEnabled = isGiveMoney;
            fiveHundredCB.IsEnabled = isGiveMoney;
            thousandCB.IsEnabled = isGiveMoney;
            fiveThousandCB.IsEnabled = isGiveMoney;
            getBtn.IsEnabled = isGiveMoney;
        }

        // Проверка внесено ли средств больше чем должно быть в банкомате
        private bool setMany(int number) 
        {
            if (amauntTB.Text != null && amauntTB.Text != "")
            {
                int summ = Convert.ToInt32(amauntTB.Text);

                if (itogSumm + summ + number > maxAmount) return true;
                else return false;                
            }
            else return false; 

        }

        // Обработка поступления средств
        private void popolnenie() 
        {
            getOneLable.Content = "";
            getTwoLable.Content = "";
            getFourLable.Content = "";
            getThreeLable.Content = "";
            getFiveLable.Content = "";
            getSixLable.Content = "";
            getSevenLable.Content = "";
            

            int value = Convert.ToInt32(getSummTB.Text);
            int x = 0;

            if ((bool)fiveThousandCB.IsChecked) 
            {
                if (fiveThousand >= value / 5000 && value >= 5000) {
                    fiveThousandTemp += value / 5000;
                    value %= 5000;
                    
                    getOneLable.Content = "5000 * " + fiveThousandTemp + " = " + fiveThousandTemp * 5000; 
                }
                if ((bool)thousandCB.IsChecked && value !=0 && value >= 1000) 
                {
                    if (thousand >= value / 1000)
                    {
                        thousandTemp += value / 1000;
                        value %= 1000;
                        
                        getTwoLable.Content = "1000 * " + thousandTemp + " = " + thousandTemp * 1000;
                    }
                }
                if ((bool)fiveHundredCB.IsChecked && value != 0 && value >= 500) 
                {
                    if (fiveHundred >= value / 500)
                    {
                        fiveHundredTemp += value / 500;
                        value %= 500;
                        
                        getFourLable.Content = "500 * " + fiveHundredTemp + " = " + fiveHundredTemp * 500;
                    }
                }
                if ((bool)twoHundredCB.IsChecked && value != 0 && value >= 200) 
                {
                    if (twoHundred >= value / 200)
                    {
                        twoHundredTemp += value / 200;
                        value %= 200;
                        
                        getFiveLable.Content = "200 * " + twoHundredTemp + " = " + twoHundredTemp * 200;
                    }
                }
                if ((bool)hundredCB.IsChecked && value != 0 && value >= 100) 
                {
                    if (hundred >= value / 100)
                    {
                        hundredTemp += value / 100;
                        value %= 100;
                        
                        getThreeLable.Content = "100 * " + hundredTemp + " = " + hundredTemp * 100;
                    }
                }
                if ((bool)fiftyCB.IsChecked && value != 0 && value >= 50)
                {
                    if (fifty >= value / 50)
                    {
                        fiftyTemp += value / 50;
                        value %= 50;
                       
                        getSixLable.Content = "50 * " + fiftyTemp + " = " + fiftyTemp * 50;
                    }
                }
                if (value != 0) 
                {
                    getSevenLable.Content = "Остальными купюрами : " + value;
                } 
            }
            else if ((bool)thousandCB.IsChecked) 
            {
                if (thousand >= value / 1000 && value >= 1000)
                {
                    thousandTemp += value / 1000;
                    value %= 1000;

                    getTwoLable.Content = "1000 * " + thousandTemp + " = " + thousandTemp * 1000;
                }

                if ((bool)fiveHundredCB.IsChecked && value != 0 && value >= 500)
                {
                    if (fiveHundred >= value / 500)
                    {
                        fiveHundredTemp += value / 500;
                        value %= 500;

                        getFourLable.Content = "500 * " + fiveHundredTemp + " = " + fiveHundredTemp * 500;
                    }
                }
                if ((bool)twoHundredCB.IsChecked && value != 0 && value >= 200)
                {
                    if (twoHundred >= value / 200)
                    {
                        twoHundredTemp += value / 200;
                        value %= 200;

                        getFiveLable.Content = "200 * " + twoHundredTemp + " = " + twoHundredTemp * 200;
                    }
                }
                if ((bool)hundredCB.IsChecked && value != 0 && value >= 100)
                {
                    if (hundred >= value / 100)
                    {
                        hundredTemp += value / 100;
                        value %= 100;

                        getThreeLable.Content = "100 * " + hundredTemp + " = " + hundredTemp * 100;
                    }
                }
                if ((bool)fiftyCB.IsChecked && value != 0 && value >= 50)
                {
                    if (fifty >= value / 50)
                    {
                        fiftyTemp += value / 50;
                        value %= 50;

                        getSixLable.Content = "50 * " + fiftyTemp + " = " + fiftyTemp * 50;
                    }
                }
                if (value != 0)
                {
                    getSevenLable.Content = "Остальными купюрами : " + value;
                }
            }
            else if ((bool)fiveHundredCB.IsChecked) 
            {

                if (fiveHundred >= value / 500 && value >= 500)
                {
                    fiveHundredTemp += value / 500;
                    value %= 500;

                    getFourLable.Content = "500 * " + fiveHundredTemp + " = " + fiveHundredTemp * 500;
                }

                if ((bool)twoHundredCB.IsChecked && value != 0 && value >= 200)
                {
                    if (twoHundred >= value / 200)
                    {
                        twoHundredTemp += value / 200;
                        value %= 200;

                        getFiveLable.Content = "200 * " + twoHundredTemp + " = " + twoHundredTemp * 200;
                    }
                }
                if ((bool)hundredCB.IsChecked && value != 0 && value >= 100)
                {
                    if (hundred >= value / 100)
                    {
                        hundredTemp += value / 100;
                        value %= 100;

                        getThreeLable.Content = "100 * " + hundredTemp + " = " + hundredTemp * 100;
                    }
                }
                if ((bool)fiftyCB.IsChecked && value != 0 && value >= 50)
                {
                    if (fifty >= value / 50)
                    {
                        fiftyTemp += value / 50;
                        value %= 50;

                        getSixLable.Content = "50 * " + fiftyTemp + " = " + fiftyTemp * 50;
                    }
                }
                if (value != 0)
                {
                    getSevenLable.Content = "Остальными купюрами : " + value;
                }
            }
            else if ((bool)twoHundredCB.IsChecked) 
            {

                if (twoHundred >= value / 200 && value >= 200)
                {
                    twoHundredTemp += value / 200;
                    value %= 200;

                    getFiveLable.Content = "200 * " + twoHundredTemp + " = " + twoHundredTemp * 200;
                }

                if ((bool)hundredCB.IsChecked && value != 0 && value >= 100)
                {
                    if (hundred >= value / 100)
                    {
                        hundredTemp += value / 100;
                        value %= 100;

                        getThreeLable.Content = "100 * " + hundredTemp + " = " + hundredTemp * 100;
                    }
                }
                if ((bool)fiftyCB.IsChecked && value != 0 && value >= 50)
                {
                    if (fifty >= value / 50)
                    {
                        fiftyTemp += value / 50;
                        value %= 50;

                        getSixLable.Content = "50 * " + fiftyTemp + " = " + fiftyTemp * 50;
                    }
                }
                if (value != 0)
                {
                    getSevenLable.Content = "Остальными купюрами : " + value;
                }
            }
            else if ((bool)hundredCB.IsChecked) 
            {
                if (hundred >= value / 100 && value >= 100)
                {
                    hundredTemp += value / 100;
                    value %= 100;

                    getThreeLable.Content = "100 * " + hundredTemp + " = " + hundredTemp * 100;
                }

                if ((bool)fiftyCB.IsChecked && value != 0 && value >= 50)
                {
                    if (fifty >= value / 50)
                    {
                        fiftyTemp += value / 50;
                        value %= 50;

                        getSixLable.Content = "50 * " + fiftyTemp + " = " + fiftyTemp * 50;
                    }
                }
                if (value != 0)
                {
                    getSevenLable.Content = "Остальными купюрами : " + value;
                }
            }
            else if ((bool)fiftyCB.IsChecked) 
            {
                if (fifty >= value / 50)
                {
                    fiftyTemp += value / 50;
                    value %= 50;

                    getSixLable.Content = "50 * " + fiftyTemp + " = " + fiftyTemp * 50;
                }

                if (value != 0)
                {
                    getSevenLable.Content = "Остальными купюрами : " + value;
                }
            }

            fifty -= fiftyTemp;
            hundred -= hundredTemp;
            twoHundred -= twoHundredTemp;
            fiveHundred -= fiveHundredTemp;
            thousand -= thousandTemp;
            fiveThousand -= fiveThousandTemp;

            getCountBills();

            fiftyTemp = 0;
            hundredTemp = 0;
            twoHundredTemp = 0;
            fiveHundredTemp = 0;
            thousandTemp = 0;
            fiveThousandTemp = 0;
        }       
    }
}
